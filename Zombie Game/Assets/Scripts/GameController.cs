using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController player;
    public GameOver GameOverScreen;
    static int zombiesKilled = 0;
    public bool firstZombie = false;
    public bool secondZombie = false;
    static int activeZombies = 0;

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

    public void firstZombieSeen()
    {
        firstZombie = true;
    }

    public void secondZombieSeen()
    {
        secondZombie = true;
    }
}
