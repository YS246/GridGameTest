using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public const string DefaultObstacleResourcesPath = "DefaultObstacleData";

    [SerializeField] private GameObject obstacleDisplayPrefab;

    private ObstacleData defaultObstacleData;

    public void SpawnObstacles()
    {
        defaultObstacleData = Resources.Load<ObstacleData>(DefaultObstacleResourcesPath);

        List<Vector2Int> obstacleCoordinates = defaultObstacleData.obstacleCoordinates;

        foreach (Vector2Int coordinate in obstacleCoordinates)
        {
            GameCore.instance.gridManager.SetCellAsObstacle(coordinate, obstacleDisplayPrefab);
        }
    }
}
