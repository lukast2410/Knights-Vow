using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public Slider loadBar1;
    public Slider loadBar2;
    public Text loadText;
    public Text mottoText;
    public static int nextScene = 1;
    string[] motto = new string[4];
    // Start is called before the first frame update
    void Start()
    {
        motto[0] = "Trough Hard Work and Dedication, We Hold Our Future in Our Hands - Bluejack 20-1";
        motto[1] = "Always Try New Things, Overcome All Problems - Bluejack 19-1";
        motto[2] = "Always Strive to Learn, Achieve, and Believe - Bluejack 19-1";
        int randNum = Random.Range(0, 3);
        mottoText.text = motto[randNum];
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation gameOne = SceneManager.LoadSceneAsync(nextScene);
        float progress;

        while(gameOne.progress < 1)
        {
            progress = gameOne.progress;
            Debug.Log(progress);
            loadBar1.value = progress;
            loadBar2.value = progress;
            loadText.text = (progress * 100).ToString() + " %";
            yield return new WaitForEndOfFrame();
        }
    }

}
