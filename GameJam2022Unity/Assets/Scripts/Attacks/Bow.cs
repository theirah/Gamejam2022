using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bow", menuName = "Weapon/Bow", order = 1)]
public class Bow : Weapon
{
    [SerializeField]
    float spawnDistance; // distance from initiator to spawn arrow
    

    protected override Vector3 GetHitSpawnLocation(Vector3 initiatorPosition, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - initiatorPosition).normalized;
        return initiatorPosition + direction * spawnDistance;
    }

    protected override Quaternion GetHitSpawnRotation(Vector3 initiatorPosition, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - initiatorPosition).normalized;
        return Quaternion.Euler(0, 0, DirectionToAngle(direction));
    }

    protected float DirectionToAngle(Vector3 direction)
    {
        float angleInRadians = Mathf.Atan2(direction.y, direction.x);
        return Mathf.Rad2Deg*angleInRadians;
    }
}
