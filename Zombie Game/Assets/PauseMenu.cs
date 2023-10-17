using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject GameOver;

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Menu");
        Resume();
    }

    public void Pause()
    {
        if (!GameOver.activeSelf)
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }
}
