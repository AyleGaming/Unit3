using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private Transform weaponTip;
    [SerializeField] private Rigidbody projectilePrefab;
    [SerializeField] private float shootingForce;

    ObjectPooling objectPoolingCache;

    private void Awake()
    {
        objectPoolingCache = FindObjectOfType<ObjectPooling>();
    }

    public void ShootAbility()
    {
        Rigidbody clonedRigidbody = objectPoolingCache.RetrieveAvailableBullet().GetRigidBody();

        if (clonedRigidbody == null) return;

        clonedRigidbody.position = weaponTip.position;
        clonedRigidbody.rotation = weaponTip.rotation;

        clonedRigidbody.AddForce(weaponTip.forward * shootingForce);
    }
}
