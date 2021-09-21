using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class RandomE
{
    public static E SelectRandom<E>(this System.Random random, HashSet<E> set)
    {
        return (new List<E>(set))[random.Next() % set.Count];
    }

}

public class CubeMaze : MonoBehaviour
{
    public float span = 0.09879102f;
    public int w = 12;
    public int h = 12;

    public int seed = 314;

    public GameObject PrimWall;
    public GameObject PrimLink;
    void Start()
    {
        var maze = new HashSet<PrimWall>();
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                maze.Add(new PrimWall(i, j));
            }
        }


        var start = new PrimWall(0, 0);

        var seen = new HashSet<PrimWall>();
        seen.Add(start);
        var todo = new HashSet<PrimWall>(seen);
        seed = Random.Range(0, 314);
        var random = new System.Random(seed);


        var PrimLinks = new List<PrimWall[]>();
        while (seen.Count < maze.Count)
        {
            var next = random.SelectRandom(todo);
            var neighbors = next.Neighbors;
            
            neighbors.RemoveWhere(e => seen.Contains(e));
            neighbors.RemoveWhere(e => !maze.Contains(e));

            if (0 == neighbors.Count)
            {
                todo.Remove(next);
                continue;
            }

            var PrimLink = random.SelectRandom(neighbors);

            PrimLinks.Add(new PrimWall[] { next, PrimLink });
            seen.Add(PrimLink);
            todo.Add(PrimLink);
        }

        foreach (var next in seen)
        {
            var floor = Instantiate(PrimWall) as GameObject;

            floor.transform.parent = transform;
            floor.transform.localPosition = next.ToVector3(span);
            floor.transform.localRotation = Quaternion.Euler(0, 0, 0);

            floor.name = next.ToString();
        }

        foreach (var next in PrimLinks)
        {
            var where = 0.5f * (next[0].ToVector3(span) + next[1].ToVector3(span));

            var bridge = Instantiate(PrimLink) as GameObject;

            bridge.transform.parent = transform;
            bridge.transform.localPosition = where;
            bridge.transform.localRotation = Quaternion.Euler(0, 0, 0);
            bridge.name = next[0] + "<=>" + next[1];
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
