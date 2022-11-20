using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxLifetime : MonoBehaviour
{
    public int lifetime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        lifetime--;
        if (lifetime < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
