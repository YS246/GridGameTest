using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinder : MonoBehaviour
{
    private const int AdjacentNodeCost = 10;

    private Heap<PathNode> openNodes;
    private Heap<PathNode> closedNodes;

    private PathNode startNode;
    private PathNode endNode;

    private Dictionary<Vector2Int, PathNode> nodeLookup = null;

    private Dictionary<Vector2Int, List<PathNode>> adjacentNodeLookup = null;

    private void Initialize()
    {
        int totalCells = GameCore.instance.gridManager.GetTotalCellCount();

        openNodes = new Heap<PathNode>(totalCells);
        closedNodes = new Heap<PathNode>(totalCells);

        nodeLookup = new Dictionary<Vector2Int, PathNode>();

        foreach (Cell c in GameCore.instance.gridManager.GetAllCells())
        {
            PathNode node = new PathNode(c.coordinate);
            nodeLookup.Add(node.coordinate, node);
        }

        adjacentNodeLookup = new Dictionary<Vector2Int, List<PathNode>>();

        foreach (PathNode node in nodeLookup.Values)
        {
            adjacentNodeLookup.Add(node.coordinate, GetAdjacentNodes(node.coordinate));
        }
    }

    private List<PathNode> GetAdjacentNodes(Vector2Int coordinate)
    {
        List<Vector2Int> adjacentCoordinates = GameCore.instance.gridManager.GetAdjacentCoordinates(coordinate);

        List<PathNode> adjacents = adjacentCoordinates.Select((coor) => nodeLookup[coor]).ToList();

        return adjacents;
    }


    private void ResetAllNodes()
    {
        foreach (PathNode node in nodeLookup.Values)
        {
            node.ResetState();

            Cell c = GameCore.instance.gridManager.GetCell(node.coordinate);
            node.moveable = c.moveable;
        }
    }

    public List<Vector2Int> StartPathfinding(Vector2Int startCoor, Vector2Int endCoor, bool excludeStartCoor, bool excludeEndCoor)
    {
        if (nodeLookup == null)
        {
            Initialize();
        }

        ResetAllNodes();

        startNode = nodeLookup[startCoor];
        endNode = nodeLookup[endCoor];

        openNodes.Clear();
        closedNodes.Clear();

        openNodes.Add(startNode);

        startNode.gCost = 0;

        startNode.hCost = GridHelper.GetDistance(startNode.coordinate, endNode.coordinate) * AdjacentNodeCost;

        startNode.CalculateFCost();

        while (openNodes.Count > 0)
        {
            PathNode currentNode = openNodes.RemoveFirst();

            if (currentNode == endNode)
            {
                return CalculatePath(endNode, excludeStartCoor, excludeEndCoor);   //reached final node
            }

            closedNodes.Add(currentNode);


            foreach (PathNode adjNode in adjacentNodeLookup[currentNode.coordinate] /*currentNode.adjacentNodes*/)
            {
                if (closedNodes.Contains(adjNode))
                {
                    continue;
                }

                int newGCost = currentNode.gCost + AdjacentNodeCost;

                if (newGCost < adjNode.gCost)
                {
                    adjNode.gCost = newGCost;
                    adjNode.hCost = CalculateHCost(adjNode.coordinate, endNode.coordinate);
                    //adjNode.fromNode = currentNode;
                    adjNode.fromCoordinate = currentNode.coordinate;
                    adjNode.hasFromNode = true;

                    adjNode.CalculateFCost();

                    if (openNodes.Contains(adjNode))
                    {
                        openNodes.UpdateItem(adjNode);
                    }
                    else
                    {
                        if (adjNode != endNode)
                        {
                            if (adjNode.moveable == false)
                            {
                                continue;
                            }
                        }

                        openNodes.Add(adjNode);
                    }
                }
            }
        }

        //Debug.Log("No path found.");
        return null;
    }

    private int CalculateHCost(Vector2Int targetCoor, Vector2Int endCoor)
    {
        return GridHelper.GetDistance(targetCoor, endCoor) * AdjacentNodeCost;
    }

    private List<Vector2Int> CalculatePath(PathNode finalNode, bool excludeStart, bool excludeEnd)
    {
        List<Vector2Int> results = new List<Vector2Int>();

        PathNode currentNode = finalNode;

        while (currentNode != null)
        {
            results.Add(currentNode.coordinate);

            //currentNode = currentNode.fromNode;
            if (currentNode.hasFromNode)
            {
                currentNode = nodeLookup[currentNode.fromCoordinate];
            }
            else
            {
                currentNode = null;
            }
        }

        results.Reverse();

        if (excludeEnd && results.Count > 1)
        {
            results.RemoveAt(results.Count - 1);
        }

        if (excludeStart && results.Count > 1)
        {
            results.RemoveAt(0);
        }

        return results;
    }
}
