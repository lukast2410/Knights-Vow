using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonScript : MonoBehaviour
{
    public static bool cont = false;
    public static bool start = false;
    public Button load;

    public void chaneScene(int x)
    {
        Debug.Log("StartGame");
        SceneManager.LoadScene(x);
    }

    public void changeSceneWithLoading(int x)
    {
        Debug.Log("StartGame");
        LoadingScene.nextScene = x;
        SceneManager.LoadScene(0);
    }

    public void startGame()
    {
        Debug.Log("StartGame");
        start = true;
        LoadingScene.nextScene = 2;
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Debug.Log("quitt");
        Application.Quit();
    }

    void Start()
    {
        string path = Application.persistentDataPath + "/player.lt";
        if (File.Exists(path))
        {
            load.interactable = true;
        }
        else
        {
            load.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void continueGame()
    {
        cont = true;
        changeSceneWithLoading(2);
    }
}
