using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ObstacleEditor : EditorWindow
{
    private bool[,] obstacleArray = new bool[0, 0];

    private int width = 10;
    private int height = 10;

    private bool invertVertical = true;

    [MenuItem("Custom/ObstacleEditor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ObstacleEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("Obstacles On Grid", EditorStyles.boldLabel);

        DrawToggleArray();

        if (GUILayout.Button("Apply"))
        {
            // applies array 
            ApplyToObstacleData();
        }

        if (GUILayout.Button("Load"))
        {
            // loads array 
            LoadObstacleData();
        }
    }

    private void ApplyToObstacleData()
    {
        if (GameCore.instance != null)
        {
            ObstacleData obstacleData = Resources.Load<ObstacleData>(ObstacleManager.DefaultObstacleResourcesPath);

            obstacleData.ClearObstacle();

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    bool hasObstacle = obstacleArray[j, i];

                    if (hasObstacle)
                    {
                        Vector2Int coor = GridHelper.GetCoordinate(i, j);

                        obstacleData.AddObstacle(coor);
                    }
                }
            }

            EditorUtility.SetDirty(obstacleData);
        }
    }

    private void LoadObstacleData()
    {
        if (GameCore.instance != null)
        {
            ObstacleData obstacleData = Resources.Load<ObstacleData>(ObstacleManager.DefaultObstacleResourcesPath);

            obstacleArray = new bool[height, width];

            foreach (Vector2Int coor in obstacleData.obstacleCoordinates)
            {
                obstacleArray[coor.x, coor.y] = true;
            }
        }
    }

    private void DrawToggleArray()
    {
        if (obstacleArray.GetLength(0) != width || obstacleArray.GetLength(1) != height)
        {
            return;
        }

        for (int j = 0; j < height; j++)
        {
            int heightIndex = invertVertical ? height - 1 - j : j;

            EditorGUILayout.BeginHorizontal();

            for (int i = 0; i < width; i++)
            {
                obstacleArray[heightIndex, i] = EditorGUILayout.Toggle(obstacleArray[heightIndex, i]);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

}