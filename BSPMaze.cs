using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSPMaze : MonoBehaviour
{
	public GameObject wall;
	public GameObject player;
	public GameObject portal;
	public GameObject lamp;
	public GameObject enemy;
	private int[,] array;
    private float tileDistance = 5.818f;
	private int minWidth;
	private int minHeight;

	public void bsp(int x, int y, int w, int h)
	{
		int rendem = Random.Range(0,2);
		if (w < minWidth || h < minHeight)
		{
			return;
		}
		if (rendem == 0)
		{
			if ((minWidth + 1) >= (w - minWidth) || w < minWidth)
			{
				return;
			}
			int slice = 0;
			do
			{
				slice = Random.Range(0, (w - minWidth) - (minWidth + 1)) + minWidth;
			} while ((array[y,slice + x] == 0 && array[y,slice + x - 1] == 1) || (array[y + (h - 1),slice + x] == 0 && array[y + (h - 1),slice + x - 1] == 1));
			int door = 0;
			do
			{
				door = Random.Range(0, (h - 1));
			} while (door <= 0 || array[door - 1,slice + x] == 1 || array[door + 1,slice + x] == 1);

			for (int i = 0; i < h; i++)
			{
				array[y + i,slice + x] = 1;
				if (i == door)
				{
					array[y + i,slice + x] = 0;
				}
			}
			bsp(x, y, slice, h);
			bsp(slice + x, y, (w - slice), h);
		}
		else
		{
			if ((minHeight + 1) >= (h - minHeight) || h < minHeight)
			{
				return;
			}
			int slice = 0;
			do
			{
				slice = Random.Range(0, ((h - minHeight) - (minHeight + 1))) + minHeight;
			} while ((array[slice + y,x] == 0 && array[slice + y - 1,x] == 1) || (array[slice + y,x + (w - 1)] == 0 && array[slice + y - 1,x + (w - 1)] == 1));
			int door = 0;
			do
			{
				door = Random.Range(0, (w - 1));
			} while (door <= 0 || array[slice + y,door - 1] == 1 || array[slice + y,door + 1] == 1);
			for (int i = 0; i < w; i++)
			{
				array[slice + y,x + i] = 1;
				if (i == door)
				{
					array[slice + y,x + i] = 0;
				}
			}

			bsp(x, y, w, slice);
			bsp(x, slice + y, w, (h - slice));
		}
	}

	void Start()
    {
		int width = ChangePortal1Script.widthDungeon1;
		int height = ChangePortal1Script.heightDungeon1;
		int ratio = ChangePortal1Script.ratioDungeon1;
		//int width = 42;
		//int height = 42;
		//int ratio = 5;
		array = new int[width, height];
		minHeight = height / ratio;
		minWidth = width / ratio;
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				array[i,j] = 0;
				if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
				{
					array[i,j] = 1;
				}
			}
		}
		bsp(0, 0, width, height);
		Debug.Log("hei");
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				if(array[i,j] == 1)
				{
					if (i == 0)
					{
						GameObject tempWall = Instantiate(wall);
						tempWall.transform.position = new Vector3(i * tileDistance - (tileDistance / 2), 0, j * tileDistance - (tileDistance / 2));
						if(j != 0 && j != width-1 && array[i+1,j] == 1)
						{
							GameObject tempWall1 = Instantiate(wall, Vector3.zero, Quaternion.Euler(0, 90, 0));
							tempWall1.transform.position = new Vector3(i * tileDistance - (tileDistance / 2), 0, j * tileDistance);
						}
					}
					else if (i == height-1)
					{
						GameObject tempWall = Instantiate(wall);
						tempWall.transform.position = new Vector3(i * tileDistance + (tileDistance / 2), 0, j * tileDistance - (tileDistance / 2));
						if (j != 0 && j != width - 1 && array[i - 1, j] == 1)
						{
							GameObject tempWall1 = Instantiate(wall, Vector3.zero, Quaternion.Euler(0, 90, 0));
							tempWall1.transform.position = new Vector3(i * tileDistance - (tileDistance / 2), 0, j * tileDistance);
						}
					}
					if (j == 0)
					{
						GameObject tempWall = Instantiate(wall, Vector3.zero, Quaternion.Euler(0, 90, 0));
						tempWall.transform.position = new Vector3(i * tileDistance - (tileDistance / 2), 0, j * tileDistance - (tileDistance / 2));
						if (i != 0 && i != height - 1 && array[i, j + 1] == 1)
						{
							GameObject tempWall1 = Instantiate(wall);
							tempWall1.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance - (tileDistance / 2));
						}
					}
					else if (j == width-1)
					{
						GameObject tempWall = Instantiate(wall, Vector3.zero, Quaternion.Euler(0, 90, 0));
						tempWall.transform.position = new Vector3(i * tileDistance - (tileDistance / 2), 0, j * tileDistance + (tileDistance / 2));
						if (i != 0 && i != height - 1 && array[i, j-1] == 1)
						{
							GameObject tempWall1 = Instantiate(wall);
							tempWall1.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance - (tileDistance / 2));
						}
					}
					if(i > 0 && i < height - 1 && j > 0 && j < width - 1)
					{
						if(array[i, j - 1] == 1 && array[i, j + 1] == 1)
						{
							GameObject tempWall = Instantiate(wall);
							tempWall.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance - (tileDistance / 2));
							if (array[i - 1, j] == 1)
							{
								GameObject tempWall1 = Instantiate(wall, Vector3.zero, Quaternion.Euler(0, 90, 0));
								tempWall1.transform.position = new Vector3((i - 1) * tileDistance, 0, j * tileDistance);
							}
							else if(array[i + 1, j] == 1)
							{
								GameObject tempWall1 = Instantiate(wall, Vector3.zero, Quaternion.Euler(0, 90, 0));
								tempWall1.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance);
							}
						}
						else if(array[i - 1, j] == 1 && array[i + 1, j] == 1)
						{
							GameObject tempWall = Instantiate(wall, Vector3.zero, Quaternion.Euler(0, 90, 0));
							tempWall.transform.position = new Vector3(i * tileDistance - (tileDistance / 2), 0, j * tileDistance);
							if (array[i, j + 1] == 1)
							{
								GameObject tempWall1 = Instantiate(wall);
								tempWall1.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance);
							}
							else if (array[i, j - 1] == 1)
							{
								GameObject tempWall1 = Instantiate(wall);
								tempWall1.transform.position = new Vector3(i * tileDistance, 0, (j-1) * tileDistance);
							}
						}
						else if (array[i, j - 1] == 1 && array[i + 1, j] == 1)
						{
							GameObject tempWall1 = Instantiate(wall);
							tempWall1.transform.position = new Vector3(i * tileDistance, 0, (j - 1) * tileDistance);
							GameObject tempWall = Instantiate(wall, Vector3.zero, Quaternion.Euler(0, 90, 0));
							tempWall.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance);
						}
						else if (array[i, j - 1] == 1 && array[i - 1, j] == 1)
						{
							GameObject tempWall1 = Instantiate(wall);
							tempWall1.transform.position = new Vector3(i * tileDistance, 0, (j - 1) * tileDistance);
							GameObject tempWall = Instantiate(wall, Vector3.zero, Quaternion.Euler(0, 90, 0));
							tempWall.transform.position = new Vector3((i - 1) * tileDistance, 0, j * tileDistance);
						}
						else if (array[i, j + 1] == 1 && array[i + 1, j] == 1)
						{
							GameObject tempWall1 = Instantiate(wall);
							tempWall1.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance);
							GameObject tempWall = Instantiate(wall, Vector3.zero, Quaternion.Euler(0, 90, 0));
							tempWall.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance);
						}
						else if (array[i, j + 1] == 1 && array[i - 1, j] == 1)
						{
							GameObject tempWall1 = Instantiate(wall);
							tempWall1.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance);
							GameObject tempWall = Instantiate(wall, Vector3.zero, Quaternion.Euler(0, 90, 0));
							tempWall.transform.position = new Vector3((i - 1) * tileDistance, 0, j * tileDistance);
						}
					}

				}
				//if (i == minHeight / 2 && j == 2)
				//{
				//	Debug.Log("playerr");
				//	player.transform.position = new Vector3(i * tileDistance, 0.3f, j * tileDistance);
				//}
				if (i % 3 == 0 && j % 3 == 0 && array[i,j] != 1 && array[i,j-1] != 1 && array[i, j + 1] != 1 && array[i+1, j] != 1 && array[i-1, j] != 1)
				{
					GameObject tempLamp = Instantiate(lamp);
					tempLamp.transform.position = new Vector3(i * tileDistance, 3.8f, j * tileDistance);
				}
			}
		}

		int rendem = Random.Range(0, 2);
		if(rendem == 0)
		{
			int w = 0;
			int h = 0;
			for(int i = width - 2; i > -1; i--)
			{
				if(array[height-2, i] == 1)
				{
					w = width - 1 - i;
					break;
				}
			}
			for (int i = height - 2; i > -1; i--)
			{
				if (array[i, width-2] == 1)
				{
					h = height - 1 - i;
					break;
				}
			}
			h /= 2;
			w /= 2;
			GameObject tempPortal = Instantiate(portal);
			tempPortal.transform.position = new Vector3((height-h-1) * tileDistance, 0.4f, (width-w-1) * tileDistance);
		}
		else
		{
			int w = 0;
			int h = 0;
			for (int i = 1; i < width; i++)
			{
				if (array[height - 2, i] == 1)
				{
					w = width - 1 - i;
					break;
				}
			}
			for (int i = height - 2; i > -1; i--)
			{
				if (array[i, width - 2] == 1)
				{
					h = height - 1 - i;
					break;
				}
			}
			h /= 2;
			w /= 2;
			GameObject tempPortal = Instantiate(portal);
			tempPortal.transform.position = new Vector3((height - h - 1) * tileDistance, 0.4f, (w+1) * tileDistance);
		}
		int morakCount = (width + height) / (ratio * 3 / 2);
		for(int i = 0; i < morakCount; i++)
		{
			int x = 0;
			int y = 0;
			do
			{
				x = Random.Range(minHeight / 2, height - (minHeight / 2));
				y = Random.Range(minWidth / 2, width - (minWidth / 2));
			} while (array[x, y] == 1 || array[x, y - 1] == 1 || array[x, y + 1] == 1 || array[x + 1, y] == 1 || array[x - 1, y] == 1);
			Vector3 position = new Vector3(x * tileDistance, 0.1f, y * tileDistance);
			GameObject tempEnemy = Instantiate(enemy, position, Quaternion.identity);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
