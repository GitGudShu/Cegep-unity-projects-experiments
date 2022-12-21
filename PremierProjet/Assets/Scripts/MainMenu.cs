using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0;
    }

    public void Play_Click()
    {
        // Load the game scene
        SceneManager.LoadScene("Demo", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
