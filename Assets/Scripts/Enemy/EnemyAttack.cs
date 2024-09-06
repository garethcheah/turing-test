using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyState
{
    private float _distanceToPlayer;
    private PlayerHealth _playerHealth;
    private float _damagePerSecond = 10.0f;

    public EnemyAttack(EnemyController enemyController) : base(enemyController)
    {
        _playerHealth = enemyController.player.GetComponent<PlayerHealth>();
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        Attack();

        if (_enemyController.player != null)
        {
            _distanceToPlayer = Vector3.Distance(_enemyController.transform.position, _enemyController.player.position);

            if (_distanceToPlayer > _enemyController.playerCheckDistance / 5)
            {
                _enemyController.ChangeState(new EnemyFollow(_enemyController));
            }

            _enemyController.agent.destination = _enemyController.player.position;
        }
        else
        {
            _enemyController.ChangeState(new EnemyIdle(_enemyController));
        }
    }

    private void Attack()
    {
        if (_playerHealth != null)
        {
            _playerHealth.DeductHealth(_damagePerSecond * Time.deltaTime);
        }
    }
}
