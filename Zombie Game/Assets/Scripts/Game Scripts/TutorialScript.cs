using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class TutorialScript : MonoBehaviour
{   

  
    private string[] objectives = {"Gun Attack", "Normal Attack", "Grenade", "HP Bar", "Health Kit", "Enemy Spawn", "Enter Hospital"};
    /*private List<(string, double)> tutorialList = new List<(string, double)>();*/
    /*private List<(string, double)> objectiveList = new List<(string, double)>();*/
    private List<string> tutorialList = new List<string>();
    private List<string> objectiveList = new List<string>();
    [SerializeField] private PopupSystem popup;
    [SerializeField] ObjectiveController_Stage1 go;
    public double currObjective = 0;
    public bool tutorialNext;
    public bool objectiveFinished;

   
    void Start()
    {
        /*tutorialList.Add(("Each weapon has a different normal attack", 0));
        tutorialList.Add(("Press Left-Mouse Button to fire, manage ammo wisely.", 0));
        tutorialList.Add(("When enemy approaches, use Right-Mouse Button to attack", 1));
        tutorialList.Add(("Grenade can be threw at interval, dealing massive area of effect damage.", 2));
        tutorialList.Add(("Press Spacebar to throw a grenade at the front.", 2));
        tutorialList.Add(("Health bar can be seen at the bottom of the screen.", 3));
        tutorialList.Add(("The soldier will die when the bar is depleted.", 3));
        tutorialList.Add(("You have been damaged, however, random HP kits can be found!", 3));
        tutorialList.Add(("Pick up an HP kit to restore HP.", 3));
        tutorialList.Add(("Enemies have been spooted!!!", 4));
        tutorialList.Add(("Each enemy can have different health, attack, and speed. Be on guard.", 4));
        tutorialList.Add(("Find the entrance to the hospital", 5));
        tutorialList.Add(("The hospital is locked, find the key to the hospital!", 5));
        objectiveList.Add(("Fire bullet 5 times", 0));
        objectiveList.Add(("Auto-attack 5 times", 1));
        objectiveList.Add(("Throw grenade 3 times", 2));
        objectiveList.Add(("Pick up HP kit", 3));
        objectiveList.Add(("Eliminate 2 zombies", 4));
        objectiveList.Add(("Eliminate all zombies", 5));*/
        tutorialList.Add("Each weapon has a different attack.\nPress/Hold Left-Mouse button (Or Right Trigger) to fire, manage your ammo wisely.");
        tutorialList.Add("When enemies get too close, use the Right-Mouse Button (Or Right Bumper) to do a melee attack.");
        tutorialList.Add("Grenade can be thrown once every 2 seconds, dealing massive area of effect damage.\n Press the Spacebar (Or Left Trigger) to throw a grenade.");
        /*tutorialList.Add("Health bar can be seen at the bottom of the screen. The soldier will die when the bar is depleted.");
        tutorialList.Add("You have been damaged, however, random HP kits can be found to restore HP.\nPick up an HP kit to restore HP.");*/
        tutorialList.Add("An enemy has been spotted!!!\nEach enemy can have different health, attack, and speed. Be on guard!");
        tutorialList.Add("Find the entrance to the hospital.");
        /*popup.PopUp("Welcome to the tutorial stage. We will be guiding you through the basic combat system!", 0);*/
        tutorialNext = true;
        objectiveFinished = true;
    }


    // Update is called once per frame
    void Update()
    {
      if(tutorialNext && objectiveFinished && currObjective < 3)
        {
          StartCoroutine(tutorialStart());
        }
    }

    IEnumerator tutorialStart()
    {
       tutorialNext = false;
       objectiveFinished = false;
       switch(currObjective)
        {
            case 0:
              yield return new WaitWhile(() => popup.isDisplayed());
              popup.PopUp(tutorialList[0]); 
              yield return new WaitForSeconds(1);
              go.objectiveFlag++;
              go.weapon.bulletShot = 0;
              break;
            case 1:
              yield return new WaitWhile(() => popup.isDisplayed());
              popup.PopUp(tutorialList[1]);
              yield return new WaitForSeconds(1);
              go.objectiveFlag++;
              go.weapon.meleeCount = 0;
              break;
            case 2:
              yield return new WaitWhile(() => popup.isDisplayed());
              popup.PopUp(tutorialList[2]);
              yield return new WaitForSeconds(1);
              go.weapon.grenadeThrew = 0;
              go.objectiveFlag++;
              break;
        }
    }

   public void nextTutorial()
   {
      currObjective++;
   }
}
