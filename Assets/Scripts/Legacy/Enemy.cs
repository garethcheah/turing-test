using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool isIdle = true;
    public bool isPlayerFound;
    public bool isCloseToPlayer;

    [SerializeField] private Transform[] _secondaryTargetPoins;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _enemy;
    [SerializeField] private float _targetCheckDistance = 10.0f;
    [SerializeField] private float _targetCheckRadius = 0.4f;
    [SerializeField] private float _targetAggroDistance = 5.0f;

    private NavMeshAgent _agent;
    private int _currentTarget = 0;

    // Start is called before the first frame update
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = _secondaryTargetPoins[_currentTarget].position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isIdle)
        {
            Idle();
        }
        else if (isPlayerFound)
        {
            if (isCloseToPlayer)
            {
                Attack();
            }
            else
            {
                FollowPlayer();
            }
        }
    }

    private void FollowPlayer()
    {
        if (_target != null)
        {
            if (Vector3.Distance(transform.position, _target.position) > _targetCheckDistance)
            {
                isPlayerFound = false;
                isIdle = true;
            }

            if (Vector3.Distance(transform.position, _target.position) < _targetCheckDistance / 5)
            {
                isCloseToPlayer = true;
            }
            else
            {
                isCloseToPlayer = false;
            }

            _agent.destination = _target.position;
        }
        else
        {
            isPlayerFound = false;
            isIdle = true;
            isCloseToPlayer = false;
        }
    }

    private void Idle()
    {
        if (_agent.remainingDistance < 0.1f)
        {
            _currentTarget++;

            if (_currentTarget >= _secondaryTargetPoins.Length)
                _currentTarget = 0;

            _agent.destination = _secondaryTargetPoins[_currentTarget].position;
        }

        if (Physics.SphereCast(_enemy.position, _targetCheckRadius, transform.forward, out RaycastHit rayCastHit, _targetAggroDistance))
        {
            if (rayCastHit.transform.CompareTag("Player"))
            {
                Debug.Log("Player found.");
                isIdle = false;
                isPlayerFound = true;
                _target = rayCastHit.transform;
                _agent.destination = _target.position;
            }
        }
    }

    private void Attack()
    {
        Debug.Log("Attacking player.");

        if (Vector3.Distance(transform.position, _target.position) > _targetCheckDistance / 5)
        {
            isCloseToPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_enemy.position, _targetCheckRadius);
        Gizmos.DrawWireSphere(_enemy.position + _enemy.forward * _targetCheckDistance, _targetCheckRadius);
        Gizmos.DrawLine(_enemy.position, _enemy.position + _enemy.forward * _targetCheckDistance);
    }
}
