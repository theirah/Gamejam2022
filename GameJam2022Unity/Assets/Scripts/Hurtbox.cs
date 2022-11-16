using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hitbox attack = collision.GetComponent<Hitbox>();
        if (attack != null)
        {
            //TODO: Check if the object this script is attached to takes damage from the attacker by looking at the attack object
            //Handle damage here
            Debug.Log("Ouch!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
