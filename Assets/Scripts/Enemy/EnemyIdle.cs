using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyState
{
    private int _currentTarget = 0;

    public EnemyIdle(EnemyController enemyController) : base(enemyController)
    {
    }

    public override void OnStateEnter()
    {
        _enemyController.agent.destination = _enemyController.targetPoints[_currentTarget].position;
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        if (_enemyController.agent.remainingDistance < 0.1f)
        {
            _currentTarget++;

            if (_currentTarget >= _enemyController.targetPoints.Length)
                _currentTarget = 0;

            _enemyController.agent.destination = _enemyController.targetPoints[_currentTarget].position;
        }

        if (Physics.SphereCast(_enemyController.transform.position, _enemyController.checkRadius, _enemyController.transform.forward, out RaycastHit rayCastHit, _enemyController.aggroDistance))
        {
            if (rayCastHit.transform.CompareTag("Player"))
            {
                Debug.Log("Player found.");
                _enemyController.player = rayCastHit.transform;
                _enemyController.agent.destination = _enemyController.player.position;
                _enemyController.ChangeState(new EnemyFollow(_enemyController));
            }
        }
    }
}
