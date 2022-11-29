using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] public GameObject grenade;
    [SerializeField] public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        explosion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (explosion == null) 
        {
            Destroy(this.gameObject);
        }
        if (grenade == null)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            if (explosion != null)
            {
                explosion.SetActive(true);
            }
        }
    }
}
