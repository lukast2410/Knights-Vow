using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapPal : MonoBehaviour
{
    public static bool portal;
    public static Vector3 startPalPostInTrapDungeon = Vector3.zero;
    public ChangePortal1Script portalScript;
    private int deathCount = 0;
    private bool death;
    public Text deathText;
    public GameObject youDeath;
    private int palMaxHealth;
    public GameObject startEffect;
    public AudioClip startAudio;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        portal = false;
        audio = GetComponent<AudioSource>();
        death = false;
        deathCount = 0;
        deathText.text = deathCount.ToString();
        youDeath.SetActive(false);
        palMaxHealth = calculateMaxHealth(PaladinLifeManager.level);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (portal)
        {
            portal = false;
            portalScript.Activate();
        }
        if (!death)
        {
            if (transform.position.y < -12)
            {
                StartCoroutine(DeathRoutine());
            }
        }
    }

    IEnumerator DeathRoutine()
    {
        deathCount++;
        youDeath.SetActive(true);
        death = true;
        yield return new WaitForSeconds(1f);
        PaladinLifeManager.currHP -= (palMaxHealth / 10);
        youDeath.SetActive(false);
        death = false;
        deathText.text = deathCount.ToString();
        transform.position = startPalPostInTrapDungeon;
        Instantiate(startEffect, transform.position, Quaternion.identity);
        audio.PlayOneShot(startAudio);
    }

    private int calculateMaxHealth(int level)
    {
        if (level == 1)
        {
            return 15;
        }
        return level * 15 + calculateMaxHealth(level - 1);
    }
}
