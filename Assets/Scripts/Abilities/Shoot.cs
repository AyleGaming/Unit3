using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private Transform weaponTip;
    [SerializeField] private Rigidbody projectilePrefab;
    [SerializeField] private float shootingForce;

    // Start is called before the first frame update
    public void ShootAbility()
    {
        Rigidbody clonedRigidBody = Instantiate(projectilePrefab, weaponTip.position, weaponTip.rotation);
        clonedRigidBody.AddForce(weaponTip.forward * shootingForce);
    }
}
