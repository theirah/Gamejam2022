using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Scratch", order = 1)]
public class Scratch : Weapon
{
    [SerializeField]
    Vector3 spawnOffset;
    [SerializeField]
    GameObject hitboxRepresentation; // the object representing the hitbox and animation associated with the struck location
    [SerializeField]
    float cooldown;
    private GameObject hitbox = null;

    public void Fire(Vector3 initiatorPosition, Vector3 targetPosition)
    {
        if (hitbox == null) 
        {
            int offsetDirection = targetPosition.x >= initiatorPosition.x ? 1 : -1;
            Vector3 instanceOffset = spawnOffset;
            instanceOffset.x *= offsetDirection;
            hitbox = Instantiate(hitboxRepresentation, initiatorPosition + instanceOffset, Quaternion.identity);
            hitbox.transform.localScale = new Vector3(offsetDirection * hitbox.transform.localScale.x, hitbox.transform.localScale.y, hitbox.transform.localScale.z);
        }
    }
}
