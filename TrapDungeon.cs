using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDungeon : MonoBehaviour
{
    public GameObject normalFloor;
    public GameObject dropFloor;
    public GameObject lamp;
    public GameObject player;
    public GameObject wall;
    public GameObject portal;
    public GameObject door;
    List<Coord> allCoord;
    Queue<Coord> shuffleCoord;
    Coord source;
    private float tileDistance = 5.818f;

    // Start is called before the first frame update
    void Start()
    {
        GenerateTrap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateTrap()
    {
        int heightAsli = ChangePortal1Script.heightDungeon1 + 2;
        int height = heightAsli - 3;
        int width = ChangePortal1Script.widthDungeon1;
        int rasio = ChangePortal1Script.ratioDungeon1;
        //int height = 12;
        //int width = 7;
        //int rasio = 2;
        allCoord = new List<Coord>();
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                allCoord.Add(new Coord(x, y)); 
            }
        }
        int seed = Random.Range(1,50);
        shuffleCoord = new Queue<Coord>(Utility.ShuffleArray(allCoord.ToArray(), seed));

        bool[,] trapMap = new bool[width, height];
        int obstacle = width * height / rasio;
        Debug.Log("rasio -> " + rasio + " obs -> " + obstacle);
        int currObs = 0;
        Debug.Log("0 -> " + trapMap.GetLength(0));
        Debug.Log("1 -> " + trapMap.GetLength(1));

        //for (int i = 0; i < width; i++)
        //{
        //    trapMap[i, height - 2] = true;
        //}

        source = new Coord(Random.Range(0, width-1), Random.Range(1, height - 3));
        Debug.Log("source x -> " + source.x + " source y -> " + source.y);

        for (int i = 0; i < obstacle; i++)
        {
            Coord randomCoord = GetRandomCoord();
            Debug.Log("ke " + i + " randomX " + randomCoord.x + "randomY " + randomCoord.y);
            trapMap[randomCoord.x, randomCoord.y] = true;
            currObs++;

            if ((randomCoord.x != source.x || randomCoord.y != source.y) && checkFloodFill(trapMap, currObs)) 
            {
                Debug.Log("dropFloor");
                trapMap[randomCoord.x, randomCoord.y] = true;
            }
            else
            {
                trapMap[randomCoord.x, randomCoord.y] = false;
                obstacle++;
                currObs--;
            }
        }

        int randomForBeforeLastLine = 0;
        do
        {
            randomForBeforeLastLine = Random.Range(0, width-1);
        } while (trapMap[randomForBeforeLastLine, height-1]);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height + 3; j++)
            {
                if (i == 0 )
                {
                    GameObject tempWall = Instantiate(wall);
                    tempWall.transform.position = new Vector3(i * tileDistance - (tileDistance / 2), 0, j * tileDistance - (tileDistance / 2));
                }else if(i == width - 1)
                {
                    GameObject tempWall = Instantiate(wall);
                    tempWall.transform.position = new Vector3(i * tileDistance + (tileDistance / 2), 0, j * tileDistance - (tileDistance / 2));
                }
                if(j == 0)
                {
                    GameObject tempWall = Instantiate(wall, Vector3.zero, Quaternion.Euler(0, 90, 0));
                    tempWall.transform.position = new Vector3(i * tileDistance - (tileDistance / 2), 0, j * tileDistance - (tileDistance / 2));
                }else if(j == height + 2)
                {
                    GameObject tempWall = Instantiate(wall, Vector3.zero, Quaternion.Euler(0, 90, 0));
                    tempWall.transform.position = new Vector3(i * tileDistance - (tileDistance / 2), 0, j * tileDistance + (tileDistance / 2));
                }
                if(j>0 && j < height + 1)
                {
                    if (trapMap[i,j-1] == false)
                    {
                        GameObject tempFloor = Instantiate(normalFloor);
                        tempFloor.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance);
                    }
                    else if (trapMap[i, j-1] == true)
                    {
                        GameObject tempFloor = Instantiate(dropFloor);
                        tempFloor.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance);
                    }
                }
                else
                {
                    if(j == height + 1)
                    {
                        if(i == randomForBeforeLastLine)
                        {
                            GameObject tempFloor = Instantiate(normalFloor);
                            tempFloor.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance);
                        }
                        else
                        {
                            GameObject tempFloor = Instantiate(dropFloor);
                            tempFloor.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance);
                        }
                    }
                    else
                    {
                        GameObject tempFloor = Instantiate(normalFloor);
                        tempFloor.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance);
                        if(j == 0)
                        {
                            if(i == width / 2)
                            {
                                GameObject tempPlayer = Instantiate(player);
                                tempPlayer.transform.position = new Vector3(i * tileDistance, 0.3f, j * tileDistance);
                                TrapPal.startPalPostInTrapDungeon = tempPlayer.transform.position;
                                GameObject tempDoor = Instantiate(door, Vector3.zero, Quaternion.Euler(0, 90, 0));
                                tempDoor.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance - (tileDistance / 2.33f));
                            }
                        }else if(j == height + 2)
                        {
                            if (i == width / 2)
                            {
                                GameObject tempPortal = Instantiate(portal);
                                tempPortal.transform.position = new Vector3(i * tileDistance, 0.2f, j * tileDistance);
                            }
                        }
                    }
                }
                GameObject tempRoof = Instantiate(normalFloor);
                tempRoof.transform.position = new Vector3(i * tileDistance, 6.2f, j * tileDistance);
                //Debug.Log("baris ke " + i + " kolom ke " + j + " isinya " + array[i, j]);
                if(i!=0 && i!=width-1 && i%2 == 0 && j%2 == 0)
                {
                    GameObject tempLamp = Instantiate(lamp);
                    tempLamp.transform.position = new Vector3(i * tileDistance, 3.7f, j * tileDistance);
                }
            }
        }
    }

    bool checkFloodFill(bool[,] trapMap, int currObs)
    {
        bool[,] mapFlags = new bool[trapMap.GetLength(0), trapMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(source);
        mapFlags[source.x, source.y] = true;

        int canBeAccess = 1;
        while(queue.Count > 0)
        {
            Coord tile = queue.Dequeue();
            for(int x=-1; x<=1; x++){
                for(int y=-1; y<=1; y++){
                    int checkX = tile.x + x;
                    int checkY = tile.y + y;
                    if(x == 0 || y == 0){
                        if(checkX >= 0 && checkY >= 0 && checkX < trapMap.GetLength(0) && checkY < trapMap.GetLength(1)){
                            if(!mapFlags[checkX, checkY] && !trapMap[checkX, checkY]){
                                mapFlags[checkX, checkY] = true;
                                queue.Enqueue(new Coord(checkX, checkY));
                                canBeAccess++;
                            }
                        }
                    }
                }
            }
        }

        int accessible = trapMap.GetLength(0) * trapMap.GetLength(1) - currObs;
        Debug.Log("saat currobs -> " + currObs + " access " + accessible + " can be " + canBeAccess);
        return accessible == canBeAccess;
    }

    public Coord GetRandomCoord()
    {
        Coord randCoord = shuffleCoord.Dequeue();
        shuffleCoord.Enqueue(randCoord);
        return randCoord;
    }

    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int xx, int yy)
        {
            x = xx;
            y = yy;
        }
    }
}
