using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerDeathComponent : MonoBehaviour
{
    public void Die()
    {
        FindObjectOfType<PauseManager>().PauseAll();
        UnityEvent afterFadeEvent = new UnityEvent();
        afterFadeEvent.AddListener(Unpause);
        afterFadeEvent.AddListener(ReloadLevel);
        GameObject fadeScreen = GameObject.FindGameObjectWithTag("FadeScreen");
        fadeScreen.GetComponent<FadeToBlackComponent>().BeginFadeToBlack(2, afterFadeEvent);
    }

    protected void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    protected void Unpause()
    {
        FindObjectOfType<PauseManager>().UnpauseAll();
    }
}
