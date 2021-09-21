using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofMaze : MonoBehaviour
{
    
    private float tileDistance = 5.818f;
    public GameObject roof;
    public GameObject eagleCam;
    void Start()
    {
        int width = ChangePortal1Script.widthDungeon1;
        int height = ChangePortal1Script.heightDungeon1;
        //int width = 42;
        //int height = 42;
        for (int i = 0; i < height; i++){
            for(int j = 0; j < width; j++){
                GameObject tempRoof = Instantiate(roof);
                tempRoof.transform.position = new Vector3(i * tileDistance, 6.2f, j * tileDistance);
                tempRoof.transform.parent = this.transform;
            }
        }
        eagleCam.transform.position = new Vector3(height/2 * tileDistance, (height+width)*11/4, width/2 * tileDistance);
    }

    void Update()
    {
        
    }
}
