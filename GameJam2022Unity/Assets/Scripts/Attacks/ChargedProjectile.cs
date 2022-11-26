using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedProjectile : ChargedObjectComponent
{
    [SerializeField]
    float maxSpeed;

    protected float CalculateProjectileSpeed(float chargePercentage)
    {
        return chargePercentage * maxSpeed;
    }

    public override void InitWithCharge(float chargePercentage)
    {
        Rigidbody2D projectileRigidbody = GetComponent<Rigidbody2D>();
        if (projectileRigidbody)
        {
            float speed = CalculateProjectileSpeed(chargePercentage);
            projectileRigidbody.velocity = transform.rotation * (speed * new Vector3(1,0,0));
        }

    }
}
