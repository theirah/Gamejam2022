using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBush : MonoBehaviour
{
    public float healthAmount;
    public GameObject emptyBush;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(InteractorComponent interactor)
    {
        HealthComponent health =  interactor.transform.parent.GetComponent<HealthComponent>();
        if (health != null)
        {
            health.CurrHealth += healthAmount;
            if (health.CurrHealth > health.MaxHealth)
                health.CurrHealth = health.MaxHealth;

            emptyBush.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
