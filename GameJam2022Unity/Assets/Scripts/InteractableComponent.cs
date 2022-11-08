using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableComponent : MonoBehaviour
{
    // require trigger collider
    // register/unregister with InteractorComponent if it enters range of trigger collider

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
        Debug.Log("Interacting with interactable");
    }
}
