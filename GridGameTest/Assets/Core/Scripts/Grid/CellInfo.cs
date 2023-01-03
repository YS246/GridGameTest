using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct CellInfo
{
    public Vector2Int coordinate;
    public CellType cellType;

    public CellInfo(Vector2Int coordinate, CellType cellType)
    {
        this.coordinate = coordinate;
        this.cellType = cellType;
    }
}