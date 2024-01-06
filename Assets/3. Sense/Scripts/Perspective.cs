using System;
using TreeEditor;
using UnityEngine;

namespace Study
{
    public class Perspective : Sense
    {
        public int FieldOfView = 45;
        public int ViewDistance = 100;

        private Transform playerTrans;
        private Vector3 rayDirection;

        protected override void Initialize()
        {
            playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        }

        protected override void UpdateSense()
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= detectionRate) DetectAspect();
        }

        private void DetectAspect()
        {
            RaycastHit hit;

            rayDirection = playerTrans.position - transform.position;

            if ((Vector3.Angle(rayDirection, transform.forward)) < FieldOfView)
            {
                if (Physics.Raycast(transform.position, rayDirection, out hit, ViewDistance))
                {
                    var aspect = hit.collider.GetComponent<Aspect>();
                    if (aspect != null)
                    {
                        if (aspect.aspectName == aspectName)
                        {
                            Debug.Log("Enemy Detected");
                        }
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (playerTrans == null) return;
            Debug.DrawLine(transform.position,playerTrans.position,Color.red);

            var frontRayPoint = transform.position + (transform.forward * ViewDistance);
            
            Vector3 leftRayPoint = frontRayPoint;
            leftRayPoint.x += FieldOfView * 0.5f;

            Vector3 rightRayPoint = frontRayPoint;
            rightRayPoint.x -= FieldOfView * 0.5f;
            
            Debug.DrawLine(transform.position, frontRayPoint, Color.green);
            
            Debug.DrawLine(transform.position, leftRayPoint, Color.green);

            Debug.DrawLine(transform.position, rightRayPoint, Color.green);
        }
    }
}