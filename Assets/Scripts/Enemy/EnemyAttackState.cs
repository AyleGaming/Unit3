using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    float _distanceToPlayer;

    Attack _attackAbility;

    public EnemyAttackState(EnemyController enemy) : base(enemy)
    {
        _attackAbility = enemy.GetComponent<Attack>();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Attacking player!");

        if(_attackAbility != null)
        {
            _attackAbility.StartAttack(_enemy._player);
        }
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy Will stop Attacking player");

        if (_attackAbility != null)
        {
            _attackAbility.StopAttack();
        }
    }

    public override void OnStateUpdate()
    {
        if (_enemy._player != null)
        {

            _distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy._player.position);

            //Go to follow mode
            if (_distanceToPlayer > 2)
            {
                _enemy.ChangeState(new EnemyFollowState(_enemy));
            }


            _enemy._agent.destination = _enemy._player.position;
        }
        else
        {
            // Go back to idle
            _enemy.ChangeState(new EnemyIdleState(_enemy));
        }
    }
}
