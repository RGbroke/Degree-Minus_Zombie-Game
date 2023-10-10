using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI description;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        PlayerController scorekeep = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        description.text = scorekeep.score.ToString() + " Zombies \"Oof'd\"";
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("start");
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
