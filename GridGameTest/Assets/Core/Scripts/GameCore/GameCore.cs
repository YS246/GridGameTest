using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : Singleton<GameCore>
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private GameUIManager _gameUIManager;
    [SerializeField] private ObstacleManager _obstacleManager;
    [SerializeField] private GameplayManager _gameplayManager;
    [SerializeField] private PathFinder _pathFinder;

    public GridManager gridManager => _gridManager;
    public GameUIManager gameUIManager => _gameUIManager;
    public ObstacleManager obstacleManager => _obstacleManager;
    public GameplayManager gameplayManager => _gameplayManager;
    public PathFinder pathFinder => _pathFinder;


    private void Start()
    {
        gridManager.Initialize();

        obstacleManager.SpawnObstacles();

        gameplayManager.StartGame();
    }
}
