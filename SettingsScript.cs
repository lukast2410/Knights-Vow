using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsScript : MonoBehaviour
{
    Resolution[] resolutions;

    public Dropdown resolutionDropDown;
    public Image black;
    public GameObject blackTo;
    public GameObject menuCanvas;
    public GameObject paladin;

    void Start()
    {
        black.canvasRenderer.SetAlpha(1.0f);
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currOpt = 0;
        for(int i=0;i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currOpt = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currOpt;
        resolutionDropDown.RefreshShownValue();
    }

    public void SetResolution(int indexResolution)
    {
        Resolution resolution = resolutions[indexResolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void fadeOut()
    {
        StartCoroutine(fadeOutRoutine());
    }

    public void backToMenu()
    {
        paladin.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void fadeIn()
    {
        StartCoroutine(fadeInRoutine());
    }

    IEnumerator fadeInRoutine()
    {
        blackTo.SetActive(true);
        yield return new WaitForSeconds(2);
        black.canvasRenderer.SetAlpha(1.0f);
        black.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(2);
        blackTo.SetActive(false);
    }

    IEnumerator fadeOutRoutine()
    {
        black.canvasRenderer.SetAlpha(0f);
        blackTo.SetActive(true);
        black.CrossFadeAlpha(1, 2, false);
        yield return new WaitForSeconds(2);
        backToMenu();
    }
}
