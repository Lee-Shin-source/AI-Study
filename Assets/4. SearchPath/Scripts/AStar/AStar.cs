using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace _4._SearchPath.Scripts.AStar
{
    public class AStar : MonoBehaviour
    {
        public static PriorityQueue closedList, openList;

        Node node = null;
        private static float HeuristicEstimateCost(Node curNode, Node goalNode)
        {
            var vecCost = curNode.position - goalNode.position;
            return vecCost.sqrMagnitude;
        }


        public IEnumerator FindPath(Node start, Node goal, ArrayList path)
        {
            openList = new PriorityQueue();
            openList.Push(start);
            start.nodeTotalCost = 0.0f;
            start.estimatedCost = HeuristicEstimateCost(start, goal);

            closedList = new PriorityQueue();
            yield return StartCoroutine(Run(goal));

            if (node.position != goal.position)
            {
                Debug.LogError("Goal Not Found");
                yield break;;
            }

            path = CalculatePath(node);
        }

        private IEnumerator Run(Node goal)
        {
            while (openList.Length != 0)
            {
                node = openList.First();
                if (node.position == goal.position)
                {
                    yield break;
                }
                ArrayList neighbours = new ArrayList();
                
                GridManager.instance.GetNeighbours(node,neighbours);
                
                
                for (int i = 0; i < neighbours.Count; i++)
                {
                    var neighbourNode = (Node)neighbours[i];
                    if (!closedList.Contains(neighbourNode))
                    {
                        var cost = HeuristicEstimateCost(node, neighbourNode);

                        var totalCost = node.nodeTotalCost + cost;
                        var neighbourNodeEstCost = HeuristicEstimateCost(neighbourNode, goal);

                        neighbourNode.nodeTotalCost = totalCost;
                        neighbourNode.parent = node;
                        neighbourNode.estimatedCost = totalCost + neighbourNodeEstCost;
                        Debug.Log($"nodeTotalCost : {totalCost} estimatedCost : {neighbourNode.estimatedCost}");
                        if (!openList.Contains(neighbourNode))
                        {
                            openList.Push(neighbourNode);
                        }
                    }

                    // yield return new WaitForSeconds(0.2f);
                }
                
                closedList.Push(node);
                openList.Remove(node);
            }
            yield break;
        }

        private static ArrayList CalculatePath(Node node)
        {
            var list = new ArrayList();
            while (node != null)
            {
                list.Add(node);
                node = node.parent;
            }
            list.Reverse();
            return list;
        }
        
        // private void OnDrawGizmos()
        // {
        //     if (closedList != null && closedList.Length > 0)
        //     {
        //         for (var i = 0; i < closedList.nodes.Count; i++)
        //         {
        //             var closedNode = (Node)closedList.nodes[i];
        //             Gizmos.color = Color.red;
        //             Gizmos.DrawCube(closedNode.position, new Vector3(GridManager.instance.gridCellSize, 1.0f,GridManager.instance.gridCellSize));
        //         }
        //     }
        //     
        //     if (openList != null && openList.Length > 0)
        //     {
        //         for (var i = 0; i < openList.nodes.Count; i++)
        //         {
        //             var openNode = (Node)openList.nodes[i];
        //             Gizmos.color = Color.yellow;
        //             Gizmos.DrawCube(openNode.position, new Vector3(GridManager.instance.gridCellSize, 1.0f,GridManager.instance.gridCellSize));
        //         }
        //     }
        //
        //     if (node != null)
        //     {
        //         Gizmos.color = Color.cyan;
        //         Gizmos.DrawCube(node.position, new Vector3(GridManager.instance.gridCellSize, 1.0f,GridManager.instance.gridCellSize));
        //     }
        // }
    }
}