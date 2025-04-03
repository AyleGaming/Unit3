using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] public LineRenderer turretLaser;
    [SerializeField] public Transform laserWeaponTip;
    [SerializeField] public float laserLength;

    public Color laserIdleColor = Color.green;
    public Color laserAttackColor = Color.red;

    void Awake()
    {
        // set color to green
        SetLaserColor(laserIdleColor);
        DrawLaser();
    }

    private void Update()
    {
        // draw laser to laser length
        DrawLaser();
    }

    public void DrawLaser()
    {
        // set base position
        turretLaser.SetPosition(0, laserWeaponTip.position);
        // draw endpoint from weapon tip forward multiplied by laser length
        turretLaser.SetPosition(1, laserWeaponTip.position + laserWeaponTip.forward * laserLength);
    }

    public void DrawLaserToHitPoint(Vector3 laserHitPoint)
    {
        // draw end of laser to hit point
        turretLaser.SetPosition(1, laserHitPoint); 
    }

    public void SetLaserColor(Color laserColor)
    {
        Color transparentColor = new Color(laserColor.r, laserColor.g, laserColor.b, 0.5f);
        turretLaser.startColor = transparentColor;
        turretLaser.endColor = transparentColor;
    }

    public bool LaserCheckCollision()
    {
        Ray laserRay = new (laserWeaponTip.position, laserWeaponTip.forward);

        // Check if ray hits something in range of laser length 
        if (Physics.Raycast(laserRay, out RaycastHit laserRayHit, laserLength))
        {

            // Laser hit player
            if (laserRayHit.transform.CompareTag("Player"))
            {
                SetLaserColor(laserAttackColor);
                DrawLaserToHitPoint(laserRayHit.point);  // Draw laser to hit
                return true;
            } 
            // Laser hit something else
            else
            {
                SetLaserColor(laserIdleColor);
                DrawLaserToHitPoint(laserRayHit.point); // Draw laser to hit
            }
        }
        // Laser didn't hit, reset laser
        else
        {
            SetLaserColor(laserIdleColor);
            DrawLaser();
        }

        return false;
    }
}
