using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioManager audioManager;
    public void PlayGame()
    {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
