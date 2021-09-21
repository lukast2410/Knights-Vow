using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimWall
{
    public readonly int i;
    public readonly int j;

    public PrimWall(int i, int j)
    {
        this.i = i;
        this.j = j;
    }

    // override object.Equals
    public override bool Equals(object obj)
    {
        var other = obj as PrimWall;
        return null != other && i == other.i && j == other.j;
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public override string ToString()
    {
        return "Cell id(" + i + "," + j + ")";
    }

    public HashSet<PrimWall> Neighbors
    {
        get
        {
            return new HashSet<PrimWall>(new PrimWall[]
            {
                new PrimWall(i + 1, j),
                new PrimWall(i - 1, j),
                new PrimWall(i, j + 1),
                new PrimWall(i, j - 1),
            });
        }
    }

    public Vector3 ToVector3(float span)
    {
        return new Vector3(i * span, 0, j * span);
    }
}
