using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bow", menuName = "Weapon/Bow", order = 1)]
public class Bow : Weapon
{
    [SerializeField]
    float maxChargeTime;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    GameObject projectilePrefab;
    [SerializeField]
    float spawnOffset;

    float chargeTime;
    public void AddCharge(float chargeAmt)
    {
        chargeTime += chargeAmt;
        if (chargeTime > maxChargeTime)
        {
            chargeTime = maxChargeTime;
        }
    }

    public void CancelCharging()
    {
        chargeTime = 0;
    }

    public void Fire(Vector3 initiatorPosition, Vector3 targetPosition)
    {
        if (projectilePrefab)
        {
            Vector3 direction = (targetPosition - initiatorPosition).normalized;

            GameObject projectile = Instantiate(projectilePrefab, initiatorPosition + direction*spawnOffset, Quaternion.Euler(DirectionToAngle(direction), 0, 0));
            Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
            if (projectileRigidbody)
            {
                float speed = CalculateProjectileSpeed();
                projectileRigidbody.velocity = speed * direction;
            }
        }
    }

    protected float CalculateProjectileSpeed()
    {
        return (chargeTime / maxChargeTime) * maxSpeed;
    }

    protected float DirectionToAngle(Vector3 direction)
    {
        return 0;
    }
}
