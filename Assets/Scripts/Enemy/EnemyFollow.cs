using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyFollow : EnemyState
{
    private float _distanceToPlayer;

    public EnemyFollow(EnemyController enemyController) : base(enemyController)
    {
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        if (_enemyController._player != null)
        {
            _distanceToPlayer = Vector3.Distance(_enemyController.transform.position, _enemyController._player.position);

            if (_distanceToPlayer > _enemyController._playerCheckDistance)
            {
                _enemyController.ChangeState(new EnemyIdle(_enemyController));
            }

            if (_distanceToPlayer < _enemyController._playerCheckDistance / 5)
            {
                _enemyController.ChangeState(new EnemyAttack(_enemyController));
            }

            _enemyController._agent.destination = _enemyController._player.position;
        }
        else
        {
            _enemyController.ChangeState(new EnemyIdle(_enemyController));
        }
    }
}
