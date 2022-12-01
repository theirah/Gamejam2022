using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionStateManager : MonoBehaviour
{
    public int corruptionLevel = 0;
    public static CorruptionStateManager singleton;

    private void Awake()
    {
        if (!singleton)
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
