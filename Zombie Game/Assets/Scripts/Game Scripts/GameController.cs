using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController player;
    public GameOver GameOverScreen;
    public static int zombiesKilled = 0;
    public static int activeZombies = 0;

    
    [SerializeField] private PopupSystem popup;
    [SerializeField] private TutorialScript tutorial;
    public int maxZombies = 100;


    
    public void Start()
    {
        zombiesKilled = 0;
        activeZombies = 0;
    }

    public void GameOver()
    {
        GameOverScreen.Setup(zombiesKilled);
    }

    public void Update()
    {


        if(player.getHealth() <= 0)
        {
            player.healthBar.setActive(false);
            GameOver();
        }
        if(Input.GetKeyDown(KeyCode.G) && Time.timeScale == 0)
        {
            tutorialResume();
        }
    }
    public void zombieKilled()
    {
        zombiesKilled++;
        activeZombies--;
    }

    public void addActiveZombies(int num)
    {
        activeZombies += num;
    }

    public int getMaxZombies()
    {
        return maxZombies;
    }

    public int numActiveZombies()
    {
        return activeZombies;
    }

    public int numZombiesKilled()
    {
        return zombiesKilled;
    }

    public void tutorialResume()
    {
        popup.animator.SetTrigger("close");
        Time.timeScale = 1;
    }
}
