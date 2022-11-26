using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public DamageSources[] damagedBy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hitbox attack = collision.GetComponent<Hitbox>();
        if (attack != null)
        {
            if (damagedBy.Contains(attack.damageType))
            {
                //Handle damage here
                HealthComponent health = GetComponent<HealthComponent>();
                if (health == null)
                {
                    //If we're not directly connected, check if it's the player, with its shared health
                    health = GetComponentInParent<HealthComponent>();
                    if (health == null)
                    {
                        Debug.LogError("Hurtbox hit, but not healthComponent found");
                        return;
                    }
                }
                health.TakeDamage(attack.damage);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
