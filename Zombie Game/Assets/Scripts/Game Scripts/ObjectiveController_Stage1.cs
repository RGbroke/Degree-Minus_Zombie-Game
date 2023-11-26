using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveController_Stage1 : MonoBehaviour
{
    [SerializeField]
    private PlayerController pc;
    [SerializeField]
    private GameController gc;
    [SerializeField]
    private TextMeshProUGUI objectiveDisplay;
    [SerializeField] 
    private PopupSystem popup;

    [SerializeField]
    private GameObject trigger;

    [SerializeField]
    private int zombiesToKill;

    [SerializeField]
    private TutorialScript tutorial;
    [SerializeField]
    private GameObject activator;
    
    public bool objectiveCheck;
    public int objectiveFlag = 0;

    [SerializeField]
    public Weapon weapon;
    public int gunAttackCount = 0;
    public int normalAttackCount = 0;
    public int grenadeAttackCount = 0;
    public bool hpObtained = false;
    public bool enemyKilled = false;
    
    private bool mainObj = false;
    private string text;
   
    private bool missionPopup = false;
    private bool missionCompletePopup = false;

    private void Update()
    {
        objectiveDisplay.text = "<style=\"Title\">Objective:</style>\n\n";
        objectiveDisplay.text += text;

        if(tutorial.tutorialNext == false || tutorial.currObjective > 2)
        {
            switch (objectiveFlag)
            {
                case 1:
                    gunAttack();
                    break;
                case 2: 
                    meleeAttack();
                    break;
                case 3:
                    grenadeAttack();
                    break;
                case 4:
                    if (!missionPopup)
                    {
                        popup.PopUp("That doesn't sound good... Looks like I'll have to kill as many zombies as I can before I enter. This many zombies trailing me in such a tight space would be a death sentence");
                        missionPopup = true;
                    }
                    text = "<style=\"Normal\">Zombies Approach! Clear the Area to get inside! (" + gc.numZombiesKilled() + "/???)</style>";
                    break;
                case 5:
                    if (!missionCompletePopup)
                    {
                        popup.PopUp("What the hell!? I've killed damn near a thousand and there's still more! Looks like I'll just have to get in and take my chances with baracading the door.");
                        missionCompletePopup = true;
                    }
                    text = "<style=\"Normal\">There are just too many! Just get into the Hospital</style>";
                    trigger.SetActive(true);
                    break;    
            }
        }

        if(gc.numZombiesKilled() >= zombiesToKill && !mainObj)
        {
            mainObj = true;
            objectiveComplete();
        }
    }

    public void objectiveComplete()
    {
        objectiveFlag++;
    }

    public void tutorialComplete()
    {
        tutorial.currObjective++;
        tutorial.objectiveFinished = true;
        tutorial.tutorialNext = true;
    }


    public void gunAttack()
    {
        if(weapon.bulletShot < 5)
            {
                objectiveCheck = false;
                text = "<style=\"Normal\">Fire your gun 5 times: ("+ weapon.bulletShot + "/5)</style>";
            }
        if(weapon.bulletShot == 5 && objectiveCheck == false)
            {
                objectiveCheck = true;
                text = "<style=\"Normal\">Completed </style>";
                tutorialComplete();
            }
    }

    public void meleeAttack()
    {
        if(weapon.meleeCount < 3)
            {
                objectiveCheck = false;
                text = "<style=\"Normal\">Use a melee attack 3 times: ("+ weapon.meleeCount + "/3)</style>";
            }
        if(weapon.meleeCount == 3 && objectiveCheck == false)
            {
                objectiveCheck = true;
                text = "<style=\"Normal\">Completed </style>";
                tutorialComplete();
            }
    }

    public void grenadeAttack()
    {
        if(weapon.grenadeThrew < 2)
            {   
                objectiveCheck = false;
                text = "<style=\"Normal\">Throw a grenade 2 times: ("+ weapon.grenadeThrew + "/2)</style>";
            }
        if(weapon.grenadeThrew == 2 && objectiveCheck == false)
             {
                objectiveCheck = true;
                text = "<style=\"Normal\">Tutorial Completed!\n Find the entrance to the hospital. </style>";
                tutorialComplete();
                activator.SetActive(true);
            }
    }

    public void enemySpawned()
    {
        objectiveComplete();
    }

}
