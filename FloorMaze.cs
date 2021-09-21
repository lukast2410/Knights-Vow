using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMaze : MonoBehaviour
{
    private float tileDistance = 5.818f;
    public GameObject floor;
    // Start is called before the first frame update
    void Start()
    {
        int width = ChangePortal1Script.widthDungeon1;
        int height = ChangePortal1Script.heightDungeon1;
        //int width = 42;
        //int height = 42;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject tempFloor = Instantiate(floor);
                tempFloor.transform.position = new Vector3(i * tileDistance, 0.005f, j * tileDistance);
                tempFloor.transform.parent = this.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
