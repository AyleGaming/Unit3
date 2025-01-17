using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _targetPoints;
    [SerializeField] private Transform _enemy;
    [SerializeField] private float _playerCheckDistance;
    [SerializeField] private float _checkRadius = 0.4f;

    int _currentTarget = 0;

    private NavMeshAgent _agent;

    public bool isIdle = true;
    public bool isPlayerFound;
    public bool isCloseToPlayer;

    public Transform _player;


    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        // Tell agent to where the first point is
        _agent.destination = _targetPoints[_currentTarget].position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle)
        {
            Idle();

        } 
        else if (isPlayerFound)
        {
            if (isCloseToPlayer)
            {
                AttackPlayer();
            }
            else
            {
                FollowPlayer();
            }
        }
    }

    void Idle()
    {
       
    }

    void FollowPlayer()
    {
        if(_player != null)
        {
            if(Vector3.Distance(transform.position, _player.position) > 10)
            {
                isPlayerFound = false;
                isIdle = true;
            }

            // close to player
            if (Vector3.Distance(transform.position, _player.position) < 2)
            {
                isCloseToPlayer = true;
            } 
            else
            {
                isCloseToPlayer = false;
            }

            _agent.destination = _player.position;
        } else
        {
            isPlayerFound = false;
            isIdle = true;
            isCloseToPlayer = false;
        }
    }

    void AttackPlayer()
    {
        if(_player != null)
        {
            Debug.Log("ATTACK PALYER");
            if (Vector3.Distance(transform.position, _player.position) > 2)
            {
                isCloseToPlayer = false;
            }
        } 
        else
        {
            Debug.Log("No player found in game");
        }
    }
}
