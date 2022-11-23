using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthDisplay : MonoBehaviour
{
    [SerializeField]
    HealthComponent trackedHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trackedHealth)
        {
            transform.localScale = new Vector3( (float)trackedHealth.CurrHealth / (float)trackedHealth.MaxHealth, transform.localScale.y, transform.localScale.z);
        }
    }
}
