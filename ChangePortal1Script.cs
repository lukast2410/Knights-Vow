using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangePortal1Script : MonoBehaviour
{
    public static int ratioDungeon1;
    public static int widthDungeon1;
    public static int heightDungeon1;
    public GameObject changePortal1;
    private int idx;
    public InputField ratioInput;
    public InputField widthInput;
    public InputField heightInput;
    private string ratioS;
    int ratio = 5;
    private int wid = 5;
    private int rat = 2;
    private int heg = 10;
    private int widM = 42;
    private int ratM = 3;
    private int hegM = 42;

    public void chaneScene(int x)
    {
        Debug.Log("Portal1");
        SceneManager.LoadScene(x);
    }

    public void Activate()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Pause.stop = true;
        PaladinScript.portal = true;
        changePortal1.SetActive(true);
    }

    public void Deactivate()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Pause.stop = false;
        PaladinScript.portal = false;
        changePortal1.SetActive(false);
    }

    void Start()
    {
        idx = 0;
        changePortal1.SetActive(false);
        wid = 5;
        rat = 2;
        heg = 10;
        widM = 42;
        ratM = 3;
        hegM = 42;
    }

    

    int ratioUpd = 2;
    // Update is called once per frame
    void Update()
    {
        //ratioUpd = setMinMaxRatio();
        //inputRatio.GetComponent<Text>().text = ratioUpd.ToString();
    }
   
    public void validateWidth(string width)
    {
        wid = int.Parse(width);
        Debug.Log(wid);
        if(wid < 5)
        {
            wid = 5;
        }else if(wid > 10)
        {
            wid = 10;
        }
        //Mathf.Clamp(wid, 5, 10);
        widthInput.text = wid.ToString();
        Debug.Log(wid);
    }

    public void validateRatio(string ratio)
    {
        rat = int.Parse(ratio);
        if(rat < 2)
        {
            rat = 2;
        }else if(rat > 5)
        {
            rat = 5;
        }
        //Mathf.Clamp(rat, 2, 5);
        ratioInput.text = rat.ToString();
    }

    public void validateHeight(string height)
    {
        heg = int.Parse(height);
        if(heg < 10)
        {
            heg = 10;
        }else if(heg > 15)
        {
            heg = 15;
        }
        //Mathf.Clamp(heg, 10, 15);
        heightInput.text = heg.ToString();
    }

    public void validateWidthMaze(string width)
    {
        widM = int.Parse(width);
        Debug.Log(widM);
        if (widM < 35)
        {
            widM = 35;
        }
        else if (widM > 50)
        {
            widM = 50;
        }
        //Mathf.Clamp(wid, 5, 10);
        widthInput.text = widM.ToString();
        Debug.Log(wid);
    }

    public void validateRatioMaze(string ratio)
    {
        ratM = int.Parse(ratio);
        if (ratM < 2)
        {
            ratM = 2;
        }
        else if (ratM > 5)
        {
            ratM = 5;
        }
        //Mathf.Clamp(rat, 2, 5);
        ratioInput.text = ratM.ToString();
    }

    public void validateHeightMaze(string height)
    {
        hegM = int.Parse(height);
        if (hegM < 35)
        {
            hegM = 35;
        }
        else if (hegM > 50)
        {
            hegM = 50;
        }
        //Mathf.Clamp(heg, 10, 15);
        heightInput.text = hegM.ToString();
    }

    public void nextPortal()
    {
        ratioDungeon1 = rat;
        widthDungeon1 = wid;
        heightDungeon1 = heg;
        rat = 2;
        wid = 5;
        heg = 10;
        changeSceneWithLoading(3);
    }

    public void nextMaze()
    {
        ratioDungeon1 = ratM;
        widthDungeon1 = widM;
        heightDungeon1 = hegM;
        ratM = 3;
        widM = 42;
        hegM = 42;
        changeSceneWithLoading(4);
    }

    public void changeSceneWithLoading(int x)
    {
        Debug.Log("StartGame");
        LoadingScene.nextScene = x;
        SceneManager.LoadScene(0);
    }

    

}
