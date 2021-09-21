using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePal : MonoBehaviour
{
    public static bool portal;
    public ChangePortal1Script portalScript;
    public GameObject playerCam;
    private bool eagle = false;

    // Start is called before the first frame update
    void Start()
    {
        playerCam.SetActive(true);
        portal = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (portal)
        {
            portal = false;
            portalScript.Activate();
        }
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
            playerCam.SetActive(false);
        }
        else
        {
            playerCam.SetActive(true);
        }
    }

}
