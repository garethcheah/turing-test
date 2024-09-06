using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] targetPoints;
    public Transform enemy;
    public Transform player;
    public NavMeshAgent agent;
    public float playerCheckDistance;
    public float checkRadius = 0.4f;
    public float aggroDistance;

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
        agent = GetComponent<NavMeshAgent>();
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
        Gizmos.DrawWireSphere(enemy.position, checkRadius);
        Gizmos.DrawWireSphere(enemy.position + enemy.forward * playerCheckDistance, checkRadius);
        Gizmos.DrawLine(enemy.position, enemy.position + enemy.forward * playerCheckDistance);
    }
}
