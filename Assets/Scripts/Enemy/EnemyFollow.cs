using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (_enemyController.player != null)
        {
            _distanceToPlayer = Vector3.Distance(_enemyController.transform.position, _enemyController.player.position);

            if (_distanceToPlayer > _enemyController.playerCheckDistance)
            {
                _enemyController.ChangeState(new EnemyIdle(_enemyController));
            }

            if (_distanceToPlayer < _enemyController.playerCheckDistance / 5)
            {
                _enemyController.ChangeState(new EnemyAttack(_enemyController));
            }

            _enemyController.agent.destination = _enemyController.player.position;
        }
        else
        {
            _enemyController.ChangeState(new EnemyIdle(_enemyController));
        }
    }
}
