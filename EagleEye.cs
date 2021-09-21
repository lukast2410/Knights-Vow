using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleEye : MonoBehaviour
{
    public GameObject eagleCam;
    public GameObject roof;
    private bool eagle = false;
    // Start is called before the first frame update
    void Start()
    {
        roof.SetActive(true);
        eagleCam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        setEagle();
    }

    public void setEagle()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (!eagle)
            {
                eagle = true;
            }
            else
            {
                eagle = false;
            }
        }

        if (eagle)
        {
            roof.SetActive(false);
            eagleCam.SetActive(true);
        }
        else
        {
            roof.SetActive(true);
            eagleCam.SetActive(false);
        }
    }
}
