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
    private PopupSystem popup; /*Leave this here for now*/
    [SerializeField]
    public Weapon weapon;
    public int gunAttackCount = 0;
    public int normalAttackCount = 0;
    public int grenadeAttackCount = 0;
    public bool hpObtained = false;
    public bool enemyKilled = false;
    
    private bool mainObj = false;
    private string text;
   

    private void Update()
    {
        objectiveDisplay.text = "<style=\"Title\">Objective:</style>\n\n";
        objectiveDisplay.text += text;
        /*switch (objectiveFlag)
        {
            case 2:
                objectiveDisplay.text += "<style=\"Normal\">Good Job! Now Enter The Hospital</style>";
                trigger.SetActive(true);
                break;
            case 1:
                objectiveDisplay.text += "<style=\"Normal\">Zombies Approach! Clear the Area to get inside! (" + gc.numZombiesKilled() + "/" + zombiesToKill + ")</style>";
                break;
            case 0:
                objectiveDisplay.text += "<style=\"Normal\">Try to Enter the Hospital</style>\n";
                break;
        }
        if(gc.numZombiesKilled() >= zombiesToKill && !mainObj)
        {
            mainObj = true;
            objectiveComplete();
        }*/
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
                    text = "<style=\"Normal\">Zombies Approach! Clear the Area to get inside! (" + gc.numZombiesKilled() + "/" + zombiesToKill + ")</style>";
                    break;
                case 5:
                    text = "<style=\"Normal\">Good Job! Now Enter The Hospital</style>";
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
                text = "<style=\"Normal\">Use gun attack 5 times: ("+ weapon.bulletShot + "/5)</style>";
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
        if(weapon.meleeCount < 5)
            {
                objectiveCheck = false;
                text = "<style=\"Normal\">Use melee attack 5 times: ("+ weapon.meleeCount + "/5)</style>";
            }
        if(weapon.meleeCount == 5 && objectiveCheck == false)
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
                text = "<style=\"Normal\">Throw grenade 2 times: ("+ weapon.grenadeThrew + "/2)</style>";
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

    /*
    public void healthKit()
    {
        objectiveComplete();
    }


    public void enterHospital()
    {
        objectiveComplete();
    }
    */

    


}
