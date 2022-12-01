using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script traps entities with a BecomeTrapped script when they collide
public class TrapEntity : MonoBehaviour
{
    [SerializeField] public int trapTime = 60;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTrap()
    {
        HitboxLifetime life = GetComponent<HitboxLifetime>();
        if (life != null)
        {
            life.lifetime = trapTime;
        }
    }
}
