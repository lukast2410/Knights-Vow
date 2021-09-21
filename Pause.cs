using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pause;
    private int pauseIndex;
    public static bool stop = false;

    public void chaneScene(int x)
    {
        Debug.Log("Pause");
        LoadingScene.nextScene = x;
        SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        stop = false;
        pauseIndex = 0;
        pause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseIndex++;
            if(pauseIndex == 1)
            {
                stop = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None; 
                pause.SetActive(true);
            }
            else
            {
                stop = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                pause.SetActive(false);
                pauseIndex = 0;
            }
        }
    }
}
