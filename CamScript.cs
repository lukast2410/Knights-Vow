using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    private float rotateSpeed = 1;
    public Transform target, paladin;
    private float mouseX, mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Pause.stop == false)
        {
            CamControl();
        }
    }

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotateSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotateSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(target);

        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) ||
            Input.GetMouseButtonDown(0))
        {
            paladin.rotation = Quaternion.Euler(0, mouseX, 0);
        }
    }
}
