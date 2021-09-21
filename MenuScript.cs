using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Image black;
    public GameObject setting;
    public GameObject blackToo;
    public GameObject menuCanvas;
    public GameObject paladin;
    // Start is called before the first frame update
    void Start()
    {
        paladin.SetActive(false);
        black.canvasRenderer.SetAlpha(1.0f);
        fadeIn();
    }

    // Update is called once per frame
    public void fadeIn()
    {
        blackToo.SetActive(true);
        black.canvasRenderer.SetAlpha(1.0f);
        black.CrossFadeAlpha(0, 2, false);
        StartCoroutine(fade());
    }

    public void fadeOut()
    {
        StartCoroutine(fadeOutRoutine());
    }

    IEnumerator fadeOutRoutine()
    {
        black.canvasRenderer.SetAlpha(0f);
        blackToo.SetActive(true);
        black.CrossFadeAlpha(1, 2, false);
        yield return new WaitForSeconds(2);
        settings();
    }

    public void fromSettings()
    {
        StartCoroutine(fadeInFromSettings());
    }

    IEnumerator fadeInFromSettings()
    {
        yield return new WaitForSeconds(2);
        blackToo.SetActive(true);
        black.canvasRenderer.SetAlpha(1.0f);
        black.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(2);
        blackToo.SetActive(false);
    }

    IEnumerator fade()
    {
        yield return new WaitForSeconds(2);
        blackToo.SetActive(false);
    }

    public void settings()
    {
        paladin.SetActive(true);
        menuCanvas.SetActive(false);
        setting.SetActive(true);
    }

}
