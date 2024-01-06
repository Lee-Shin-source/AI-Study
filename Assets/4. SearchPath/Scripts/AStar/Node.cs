using System;
using UnityEngine;


namespace _4._SearchPath.Scripts.AStar
{
    public class Node : IComparable
    {
        // G : 시작 위치에서 현재 노드까지의 이동 비용
        public float nodeTotalCost;
        // H : 현재 노드에서 대상 목표 노드까지의 총 추정 비용
        public float estimatedCost;
        public bool bObstacle;
        public Node parent;
        public Vector3 position;

        public Node()
        {
            this.estimatedCost = 0.0f;
            this.nodeTotalCost = 1.0f;
            this.bObstacle = false;
            this.parent = null;
        }

        public Node(Vector3 pos)
        {
            this.estimatedCost = 0.0f;
            this.nodeTotalCost = 1.0f;
            this.bObstacle = false;
            this.parent = null;
            this.position = pos;
        }

        public void MarkAsObstacle()
        {
            this.bObstacle = true;
        }
        
        
        public int CompareTo(object obj)
        {
            var node = (Node)obj;
            if (this.estimatedCost < node.estimatedCost) return -1;
            if (this.estimatedCost > node.estimatedCost) return 1;
            return 0;
        }
    }
}