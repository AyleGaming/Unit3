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
    [SerializeField] private AudioClip attackSound;

    private bool _isAttacking;
    private float _attackTimer;
    private HealthSystem _targetToAttack;

    // Update is called once per frame
    void Update()
    {
        _attackTimer += Time.deltaTime;

        if (_isAttacking)
        {
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
        _isAttacking = true;
    }

    public void StopAttack()
    {
        _isAttacking = false;
    }

    public void AttackAbility() 
    {
        if (_targetToAttack)
        {
            _targetToAttack.DecreaseHealth(_damageToGive);
            if (attackSound != null)
            {
                AudioManager.Instance.PlaySound(attackSound);
            }
        }
    }
}
