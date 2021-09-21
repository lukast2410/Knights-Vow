using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFloorDungeonOne : MonoBehaviour
{
    public Animator anim;
    public AudioSource audio;

    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Playerr")
        {
            anim.SetBool("Drop", true);
            StartCoroutine(DropCaroutine());
        }
    }

    IEnumerator DropCaroutine()
    {
        yield return new WaitForSeconds(1);
        audio.Play();
    }
    
}
