using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletDamage;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.transform);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            HealthSystem enemyHealthSystem = collision.rigidbody.GetComponent<HealthSystem>();

            if (enemyHealthSystem != null)
            {
                enemyHealthSystem.DecreaseHealth(bulletDamage);
            }
        }
    }
}
