using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScript : MonoBehaviour
{
    public float radius;
    private float radiusInteract = 5;
    public PaladinLifeManager playerLife;
    public Transform player;
    public GameObject main;
    public GameObject saveCam;
    public GameObject saveCanvas;
    public GameObject imageBlack;
    public Image black;
    private bool openCanvas = false;
    public GameObject textInteract;

    // Start is called before the first frame update
    void Start()
    {
        saveCanvas.SetActive(false);
        main.SetActive(true);
        saveCam.SetActive(false);
        imageBlack.SetActive(false);
        textInteract.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance <= radiusInteract)
        {
            textInteract.SetActive(true);
        }
        else
        {
            textInteract.SetActive(false);
        }

        if(distance <= radius)
        {
            if (!openCanvas)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PaladinScript.portal = true;
                    StartCoroutine(SaveRoutine());
                }
            }
        }
        
    }

    IEnumerator SaveRoutine()
    {
        black.canvasRenderer.SetAlpha(0);
        imageBlack.SetActive(true);
        black.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(1);
        openSaveCanvas();
        black.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(1);
        imageBlack.SetActive(false);
    }

    public void openSaveCanvas()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        main.SetActive(false);
        saveCam.SetActive(true);
        saveCanvas.SetActive(true);
        openCanvas = true;
        Pause.stop = true;
        Debug.Log("Save");
    }

    public void closeSaveCanvas()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        main.SetActive(true);
        saveCam.SetActive(false);
        saveCanvas.SetActive(false);
        openCanvas = false;
        PaladinScript.portal = false;
        Debug.Log("Gajadi Save");
        Pause.stop = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(playerLife);
        closeSaveCanvas();
        Debug.Log("di Save");
    }

}
