using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController player;
    public GameOver GameOverScreen;
    public static int zombiesKilled = 0;
    public static int activeZombies = 0;
    public bool firstZombie = false;
    public bool secondZombie = false;
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

        if(Input.GetKeyDown(KeyCode.G))
        {
            if(popup.animator.GetCurrentAnimatorStateInfo(0).IsName("pop"))
                {
                Time.timeScale = 1;
                popup.animator.SetTrigger("close");
                }
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

    public void firstZombieSeen()
    {
        firstZombie = true;
        string text = "Type: Normal Zombie \nHP: Medium \nDamage: Medium \nSpeed: High";
        popup.PopUp(text, 0);
    }

    public void secondZombieSeen()
    {
        secondZombie = true;
        string text = "Type: Spitter Zombie \nHP: Low \nDamage: Medium \nSpeed: Medium";
        popup.PopUp(text, 0);
    }

    
}
