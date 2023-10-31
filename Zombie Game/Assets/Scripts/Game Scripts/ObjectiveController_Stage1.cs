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


    private void Update()
    {

        objectiveDisplay.text = "<style=\"Title\">Objective:</style>\n\n";
        if (gc.numZombiesKilled() >= zombiesToKill)
        {
            objectiveDisplay.text += "<style=\"Complete\">Clear Area of Zombies (" + gc.numZombiesKilled() + "/" + zombiesToKill + ")</style>\n";
            objectiveDisplay.text += "<style=\"Normal\">Enter The Hospital</style>";
            objectiveComplete();
        }
        else
        {
            objectiveDisplay.text += "<style=\"Normal\">Clear Area of Zombies (" + gc.numZombiesKilled() + "/" + zombiesToKill + ")</style>";
        }
    }

    private void objectiveComplete()
    {
        trigger.SetActive(true);
    }



}
