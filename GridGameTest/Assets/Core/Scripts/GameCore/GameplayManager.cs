using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Enemy enemyPrefab;

    private GameState _state = GameState.None;
    public GameState state
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;

            GameStateChangedEvent?.Invoke(_state);
        }
    }

    private Player player;

    private Enemy enemy;

    public event Action<GameState> GameStateChangedEvent;

    public void StartGame()
    {
        List<Cell> cells = GameCore.instance.gridManager.GetRandomMoveableCells(2);

        if (cells.Count > 0)
        {
            SpawnPlayer(cells[0].coordinate);
        }

        if (cells.Count > 1)
        {
            SpawnEnemy(cells[1].coordinate);
        }

        state = GameState.PlayerTurn;
    }

    private void SpawnPlayer(Vector2Int spawnCoordinate)
    {
        player = Instantiate(playerPrefab);

        player.mover.EnterCell(spawnCoordinate);

        player.transform.position = GridHelper.GetCellPosition(spawnCoordinate);

        player.mover.MoveStartedEvent += OnPlayerMoveStarted;
        player.mover.MoveCompletedEvent += OnPlayerMoveCompleted;

        player.Initialize(this);
    }

    private void SpawnEnemy(Vector2Int spawnCoordinate)
    {
        enemy = Instantiate(enemyPrefab);

        enemy.mover.EnterCell(spawnCoordinate);

        enemy.transform.position = GridHelper.GetCellPosition(spawnCoordinate);

        enemy.mover.MoveStartedEvent += OnEnemyMoveStarted;
        enemy.mover.MoveCompletedEvent += OnEnemyMoveCompleted;

        enemy.Initialize(this);
    }

    public Vector2Int PlayerCoordinate()
    {
        return player.mover.currentCoordinate;
    }

    public void OnCellClicked(Vector2Int coordinate, bool moveable)
    {
        if (moveable == false)
        {
            return;
        }

        if (state == GameState.PlayerTurn)
        {
            if (player.isSelected)
            {
                player.MoveToCell(coordinate);
            }
        }
    }

    public void OnPlayerMoveStarted()
    {
        state = GameState.PlayerMoving;
    }

    public void OnPlayerMoveCompleted()
    {
        state = GameState.EnemyTurn;
    }

    public void OnEnemyMoveStarted()
    {
        state = GameState.EnemyMoving;
    }

    public void OnEnemyMoveCompleted()
    {
        state = GameState.PlayerTurn;
    }
}