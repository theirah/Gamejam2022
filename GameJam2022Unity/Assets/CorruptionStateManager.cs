using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            // If we're back at the main menu, reset corruption (probably just beat the game)
            if (SceneManager.GetActiveScene().name=="Mainmenu")
            {
                singleton.corruptionLevel = 0;
            }
            Destroy(this);
        }
    }
}
