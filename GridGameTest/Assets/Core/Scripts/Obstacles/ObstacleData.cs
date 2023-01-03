using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Grid/ObstacleData")]
public class ObstacleData : ScriptableObject
{
    [SerializeField] private List<Vector2Int> _obstacleCoordinates;
    public List<Vector2Int> obstacleCoordinates => _obstacleCoordinates;

#if UNITY_EDITOR
    public void AddObstacle(Vector2Int coordinate)
    {
        if (_obstacleCoordinates.Contains(coordinate) == false)
        {
            _obstacleCoordinates.Add(coordinate);
        }
    }

    public void ClearObstacle()
    {
        _obstacleCoordinates.Clear();
    }
#endif
}