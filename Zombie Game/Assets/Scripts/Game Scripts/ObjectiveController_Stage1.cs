using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveController_Stage1 : MonoBehaviour
{
    [SerializeField]
    private GameController gc;

    [SerializeField]
    private TextMeshProUGUI objectiveDisplay;

    [SerializeField]
    private GameObject trigger;

    [SerializeField]
    private int zombiesToKill;

    private bool objectiveFlag = false;


    private void Update()
    {

        objectiveDisplay.text = "<style=\"Title\">Objective:</style>\n";
        if (gc.numZombiesKilled() >= zombiesToKill)
        {
            objectiveDisplay.text += " - Clear Area of Zombies (" + gc.numZombiesKilled() + "/" + zombiesToKill + ")";
            objectiveDisplay.text += " - Enter The Hospital";
            objectiveComplete();
        }
        else
        {
            objectiveDisplay.text += " - Clear Area of Zombies (" + gc.numZombiesKilled() + "/" + zombiesToKill + ")";
        }
    }

    private void objectiveComplete()
    {
        trigger.SetActive(true);
    }



}
