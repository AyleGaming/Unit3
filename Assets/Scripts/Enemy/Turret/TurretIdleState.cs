using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleState : TurretState
{
    readonly Laser _laserAbility;

    public TurretIdleState(TurretController turret) : base(turret)
    {
        _laserAbility = turret.GetComponent<Laser>();
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        if (_turret._player != null)
        {
            // HIT PLAYER: set attack state
            if (_laserAbility.LaserCheckCollision() == true)
            {
                _turret.ChangeState(new TurretAttackState(_turret));
            }
        }
    }
}
