using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Study
{
   public class TankAi : MonoBehaviour
{
    private GameObject _player;
    private Animator _animator;
    private Ray _ray;

    private RaycastHit _hit;

    private float _maxDistanceToCheck = 6.0f;
    private float _currentDistance;
    private Vector3 _checkDirection;
    
    // Patrol 변수
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;
    public NavMeshAgent navMeshAgent;

    private int _currentTarget;
    private float _distanceFormTarget;
    private Transform[] waypoints = null;

    public void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _animator = GetComponent<Animator>();
        pointA = GameObject.Find("WayPoint_1").transform;
        pointB = GameObject.Find("WayPoint_2").transform;
        pointC = GameObject.Find("WayPoint_3").transform;
        pointD = GameObject.Find("WayPoint_4").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        waypoints = new[]
        {
            pointA,
            pointB,
            pointC,
            pointD
        };

        _currentTarget = 0;
        navMeshAgent.SetDestination(waypoints[_currentTarget].position);
    }

    private void FixedUpdate()
    {
        _currentDistance = Vector3.Distance(_player.transform.position, transform.position);
        _animator.SetFloat("distanceFromPlayer",_currentDistance);

        _checkDirection = _player.transform.position - transform.position;
        _ray = new Ray(transform.position, _checkDirection);
        if (Physics.Raycast(_ray, out _hit, _maxDistanceToCheck))
        {
            if (_hit.collider.gameObject == _player)
            {
                _animator.SetBool("isPlayerVisible", true);
            }
            else
            {
                _animator.SetBool("isPlayerVisible", false);
            }
        }
        else
        {
            _animator.SetBool("isPlayerVisible", false);
        }

        _distanceFormTarget = Vector3.Distance(waypoints[_currentTarget].position, transform.position);
        _animator.SetFloat("distanceFromWaypoint", _distanceFormTarget);
    }

    public void SetNextPoint()
    {
        switch (_currentTarget)
        {
            case 0:
                _currentTarget = 1;
                break;
            case 1:
                _currentTarget = 2;
                break;
            case 2:
                _currentTarget = 3;
                break;
            case 3:
                _currentTarget = 0;
                break;
        }
        Debug.Log(_currentTarget);
        navMeshAgent.SetDestination(waypoints[_currentTarget].position);
    }

} 
}

