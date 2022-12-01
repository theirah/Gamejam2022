using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroySelfComponent : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
