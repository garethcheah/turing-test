using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyIdle : EnemyState
{
    private int _currentTarget = 0;

    public EnemyIdle(EnemyController enemyController) : base(enemyController)
    {
    }

    public override void OnStateEnter()
    {
        _enemyController._agent.destination = _enemyController._targetPoints[_currentTarget].position;
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        if (_enemyController._agent.remainingDistance < 0.1f)
        {
            _currentTarget++;

            if (_currentTarget >= _enemyController._targetPoints.Length)
                _currentTarget = 0;

            _enemyController._agent.destination = _enemyController._targetPoints[_currentTarget].position;
        }

        if (Physics.SphereCast(_enemyController.transform.position, _enemyController._checkRadius, _enemyController.transform.forward, out RaycastHit rayCastHit, _enemyController._aggroDistance))
        {
            if (rayCastHit.transform.CompareTag("Player"))
            {
                Debug.Log("Player found.");
                _enemyController._player = rayCastHit.transform;
                _enemyController._agent.destination = _enemyController._player.position;
                _enemyController.ChangeState(new EnemyFollow(_enemyController));
            }
        }
    }
}
