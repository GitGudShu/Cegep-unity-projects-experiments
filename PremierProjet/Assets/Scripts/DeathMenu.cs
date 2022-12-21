using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void TryAgain_Click()
    {
        Debug.Log("TryAgain");

        // Reload scene
        SceneManager.LoadScene("Demo", LoadSceneMode.Single);

        // Unpause the game
        Time.timeScale = 1;
    }

    public void MainMenu_Click()
    {
        // Load main menu scene
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }
}
