using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] _targetPoints;
    public Transform _enemy;
    public Transform _player;
    public NavMeshAgent _agent;
    public float _playerCheckDistance;
    public float _checkRadius = 0.4f;
    public float _aggroDistance;

    private EnemyState _currentState;

    public void ChangeState(EnemyState state)
    {
        _currentState.OnStateExit();
        _currentState = state;
        _currentState.OnStateEnter();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currentState = new EnemyIdle(this);
        _currentState.OnStateEnter();
    }

    // Update is called once per frame
    private void Update()
    {
        _currentState.OnStateUpdate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_enemy.position, _checkRadius);
        Gizmos.DrawWireSphere(_enemy.position + _enemy.forward * _playerCheckDistance, _checkRadius);
        Gizmos.DrawLine(_enemy.position, _enemy.position + _enemy.forward * _playerCheckDistance);
    }
}
