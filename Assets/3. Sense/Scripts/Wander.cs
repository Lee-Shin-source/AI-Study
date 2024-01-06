using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wander : MonoBehaviour
{
    private Vector3 tarPos;

    private float movementSpeed = 5.0f;
    private float rotSpeed = 2.0f;
    private float minX, maxX, minZ, maxZ;

    private void Start()
    {
        minX = -45.0f;
        maxX = 45.0f;

        minZ = -45.0f;
        maxZ = 45.0f;
        GetNextPosition();
    }

    private void Update()
    {
        if (Vector3.Distance(tarPos, transform.position) <= 5.0f)
        {
            GetNextPosition();
        }

        var tarRot = Quaternion.LookRotation(tarPos - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, rotSpeed * Time.deltaTime);
        transform.Translate(new Vector3(0,0,movementSpeed * Time.deltaTime));
        
    }

    private void GetNextPosition()
    {
        tarPos = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
    }
}
