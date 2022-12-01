using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelComponent : MonoBehaviour
{
    public string levelName; // Name of level to load when LoadLevel is called
    public void LoadLevel()
    {
        if (levelName.Length > 0)
        {
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            SceneManager.LoadScene(levelName);

        }
    }
}
