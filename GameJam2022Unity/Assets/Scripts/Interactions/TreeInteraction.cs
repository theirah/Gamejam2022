using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteraction : InteractableComponent
{

    public override void Interact(InteractorComponent interactor)
    {
        Debug.Log("I'm a tree!");
    }
}
