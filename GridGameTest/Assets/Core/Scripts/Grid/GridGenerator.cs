using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject normalGroundPrefab = null;
    [SerializeField] private GameObject alterGroundPrefab = null;

    [SerializeField] private Transform mapParent = null;

    [SerializeField] private GameObject gridHolderPrefab = null;

    [SerializeField] private int width = 10;

    [SerializeField] private int height = 10;

    public int totalCells => width * height;

    public Dictionary<Vector2Int, Cell> GenerateGrid()
    {
        Dictionary<Vector2Int, Cell> cellLookup = new Dictionary<Vector2Int, Cell>();

        GameObject holder = Instantiate(gridHolderPrefab, mapParent);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject prefab = (i + j) % 2 == 0 ? normalGroundPrefab : alterGroundPrefab;

                GameObject groundObj = Instantiate(prefab, holder.transform);

                Vector2Int coordinate = GridHelper.GetCoordinate(i, j);

                groundObj.transform.localPosition = GridHelper.GetCellPosition(coordinate);

                groundObj.name = $"Cell_(X:{coordinate.x}, Y:{coordinate.y})";

                if (groundObj.TryGetComponent(out Cell cell))
                {
                    CellInfo cellInfo = new CellInfo(coordinate, CellType.Ground);

                    cell.SetInfo(cellInfo);

                    cellLookup.Add(coordinate, cell);
                }
            }
        }

        return cellLookup;
    }
}
