using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace _4._SearchPath.Scripts.AStar
{
    public class GridManager : MonoBehaviour
    {
        public static GridManager s_Instance = null;

        public static GridManager instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = FindObjectOfType(typeof(GridManager)) as GridManager;
                    if (s_Instance == null)
                    {
                        Debug.Log("Could not locate a GridManager" + "object. \n You have to have exactly" + "one GridManager in the scene.");
                    }
                }

                return s_Instance;
            }
        }

        public int numOfRows;
        public int numOfColumns;
        public float gridCellSize;
        public bool showGrid = true;
        public bool showObstacleBlocks = true;

        private Vector3 origin = new Vector3();
        private GameObject[] obstacleList;
        public Node[,] nodes { get; set; }
        public Vector3 Origin => origin;

        private void Awake()
        {
            obstacleList = GameObject.FindGameObjectsWithTag("Obstacle");
            CalculateObstacles();
        }

        void CalculateObstacles()
        {
            nodes = new Node[numOfColumns, numOfRows];
            int index = 0;
            for (int i = 0; i < numOfColumns; i++)
            {
                for (int j = 0; j < numOfRows; j++)
                {
                    var cellPos = GetGridCellCenter(index);
                    var node = new Node(cellPos);
                    nodes[i, j] = node;
                    index++;
                }
            }

            if (obstacleList != null && obstacleList.Length > 0)
            {
                foreach (var data in obstacleList)
                {
                    int indexCell = GetGridIndex(data.transform.position);
                    Debug.Log(data.transform.position);
                    int col = GetColumn(indexCell);
                    int row = GetRow(indexCell);
                    nodes[row,col].MarkAsObstacle();
                }
            }
                
        }

   

        public Vector3 GetGridCellCenter(int index)
        {
            var cellPosition = GetGridCellPosition(index);
            cellPosition.x += (gridCellSize / 2.0f);
            cellPosition.y += (gridCellSize / 2.0f);
            return cellPosition;
        }

        public Vector3 GetGridCellPosition(int index)
        {
            var row = GetRow(index);
            var col = GetColumn(index);

            var xPosInGrid = col * gridCellSize;
            var zPosInGrid = row * gridCellSize;
            return Origin + new Vector3(xPosInGrid, 0, zPosInGrid);
        }

        public int GetGridIndex(Vector3 pos)
        {
            if (!IsInBounds(pos))
            {
                return -1;
            }

            pos -= Origin;
            var col = (int)(pos.x / gridCellSize);
            var row = (int)(pos.z / gridCellSize);

            return (row * numOfColumns + col);
        }

        private bool IsInBounds(Vector3 pos)
        {
            var width = numOfColumns * gridCellSize;
            var height = numOfRows * gridCellSize;

            return (pos.x >= Origin.x && pos.x <= Origin.x + width && pos.z <= Origin.z + height && pos.z >= Origin.z);
        }

        private int GetRow(int indexCell)
        {
            int row = indexCell / numOfColumns;
            return row;
        }

        private int GetColumn(int indexCell)
        {
            int col = indexCell % numOfColumns;
            return col;
        }

        public void GetNeighbours(Node node, ArrayList neighbors)
        {
            var neighborPos = node.position;
            var neighborIndex = GetGridIndex(neighborPos);

            int row = GetRow(neighborIndex);
            int column = GetColumn(neighborIndex);

            int leftNodeRow = row - 1;
            int leftNodeColumn = column;
            AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors);
            
            leftNodeRow = row + 1;
            leftNodeColumn = column;
            AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors);

            leftNodeRow = row;
            leftNodeColumn = column + 1;
            AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors);
            
            leftNodeRow = row;
            leftNodeColumn = column - 1;
            AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors);
        }

        // 장애물 판별 : 이웃 노드를 참조 배열 목록 neighbors에 추가
        private void AssignNeighbour(int row, int column, ArrayList neighbors)
        {
            if (row != -1 && column != -1 && row < numOfRows && column < numOfColumns)
            {
                var nodeToAdd = nodes[row, column];
                if (!nodeToAdd.bObstacle)
                {
                    neighbors.Add(nodeToAdd);
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (showGrid)
            {
                DebugDrawGrid(transform.position, numOfRows, numOfColumns, gridCellSize, Color.blue);
            }
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(transform.position, 0.5f);
            if (showObstacleBlocks)
            {
                var cellSize = new Vector3(gridCellSize, 1.0f, gridCellSize);
                if (obstacleList == null) return;
                foreach (var data in obstacleList)
                {
                    Gizmos.DrawCube(GetGridCellCenter(GetGridIndex(data.transform.position)),cellSize);
                }
            }
        }

        private void DebugDrawGrid(Vector3 origin, int numRows, int numCols, float cellSize, Color color)
        {
            var width = numCols * cellSize;
            var height = numRows * cellSize;

            for (int i = 0; i < numRows + 1; i++)
            {
                var startPos = origin + i * cellSize * new Vector3(0.0f, 0.0f, 1.0f);
                var endPos = startPos + width * new Vector3(1.0f, 0.0f, 0.0f);
                Debug.DrawLine(startPos,endPos,color);
            }
            
            for (int i = 0; i < numCols + 1; i++)
            {
                var startPos = origin + i * cellSize * new Vector3(1.0f, 0.0f, 0.0f);
                var endPos = startPos + height * new Vector3(0.0f, 0.0f, 1.0f);
                Debug.DrawLine(startPos,endPos,color);
            }
        }
    }
}