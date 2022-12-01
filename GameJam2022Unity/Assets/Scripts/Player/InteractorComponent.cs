using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorComponent : MonoBehaviour
{
    protected List<InteractableComponent> mRegisteredInteractables = new List<InteractableComponent>();

    public void OnTriggerEnter2D(Collider2D collision)
    {
        InteractableComponent interactable = collision.GetComponent<InteractableComponent>();
        if (interactable != null)
        {
            RegisterInteractable(interactable);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        InteractableComponent interactable = collision.GetComponent<InteractableComponent>();
        if (interactable != null)
        {
            UnregisterInteractable(interactable);
        }
    }

    public void RegisterInteractable(InteractableComponent interactable)
    {
        if (!mRegisteredInteractables.Contains(interactable))
        {
            mRegisteredInteractables.Add(interactable);
        }
    }

    public void UnregisterInteractable(InteractableComponent interactable)
    {
        // no need to check if mRegisteredInteractables actually has interactable since the Remove checks for it safely
        mRegisteredInteractables.Remove(interactable);
    }

    public void TryInteract()
    {
        InteractableComponent bestCandidate = null;
        float closestDist = float.PositiveInfinity;
        foreach (InteractableComponent interactable in mRegisteredInteractables)
        {
            Vector2 dist = interactable.transform.position - transform.position;

            float xDistToInteractable = Mathf.Abs(dist.x);
            if ( xDistToInteractable < closestDist)
            {
                bestCandidate = interactable;
                closestDist = xDistToInteractable;
            }
        }
        if (bestCandidate != null)
        {
            bestCandidate.Interact(this);
        }
    }
}
