using System;
using System.Collections;
using UnityEngine;

namespace _4._SearchPath.Scripts.AStar
{
    public class TestCode : MonoBehaviour
    {
        private Transform startPos, endPos;
        public Node startNode { get; set; }
        public Node goalNode { get; set; }

        public ArrayList pathArray;

        private GameObject objStartCube, objEndCube;
        private float elapsedTime = 0.0f;
        public float intervalTime = 1.0f;

        public AStar aStar;

        private void Start()
        {
            objStartCube = GameObject.FindGameObjectWithTag("Start");
            objEndCube = GameObject.FindGameObjectWithTag("End");

            pathArray = new ArrayList();
            FindPath();
        }

        // private void Update()
        // {
        //     elapsedTime += Time.deltaTime;
        //     if (elapsedTime >= intervalTime)
        //     {
        //         elapsedTime = 0.0f;
        //         FindPath();
        //     }
        // }

        private void FindPath()
        {
            startPos = objStartCube.transform;
            endPos = objEndCube.transform;

            startNode = new Node(
                GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(startPos.position)));
            goalNode = new Node(
                GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(endPos.position)));

            StartCoroutine(aStar.FindPath(startNode, goalNode, pathArray));
        }

        private void OnDrawGizmos()
        {
            if (pathArray == null)
            {
                return;
            }

            if (pathArray.Count > 0)
            {
                int index = 1;
                foreach (Node node in pathArray)
                {
                    if (index < pathArray.Count)
                    {
                        var nextNode = (Node)pathArray[index];
                        Debug.DrawLine(node.position, nextNode.position, Color.green);
                        index++;
                    }
                }
            }
        }
    }
}