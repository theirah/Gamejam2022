using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenu : DialogueTrigger
{
    AudioManager audioManager;
    UnityEvent onFinishFade = new UnityEvent();

    private void Awake()
    {
        onFinishFade.AddListener(TriggerDialogue);
    }

    public void PlayGame()
    {
        GameObject fadeScreen = GameObject.FindGameObjectWithTag("FadeScreen");
        fadeScreen.GetComponent<FadeToBlackComponent>().BeginFadeToBlack(2, onFinishFade);
        FadeMusic();
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    void FadeMusic()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayGame();
    }

    public void LoadNextScene()
    {
        //This is the same line from the original "PlayGame()" method, it just gets delayed now.
        SceneManager.LoadScene("Level1");
    }
}
