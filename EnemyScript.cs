using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public int rangePatrol1 = -5;
    public int rangePatrol2 = 6;
    public Slider slider;
    public float radius = 10f;
    public float region = 10f;
    private int enemyMaxHP;
    private int enemyCurrHP;
    private int levelEnemy;
    private int enemyAtt;
    public Transform target;
    Transform patrol;
    NavMeshAgent agent;
    Animator anim;
    Vector3 startPost, goPatrol;
    public GameObject blood;
    public float rangeAtt = 1.8f;
    int rand;
    public Animator playerAnim;
    private float lookAround = 3.5f;
    bool death = false;
    bool normalAtt = true;
    public GameObject enemyStats;
    public Text levelText;

    public GameObject startEffect;

    public AudioSource step;
    public AudioSource darkSoul;
    AudioSource audio;
    //audioClip
    public AudioClip morakImpact;
    public AudioClip morakAtt;
    public AudioClip morakDeath;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        do
        {
            rand = Random.Range(PaladinLifeManager.level - 2, PaladinLifeManager.level + 2);
        } while (rand < 1);
        Debug.Log("Level musuh -> " +rand);
        levelEnemy = rand;
        levelText.text = "Bandid Lvl." + levelEnemy;
        enemyStats.SetActive(false);
        enemyMaxHP = calculateHealth(levelEnemy);
        enemyCurrHP = enemyMaxHP;
        enemyAtt = calculateAtt(levelEnemy);
        startPost = transform.position;
        SetMaxHealth(enemyMaxHP);
        SetHealth(enemyCurrHP);
        goPatrol = new Vector3(startPost.x + Random.Range(rangePatrol1, rangePatrol2), startPost.y, startPost.z + Random.Range(rangePatrol1, rangePatrol2));
        //target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.SetInteger("Condition", 1);
        Instantiate(startEffect, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {   
        float distance = Vector3.Distance(target.position, transform.position);
        float startDistance = Vector3.Distance(transform.position, startPost);
        if (PaladinLifeManager.death == true)
        {
            agent.speed = 0;
            return;
        }
        if (!normalAtt)
        {
            if(anim.GetInteger("Condition") != 0 && anim.GetInteger("Condition") != 3)
            {
                normalAtt = true;
                enemyAtt = calculateAtt(levelEnemy);
            }
        }
        if(enemyCurrHP <= 0)
        {
            if(death == false)
            {
                Debug.Log("Mati");
                int gotPotionPercentage = Random.Range(0, 100);
                Debug.Log("Persen potion -> " + gotPotionPercentage);
                if(gotPotionPercentage < 20)
                {
                    PaladinLifeManager.potion += 1;
                }
                PaladinLifeManager.currMana += levelEnemy * 9;
                StartCoroutine(DeathRoutine());
                anim.SetInteger("Condition", 10);
                death = true;
            }
        }
        if (death)
        {
            return;
        }
        if (playerAnim.GetBool("Attacking") == true && distance<= rangeAtt)
        {
            if(anim.GetBool("CheckImpact") == true)
            {
                return;
            }
            StartCoroutine(GotImpact());
        }

        if (anim.GetBool("Return") == true)
        {
            agent.speed = 1.5f;
            return;
        }
        if (startDistance < region)
        {
            if (distance <= radius)
            {
                enemyStats.SetActive(true);
                agent.stoppingDistance = 2f;
                if(distance > agent.stoppingDistance)
                {
                    if(anim.GetBool("Attacking") == true)
                    {
                        return;
                    }
                    agent.speed = 2.5f;
                    FaceTarget();
                    anim.SetInteger("Condition", 2);
                    agent.SetDestination(target.position);
                }
                else if (distance <= agent.stoppingDistance)
                {
                    if (anim.GetBool("Attacking") == true || anim.GetBool("AfterAtt") == true)
                    {
                        return;
                    }
                    agent.speed = 0;
                    FaceTarget();
                    Attack();
                }
            }
            else
            {
                enemyStats.SetActive(false);
                float distancePatrol = Vector3.Distance(transform.position, goPatrol);
                agent.SetDestination(goPatrol);
                agent.stoppingDistance = 0.1f;
                //Debug.Log(distancePatrol);
                agent.speed = 1.5f;
                if (distancePatrol <= 1.3f)
                {
                    Debug.Log("patrol");
                    if (lookAround > 0)
                    {
                        lookAround -= Time.deltaTime;
                        anim.SetInteger("Condition", 0);
                    }
                    else
                    {
                        anim.SetInteger("Condition", 1);
                        goPatrol = new Vector3(startPost.x + Random.Range(rangePatrol1, rangePatrol2), startPost.y, startPost.z + Random.Range(rangePatrol1, rangePatrol2));
                        lookAround = 3.5f;
                    }
                }
            }

        }
        else
        {
            Return();
        }
    }

    IEnumerator DeathRoutine()
    {
        anim.SetInteger("Condition", 10);
        yield return new WaitForSeconds(2f);
        Instantiate(startEffect, transform.position, Quaternion.identity);
        darkSoul.Play();
        //gameObject.SetActive(false);
        yield return new WaitForSeconds(4f);
        int loot = Random.Range(1, 101);
        if(loot <= 10)
        {
            PaladinScript.gotWing = true;
        }else if(loot <= 40)
        {
            PaladinLifeManager.potion += 1;
        }
        Instantiate(gameObject, startPost, Quaternion.identity);
        Destroy(gameObject);
    }


    IEnumerator GotImpact()
    {
        anim.SetBool("CheckImpact", true);
        yield return new WaitForSeconds(0.2f);
        if(PaladinLifeManager.gotAttack == false)
        {
            anim.SetBool("Impact", true);
            anim.SetInteger("Condition", 4);
            enemyCurrHP -= PaladinLifeManager.attackDmg;
            Instantiate(blood, transform.position, Quaternion.identity);
            SetHealth(enemyCurrHP);
        }
        if(enemyCurrHP > 0)
        {
            yield return new WaitForSeconds(0.5f);
            anim.SetInteger("Condition", 0);
            anim.SetBool("Impact", false);
        }
        else
        {
            anim.SetInteger("Condition", 10);
        }
        anim.SetBool("CheckImpact", false);
    }

    void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        anim.SetBool("Attacking", true);
        anim.SetInteger("Condition", 3);
        FaceTarget();
        yield return new WaitForSeconds(0.4f);
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= 2.2f && anim.GetBool("Impact") == false)
        {
            PaladinLifeManager.gotAttack = true;
            attackPal();
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("udah attack");
        anim.SetBool("Attacking", false);
        StartCoroutine(AfterAttack());
    }

    public void attackPal()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= rangeAtt)
        {
            Debug.Log("Paladin dikurangin darahnyaaa");
            PaladinLifeManager.currHP -= enemyAtt;
        }
    }

    IEnumerator AfterAttack()
    {
        anim.SetBool("AfterAtt", true);
        anim.SetInteger("Condition", 0);
        enemyAtt *= 2;
        normalAtt = false;
        yield return new WaitForSeconds(3f);
        anim.SetBool("AfterAtt", false);

    }

    void Return()
    {
        StartCoroutine(ReturnRoutine());
    }

    IEnumerator ReturnRoutine()
    {
        anim.SetBool("Return", true);
        anim.SetInteger("Condition", 1);
        agent.speed = 1.5f;
        agent.SetDestination(startPost);
        yield return new WaitForSeconds(3);
        agent.speed = 2.5f;
        anim.SetInteger("Condition", 1);
        Debug.Log("Balikkk");
        anim.SetBool("Return", false);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion rotate = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, region);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, rangeAtt);
    }

    private int calculateAtt(int level)
    {
        if(level <= 1)
        {
            return 2;
        }
        return level * 2 + calculateAtt(level - 1);
    }

    private int calculateHealth(int level)
    {
        if (level <= 1)
        {
            return 17;
        }
        return level * 17 + calculateHealth(level - 1);
    }

    public void SetMaxHealth(int maxHP)
    {
        slider.maxValue = maxHP;
    }

    public void SetHealth(int currHP)
    {
        slider.value = currHP;
    }


    //audio
    public void MorakImpact()
    {
        audio.PlayOneShot(morakImpact);
    }

    public void MorakAtt()
    {
        audio.PlayOneShot(morakAtt);
    }

    public void MorakDeath()
    {
        audio.PlayOneShot(morakDeath);
    }

    public void StepMorak()
    {
        step.Play();
    }
}
