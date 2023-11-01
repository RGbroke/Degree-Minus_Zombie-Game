using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController player;
    public GameOver GameOverScreen;
    public static int zombiesKilled = 0;
    public static int activeZombies = 0;
    public PopupSystem popup;
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
    }

    public void gainScore()
    {
        zombiesKilled++;
        activeZombies--;
    }

    public void addActiveZombies(int num)
    {
        activeZombies += num;
    }

    public int numActiveZombies()
    {
        return activeZombies;
    }

    public int numZombiesKilled()
    {
        return zombiesKilled;
    }
}
