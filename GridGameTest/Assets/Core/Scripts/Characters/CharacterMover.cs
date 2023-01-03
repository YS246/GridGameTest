using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using DG.Tweening;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float stepDuration = 0.25f;

    private Vector2Int _currentCoordinate;
    public Vector2Int currentCoordinate => _currentCoordinate;

    public event Action MoveStartedEvent;
    public event Action MoveCompletedEvent;

    public void EnterCell(Vector2Int coordinate)
    {
        _currentCoordinate = coordinate;

        Cell currentCell = GameCore.instance.gridManager.GetCell(currentCoordinate);

        currentCell.OnCharacterEnter();
    }

    private void LeaveCurrentCell()
    {
        Cell currentCell = GameCore.instance.gridManager.GetCell(currentCoordinate);

        currentCell.OnCharacterLeft();
    }

    public void MoveToCell(Vector2Int destination, bool excludeStart, bool excludeEnd)
    {
        List<Vector2Int> path = GameCore.instance.pathFinder.StartPathfinding(currentCoordinate, destination, excludeStart, excludeEnd);

        if (path == null || path.Count == 0)
        {
            return;
        }

        LeaveCurrentCell();

        MoveStartedEvent?.Invoke();

        StartMove(path);

        _currentCoordinate = path[path.Count - 1];

        EnterCell(path[path.Count - 1]);
    }

    public void StartMove(List<Vector2Int> path)
    {
        StartCoroutine(MoveRoutine(path));
    }

    public IEnumerator MoveRoutine(List<Vector2Int> path)
    {
        Vector3 targetPosition;

        foreach (Vector2Int coor in path)
        {
            targetPosition = GridHelper.GetCellPosition(coor);

            transform.DOMove(targetPosition, stepDuration);

            yield return YieldHelper.WaitForSeconds(stepDuration);
        }

        MoveCompletedEvent?.Invoke();
    }

}
