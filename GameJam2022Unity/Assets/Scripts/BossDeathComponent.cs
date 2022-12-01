using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeathComponent : DialogueTrigger
{
    public void Die()
    {
        GameObject fadeScreen = GameObject.FindGameObjectWithTag("FadeScreen");
        fadeScreen.GetComponent<FadeToBlackComponent>().BeginFadeToBlack(2, null);
        TriggerDialogue();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
