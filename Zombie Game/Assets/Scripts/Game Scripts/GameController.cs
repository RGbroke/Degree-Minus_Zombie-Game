using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerController player;
    public GameOver GameOverScreen;
    static int zombiesKilled = 0;
    public bool firstZombie = false;
    public bool secondZombie = false;
    static int activeZombies = 0;

    public TextMeshProUGUI objectiveTracker;
    public GameObject deactivator;

    public void Start()
    {
        zombiesKilled = 0;
        activeZombies = 0;
        objectiveTracker.text = "Objective: Kill Zombies 0/20";
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
	    if(zombiesKilled >= 20)
	    {
            objectiveTracker.text = "Objective: Escape Into the Hospital!";
            deactivator.SetActive(true);
	    }
        else
        {
            objectiveTracker.text = "Objective: Kill Zombies " + zombiesKilled + "/20";
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
