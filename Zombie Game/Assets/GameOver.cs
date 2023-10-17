using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI description;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        description.text = score.ToString() + " Zombies killed";
        Time.timeScale = 0f;
    }
    public void RestartButton()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("start");
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
