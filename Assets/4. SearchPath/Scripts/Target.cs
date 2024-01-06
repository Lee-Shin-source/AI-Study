using System;
using UnityEngine;
using UnityEngine.AI;

namespace _4._SearchPath.Scripts
{
    public class Target : MonoBehaviour
    {
        private NavMeshAgent[] navAgents;
        public Transform targetMarker;

        private void Start()
        {
            navAgents = FindObjectsOfType(typeof(NavMeshAgent)) as NavMeshAgent[];
            
        }

        void UpdateTargets(Vector3 targetPosition)
        {
            foreach (var agent in navAgents)
            {
                agent.destination = targetPosition;
            }
        }

        private void Update()
        {
            int button = 0;

            if (Input.GetMouseButtonDown(button))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hitInfo;

                if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                {
                    Vector3 targetPosition = hitInfo.point;
                    UpdateTargets(targetPosition);
                    targetMarker.position = targetPosition + new Vector3(0, 5, 0);
                }
            }
        }
    }
}