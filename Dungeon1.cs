using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon1 : MonoBehaviour
{
    private int width = 6;
    private int height = 10;
    private int ratio;
    int[,] array;
    public GameObject normalFloor;
    public GameObject dropFloor;
    private float tileDistance = 4.8f;
    void Start()
    {
        Debug.Log("hello");
        width = ChangePortal1Script.widthDungeon1;
        ratio = ChangePortal1Script.ratioDungeon1;
        height = ChangePortal1Script.heightDungeon1;
        int x = height;
        int y = width;
        //ratio = 3;
        array = new int[x,y];

        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    array[i, j] = 1;
                }
            }
            for (int i = 0; i < y; i++)
            {
                array[0, i] = 0;
                array[x - 1, i] = 0;
            }

            int rand = 0;
            for (int i = 1; i < x - 1; i++)
            {
                if (i == 1)
                {
                    for (int j = 0; j < y / ratio; j++)
                    {
                        while (true)
                        {
                            rand = Random.Range(0, y - 1);
                            if (array[i, rand] != 0)
                            {
                                if (rand - 1 < 0)
                                {
                                    if (array[i, rand + 1] == 1)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else if (rand + 1 >= y)
                                {
                                    if (array[i, rand - 1] == 1)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else if (array[i, rand - 1] == 1 && array[i, rand + 1] == 1)
                                {
                                    break;
                                }
                            }
                        }
                        array[i, rand] = 0;
                    }
                }
                else if (i == x - 2)
                {
                    Debug.Log("mantul");
                    while (true)
                    {
                        rand = Random.Range(0, y - 1);
                        if (array[i - 1, rand] == 0)
                        {
                            break;
                        }
                    }
                    array[i, rand] = 0;
                }
                else
                {
                    for (int j = 0; j < y / ratio; j++)
                    {
                        while (true)
                        {
                            rand = Random.Range(0, y - 1);
                            if (array[i, rand] != 0)
                            {
                                if (rand - 1 < 0)
                                {
                                    if (array[i - 1, rand] == 0 || array[i, rand + 1] == 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else if (rand + 1 >= y)
                                {
                                    if (array[i - 1, rand] == 0 || array[i, rand - 1] == 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else if (array[i - 1, rand] == 0 || array[i, rand - 1] == 0 || array[i, rand + 1] == 0)
                                {
                                    break;
                                }
                            }
                        }
                        array[i, rand] = 0;
                    }
                }
            }

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (array[i, j] == 0)
                    {
                        GameObject tempFloor = Instantiate(normalFloor);
                        tempFloor.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance);
                    }
                    else if (array[i, j] == 1)
                    {
                        GameObject tempFloor = Instantiate(dropFloor);
                        tempFloor.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance);
                    }
                    Debug.Log("baris ke " + i + " kolom ke " + j + " isinya " + array[i, j]);
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
