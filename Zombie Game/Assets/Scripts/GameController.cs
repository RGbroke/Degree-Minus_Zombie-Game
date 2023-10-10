using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController player;
    public GameOver GameOverScreen;
    static int zombiesKilled = 0;

    public void GameOver()
    {
        GameOverScreen.Setup(zombiesKilled);
    }

    public void Update()
    {
        if(player.currHealth <= 0)
        {
            player.healthBar.setActive(false);
            GameOver();
        }
    }

    public void gainScore()
    {
        zombiesKilled++;
    }
}
