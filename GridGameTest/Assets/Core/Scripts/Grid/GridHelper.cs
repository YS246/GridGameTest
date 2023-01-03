using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GridHelper
{
    public static Vector2Int GetCoordinate(int width, int height)
    {
        return new Vector2Int(height, width);
    }

    public static Vector3 GetCellPosition(Vector2Int coor)
    {
        return new Vector3(coor.y, 0, coor.x);
    }

    public static Vector2Int GetCoordinate(Vector3 position)
    {
        return new Vector2Int(Mathf.RoundToInt(position.z), Mathf.RoundToInt(position.x));
    }

    public static int GetDistance(Vector2Int origin, Vector2Int target)
    {
        return Mathf.Abs(origin.x - target.x) + Mathf.Abs(origin.y - target.y);
    }

    public static bool IsWithinDistance(Vector2Int origin, Vector2Int target, int checkDistance)
    {
        return IsWithinDistanceRange(origin, target, 0, checkDistance);
    }

    //minDistance and maxDistance are inclusive
    public static bool IsWithinDistanceRange(Vector2Int origin, Vector2Int target, int minDistance, int maxDistance)
    {
        int distance = GetDistance(origin, target);

        return distance >= minDistance && distance <= maxDistance;
    }

    public static int CompareCoordinate(Vector2Int coor1, Vector2Int coor2)
    {
        if (coor1.x < coor2.x)
        {
            return -1;
        }
        else if (coor1.x > coor2.x)
        {
            return 1;
        }
        else
        {
            return coor1.y.CompareTo(coor2.y);
        }
    }
}