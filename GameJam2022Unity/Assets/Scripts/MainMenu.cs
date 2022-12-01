using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioManager audioManager;
    public void PlayGame()
    {
        GameObject fadeScreen = GameObject.FindGameObjectWithTag("FadeScreen");
        fadeScreen.GetComponent<FadeToBlackComponent>().BeginFadeToBlack(2, null);
        StartCoroutine(FadeMusicThenStart());
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    IEnumerator FadeMusicThenStart()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayGame();
        yield return new WaitForSeconds(2f);
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        //This is the same line from the original "PlayGame()" method, it just gets delayed now.
        SceneManager.LoadScene("Level1");
    }
}
