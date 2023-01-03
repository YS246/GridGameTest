using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IGamePointerSelectable
{
    [SerializeField] private GameObject selectedIndicator;
    [SerializeField] private CharacterMover _mover;

    public CharacterMover mover => _mover;

    private bool _isSelected;
    public bool isSelected => _isSelected;

    private bool canSelect;

    public void Initialize(GameplayManager gameplayManager)
    {
        gameplayManager.GameStateChangedEvent += OnGameStateChanged;
    }

    public void OnSelect()
    {
        if (canSelect == false)
        {
            return;
        }

        _isSelected = true;

        selectedIndicator.SetActive(true);

        GameCore.instance.gridManager.DisplayCellMoveableIndicator();
    }

    public void OnDeselect()
    {
        _isSelected = false;

        selectedIndicator.SetActive(false);

        GameCore.instance.gridManager.CancelDisplayCellMoveableIndicator();
    }

    public void MoveToCell(Vector2Int destination)
    {
        mover.MoveToCell(destination, true, false);
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.PlayerTurn)
        {
            canSelect = true;
        }
        else
        {
            canSelect = false;
        }
    }
}
