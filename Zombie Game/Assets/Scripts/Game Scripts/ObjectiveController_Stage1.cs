using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
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

    private bool mainObj = false;
    private int objectiveFlag = 0;
    [SerializeField]
    private PopupSystem popup; /*Leave this here for now*/

    private void Update()
    {

        objectiveDisplay.text = "<style=\"Title\">Objective:</style>\n\n";
        switch (objectiveFlag)
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
        }
    }
    public void objectiveComplete()
    {
        objectiveFlag++;
    }



}
