using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Study
{
    public class Target : MonoBehaviour
    {
        public Transform targetMarker;

        private void Update()
        {
            int button = 0;
            if (Input.GetMouseButtonDown(button))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                {
                    var targetPosition = hitInfo.point;
                    targetMarker.position = targetPosition;
                }
            }
        }
    }
}

