using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaladinLifeManager : MonoBehaviour
{
    public Text levelTxt;
    public Text potionTxt;
    public static int level = 1;
    protected int maxHP = 15;
    public static int currHP;
    protected int maxMana = 16;
    public static int potion = 5;
    public static int attackDmg = 2;
    public static int currMana;
    public ManaBarScript manaBar;
    public HealthBarScript healthBar;
    public ChangePortal1Script changePortal1;
    Animator anim;
    public GameObject gameOver;
    public static bool gotAttack = false;
    public static bool death = false;

    public GameObject bloodEffect;

    //audio
    public AudioClip potionAudio;
    AudioSource audio;
    public AudioClip lvlUpAudio;
    public AudioClip die;
    public GameObject startEffect;

    public static bool backScene = false;
    private Vector3 lastPos;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        if (buttonScript.start)
        {
            potion = 5;
            level = 1;
            buttonScript.start = false;
        }
        if (buttonScript.cont)
        {
            LoadPlayer();
            maxHP = calculateMaxHealth(level) ;
            maxMana = calculateMaxMana(level) ;
            buttonScript.cont = false;
        }
        else
        {
            currMana = 0;
            maxHP = calculateMaxHealth(level);
            maxMana = calculateMaxMana(level);
            currMana = 0;
            currHP = maxHP;
        }
        attackDmg = calculateAttackDamage(level);
        changePortal1.Deactivate();
        anim = GetComponent<Animator>();
        manaBar.SetMaxMana(maxMana);
        manaBar.SetMana(currMana);
        healthBar.setMaxHealth(maxHP);
        healthBar.SetHealth(currHP);
        SetLevel(level);
        SetPotion(potion);
        gameOver.SetActive(false);
        if (backScene)
        {
            backScene = false;
            transform.position = lastPos;
        }
        Debug.Log(attackDmg + " " + maxHP + " " + maxMana);
        Instantiate(startEffect, transform.position, Quaternion.identity);
        //attackDmg = 100;
    }

    void Update()
    {
        if(currHP <= 0)
        {
            currHP = 0;
            death = true;
            Pause.stop = true;
            StartCoroutine(DeathRoutine());
        }
        healthBar.SetHealth(currHP);
        manaBar.SetMana(currMana);
        DrinkPotion();
        SetPotion(potion);
        //if (enemyAnim.GetBool("Attacking") == true && anim.GetBool("Impact") == false)
        //{
        //    Debug.Log("Kenaa");
        //    //StartCoroutine(TakeDamage());
        //}
        if (currMana >= maxMana)
        {
            level += 1;
            maxHP = calculateMaxHealth(level);
            maxMana = calculateMaxMana(level);
            attackDmg = calculateAttackDamage(level);
            currMana = 0;
            currHP = maxHP;
            healthBar.setMaxHealth(maxHP);
            healthBar.SetHealth(currHP);
            manaBar.SetMana(currMana);
            manaBar.SetMaxMana(maxMana);
            SetLevel(level);
            audio.PlayOneShot(lvlUpAudio);
        }

        if (gotAttack == true)
        {
            Debug.Log("gotAtt trueee");
            StartCoroutine(TakeDamage());
            gotAttack = false;
        }
    }

    IEnumerator DeathRoutine()
    {
        anim.SetInteger("Condition", 10);
        yield return new WaitForSeconds(0.8f);
        audio.PlayOneShot(die);
        yield return new WaitForSeconds(0.8f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOver.SetActive(true);
    }

    public void closeChangePortal1()
    {
        changePortal1.Deactivate();
    }

    public void DrinkPotion()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (anim.GetBool("Potion") == true || Pause.stop || death || potion < 1)
            {
                return;
            }
            if(anim.GetInteger("Condition") == 0 || anim.GetInteger("Condition") == 1)
            {
                StartCoroutine(DrinkRoutine());
            }
        }
    }

    IEnumerator DrinkRoutine()
    {
        anim.SetBool("Potion", true);
        potion -= 1;
        PlayDrinkAudio();
        yield return new WaitForSeconds(0.6f);
        currHP += 20;
        if(currHP > maxHP)
        {
            currHP = maxHP;
        }
        anim.SetBool("Potion", false);
    }
    
    public void PlayDrinkAudio()
    {
        audio.PlayOneShot(potionAudio);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag == "Portal")
    //    {
    //        Debug.Log("Portal");
    //        changePortal1.Activate();
    //    }

    //}

    IEnumerator TakeDamage()
    {
        anim.SetBool("Impact", true);
        //yield return new WaitForSeconds(0.5f);
        anim.SetInteger("Condition", 12);
        Debug.Log("gotImpact");
        Instantiate(bloodEffect, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
        healthBar.SetHealth(currHP);
        yield return new WaitForSeconds(0.5f);
        anim.SetInteger("Condition", 0);

        Debug.Log("Udah kena Impact");
        anim.SetBool("Impact", false);
    }


    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        currHP = data.health;
        currMana = data.mana;
        level = data.level;
        potion = data.potion;
        PaladinScript.gotWing = data.wing;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }

    public void SetLevel(int lvl)
    {
        levelTxt.text = lvl.ToString();
    }

    public void SetPotion(int ptn)
    {
        potionTxt.text = ptn.ToString();
    }

    public void savePostForPortal()
    {
        lastPos = transform.position;
        lastPos.x -= 3;
        backScene = true;
    }

    private int calculateMaxHealth(int level)
    {
        if(level == 1)
        {
            return 15;
        }
        return level * 15 + calculateMaxHealth(level - 1);
    }

    private int calculateMaxMana(int level)
    {
        if(level == 1)
        {
            return 16;
        }
        return level * 16 + calculateMaxMana(level - 1);
    }

    private int calculateAttackDamage(int level)
    {
        if(level == 1)
        {
            return 2;
        }
        return level * 2 + calculateAttackDamage(level - 1);
    }
}
