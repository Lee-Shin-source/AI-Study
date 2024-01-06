using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Study
{
    public class PlayerTank : MonoBehaviour
    {
        public Transform targetTransform;
        private float movementSpeed, rotSpeed;

        private void Start()
        {
            movementSpeed = 10.0f;
            rotSpeed = 2.0f;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, targetTransform.position) < 5.0f)
            {
                return;
            }

            Vector3 tarpos = targetTransform.position;
            tarpos.y = transform.position.y;
            var dirRot = tarpos - transform.position;

            var tarRot = Quaternion.LookRotation(dirRot);
            transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, rotSpeed * Time.deltaTime);
            transform.Translate(new Vector3(0,0,movementSpeed * Time.deltaTime));
        }
    }  
}

