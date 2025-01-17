using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For AI chars to attack player
/// </summary>
public class Attack : MonoBehaviour
{
    [SerializeField] private float _damageToGive;
    [SerializeField] private float _attackCooldown;

    private bool _isAttacking;
    private float _attackTimer;
    private HealthSystem _targetToAttack;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAttacking)
        {
            _attackTimer += Time.deltaTime;
            if(_attackTimer >= _attackCooldown)
            {
                AttackAbility();
                _attackTimer = 0;
            }
        }
    }

    public void StartAttack(Transform target)
    {
        _targetToAttack = target.GetComponent<HealthSystem>();
        Debug.Log("START Attack by State");
        _isAttacking = true;
    }

    public void StopAttack()
    {
        Debug.Log("STOP Attack by State");
        _isAttacking = false;
    }

    public void AttackAbility() 
    {
        if (_targetToAttack)
        {
            _targetToAttack.DecreaseHealth(_damageToGive);
        }

    }
}
