using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Scratch", order = 1)]
public class Scratch : Weapon
{
    Vector3 spawnOffset;
    protected override Vector3 GetHitSpawnLocation(Vector3 initiatorPosition, Vector3 targetPosition)
    {
        Vector3 instanceOffset = spawnOffset;
        instanceOffset.x *= GetFaceDirectionMultiplier(initiatorPosition, targetPosition);
        return initiatorPosition + instanceOffset;
    }

    protected override Quaternion GetHitSpawnRotation(Vector3 initiatorPosition, Vector3 targetPosition)
    {
        if (IsFacingRight(initiatorPosition, targetPosition))
        {
            return Quaternion.identity;
        }
        else
        {
            return Quaternion.Euler(0, 180, 0);
        }
    }

    // returns 1 if facing right, -1 if facing left
    int GetFaceDirectionMultiplier(Vector3 initiatorPosition, Vector3 targetPosition)
    {
        return IsFacingRight(initiatorPosition, targetPosition) ? 1 : -1;
    }
    bool IsFacingRight(Vector3 initiatorPosition, Vector3 targetPosition)
    {
        return targetPosition.x >= initiatorPosition.x;
    }
}
