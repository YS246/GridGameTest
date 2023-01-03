using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Cell : MonoBehaviour, IGamePointerEnterHandler, IGamePointerExitHandler, IGamePointerDownHandler
{
    public enum CellState
    {
        Empty,
        Full
    }

    [SerializeField] private Transform obstacleParent;
    [SerializeField] private GameObject moveableIndicator;
    [SerializeField] private GameObject unmoveableIndicator;

    private CellInfo info;

    private CellState _state = CellState.Empty;

    public CellState state => _state;

    public Vector2Int coordinate => info.coordinate;

    public bool moveable => state == CellState.Empty;

    public void ShowSurfaceIndicator()
    {
        moveableIndicator.SetActive(moveable);
        unmoveableIndicator.SetActive(!moveable);
    }

    public void HideSurfaceIndicator()
    {
        moveableIndicator.SetActive(false);
        unmoveableIndicator.SetActive(false);
    }

    public void SetInfo(CellInfo info)
    {
        this.info = info;
    }

    public void OnGamePointerEnter()
    {
        GameCore.instance.gameUIManager.UpdateCellInfo(info);
    }

    public void OnGamePointerExit()
    {
        GameCore.instance.gameUIManager.CancelCellInfo();
    }

    public void OnGamePointerDown()
    {
        GameCore.instance.gameplayManager.OnCellClicked(coordinate, moveable);
    }

    public void SetAsObstacle(GameObject obstaclePrefab)
    {
        info.cellType = CellType.HasObstacle;

        Instantiate(obstaclePrefab, obstacleParent);

        _state = CellState.Full;
    }

    public void OnCharacterEnter()
    {
        _state = CellState.Full;
    }

    public void OnCharacterLeft()
    {
        _state = CellState.Empty;
    }
}