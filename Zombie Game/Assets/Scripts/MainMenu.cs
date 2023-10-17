using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadGame()
    {
        //Temporary Code
        this.NewGame();
        //Has to pull saved scene indicies from a menu or something
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
