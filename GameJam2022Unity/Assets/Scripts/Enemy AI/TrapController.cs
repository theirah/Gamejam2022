using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private GameObject setTrap;
    [SerializeField] private GameObject shutTrap;
    [SerializeField] public int snapTime;

    private bool trapHasShut = false;

    // Start is called before the first frame update
    void Start()
    {
        if(setTrap == null || shutTrap == null) 
        {
            Debug.LogError("Error, trap game object missing");
            return;
        }
        shutTrap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        snapTime--;
        if (snapTime < 0 && !trapHasShut)
        {
            setTrap.GetComponent<SpriteRenderer>().enabled = false;
            shutTrap.SetActive(true);
            trapHasShut = true;
        }
        else if (trapHasShut)
        {
            if (shutTrap == null)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
