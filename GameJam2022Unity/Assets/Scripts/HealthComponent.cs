using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    public float MaxHealth;

    public float CurrHealth { get; protected set; }

    public void Start()
    {
        CurrHealth = MaxHealth;
    }

    // returns true if still alive after damage
    public bool TakeDamage (float damage) {
        if (CurrHealth > 0)
        {
            CurrHealth -= damage;
            if (CurrHealth <= 0)
            {
                CurrHealth = 0;
                // Todo: Fire death event
            }
        }
        return CurrHealth > 0;
    }
}
