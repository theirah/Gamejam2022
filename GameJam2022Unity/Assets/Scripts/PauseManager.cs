using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    List<MonoBehaviour> pausedComponents = new List<MonoBehaviour>();
    bool paused = false;

    public void PauseAll()
    {
        if (paused) return;

        Time.timeScale = 0;
        GameObject[] allGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject go in allGameObjects)
        {
            MonoBehaviour[] components = go.GetComponentsInChildren<MonoBehaviour>();
            foreach (MonoBehaviour comp in components)
            {
                if (comp is IPauseable)
                {
                    if (comp.enabled)
                    {
                        pausedComponents.Add(comp);
                        comp.enabled = false;
                    }
                }
            }
        }
        paused = true;
    }

    public void UnpauseAll()
    {
        Time.timeScale = 1;
        foreach (MonoBehaviour comp in pausedComponents)
        {
            comp.enabled = true;
        }
        pausedComponents.Clear();
        paused = false;
    }
}
