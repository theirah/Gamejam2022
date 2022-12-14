using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    AudioManager audioManager;
    public AudioManager.soundEffect hitSound = AudioManager.soundEffect.HIT; 

    [SerializeField]
    public float MaxHealth;

    [SerializeField]
    public float CurrHealth;   //Needs to be accessed by healing items, so can't be protected set

    public UnityEvent OnHealthReachesZero; 

    public void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        CurrHealth = MaxHealth;
    }

    // returns true if still alive after damage
    public bool TakeDamage (float damage) {
        if (CurrHealth > 0)
        {
            audioManager.PlaySoundEffect(hitSound);

            CurrHealth -= damage;
            if (CurrHealth <= 0)
            {
                CurrHealth = 0;
                OnHealthReachesZero.Invoke();
            }
        }
        return CurrHealth > 0;
    }
}
