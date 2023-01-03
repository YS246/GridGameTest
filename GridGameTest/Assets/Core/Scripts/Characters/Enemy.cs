using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IBot
{
    [SerializeField] private GameObject selectedIndicator;
    [SerializeField] private CharacterMover _mover;

    public CharacterMover mover => _mover;

    public void Initialize(GameplayManager gameplayManager)
    {
        gameplayManager.GameStateChangedEvent += OnGameStateChanged;
    }

    public void BotAction()
    {
        MoveTowardPlayer();
    }

    private void MoveTowardPlayer()
    {
        Vector2Int playerCoor = GameCore.instance.gameplayManager.PlayerCoordinate();

        mover.MoveToCell(playerCoor, true, true);
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.EnemyTurn)
        {
            BotAction();
        }
    }
}
