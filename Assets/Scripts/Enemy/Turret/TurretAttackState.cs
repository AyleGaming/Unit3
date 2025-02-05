using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackState : TurretState
{
    readonly Attack _attackAbility;
    readonly Laser _laserAbility;

    public TurretAttackState(TurretController turret) : base(turret)
    {
        _attackAbility = turret.GetComponent<Attack>();
        _laserAbility = turret.GetComponent<Laser>();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Attack player!");
        if (_attackAbility != null)
        {
            _attackAbility.StartAttack(_turret._player);
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
        if (_turret._player != null)
        {
            // LASER STOPPED HITTING PLAYER
            if (_laserAbility.LaserCheckCollision() == false) { 
                // Go back to idle
                _turret.ChangeState(new TurretIdleState(_turret));
            }
        }
    }
}
