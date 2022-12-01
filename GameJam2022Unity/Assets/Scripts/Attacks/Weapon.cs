using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : ScriptableObject
{
    [SerializeField]
    protected GameObject hitPrefab; // the object carrying the hitbox for the attack

    [SerializeField]
    public float cooldown;

    public AudioManager.soundEffect weaponSoundEffect;

    // charge stuff
    [SerializeField]
    public bool isChargeWeapon;
    [SerializeField]
    public float maxChargeTime;

    virtual public GameObject Fire(Vector3 initiatorPosition, Vector3 targetPosition, float charge) {
        if (hitPrefab)
        {
            GameObject go = Instantiate(hitPrefab, GetHitSpawnLocation(initiatorPosition, targetPosition), GetHitSpawnRotation(initiatorPosition, targetPosition));
            return go;
        }
        return null;
    }

    virtual protected Vector3 GetHitSpawnLocation(Vector3 initiatorPosition, Vector3 targetPosition)
    {
        return initiatorPosition; // default behavior spawns on top of initiator
    }

    virtual protected Quaternion GetHitSpawnRotation(Vector3 initiatorPosition, Vector3 targetPosition)
    {
        return Quaternion.identity;
    }
}
