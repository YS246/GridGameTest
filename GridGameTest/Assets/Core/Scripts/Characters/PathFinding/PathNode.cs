using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathNode : IHeapItem<PathNode>
{
    public Vector2Int coordinate;

    public int gCost = int.MaxValue;
    public int hCost = int.MaxValue;
    public int fCost = int.MaxValue;

    public bool moveable = true;

    public bool hasFromNode = false;
    public Vector2Int fromCoordinate = new Vector2Int(-1, -1);

    //public PathNode fromNode = null;

    private int heapIndex;

    public PathNode()
    {

    }

    public PathNode(Vector2Int coor)
    {
        this.coordinate = coor;
    }

    int IHeapItem<PathNode>.HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public void ResetState()
    {
        gCost = int.MaxValue;
        hCost = int.MaxValue;
        fCost = int.MaxValue;

        //fromNode = null;
        fromCoordinate = new Vector2Int(-1, -1);
        hasFromNode = false;
    }

    public override string ToString()
    {
        return base.ToString() + " " + coordinate.ToString();
    }

    public int CompareTo(PathNode other)
    {
        int compareResult = fCost.CompareTo(other.fCost);

        if (compareResult == 0)
        {
            compareResult = hCost.CompareTo(other.hCost);
        }

        return compareResult;
    }
}