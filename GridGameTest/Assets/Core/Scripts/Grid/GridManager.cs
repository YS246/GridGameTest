using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridGenerator gridGenerator = null;

    private Dictionary<Vector2Int, Cell> cellLookup = null;

    private int totalCells;

    public void Initialize()
    {
        cellLookup = gridGenerator.GenerateGrid();

        totalCells = gridGenerator.totalCells;
    }

    public void SetCellAsObstacle(Vector2Int coordinate, GameObject obstaclePrefab)
    {
        if (cellLookup.TryGetValue(coordinate, out Cell cell))
        {
            cell.SetAsObstacle(obstaclePrefab);
        }
    }

    public List<Cell> GetAllCells()
    {
        return cellLookup.Values.ToList();
    }

    public int GetTotalCellCount()
    {
        return totalCells;
    }

    public Cell GetCell(Vector2Int coor)
    {
        if (cellLookup.ContainsKey(coor) == false)
        {
            return null;
        }

        return cellLookup[coor];
    }

    public void DisplayCellMoveableIndicator()
    {
        foreach (Cell cell in cellLookup.Values)
        {
            cell.ShowSurfaceIndicator();
        }
    }

    public void CancelDisplayCellMoveableIndicator()
    {
        foreach (Cell cell in cellLookup.Values)
        {
            cell.HideSurfaceIndicator();
        }
    }

    public List<Cell> GetRandomMoveableCells(int count)
    {
        List<Cell> selectedCells = new List<Cell>();

        int selectedCount = 0;

        List<Cell> shuffledList = cellLookup.Values.ToList().ShuffleCopy();

        foreach (Cell cell in shuffledList)
        {
            if (cell.moveable)
            {
                selectedCells.Add(cell);
                selectedCount++;
            }

            if (selectedCount >= count)
            {
                break;
            }
        }

        return selectedCells;
    }

    public List<Vector2Int> GetAdjacentCoordinates(Vector2Int coor)
    {
        List<Vector2Int> adjacents = new List<Vector2Int>();

        Vector2Int left = new Vector2Int(coor.x - 1, coor.y);
        Vector2Int right = new Vector2Int(coor.x + 1, coor.y);
        Vector2Int up = new Vector2Int(coor.x, coor.y + 1);
        Vector2Int down = new Vector2Int(coor.x, coor.y - 1);

        if (cellLookup.ContainsKey(left))
        {
            adjacents.Add(left);
        }

        if (cellLookup.ContainsKey(right))
        {
            adjacents.Add(right);
        }

        if (cellLookup.ContainsKey(up))
        {
            adjacents.Add(up);
        }

        if (cellLookup.ContainsKey(down))
        {
            adjacents.Add(down);
        }

        return adjacents;
    }
}