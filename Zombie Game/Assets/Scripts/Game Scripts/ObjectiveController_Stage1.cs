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
        objectiveDisplay.text = "Objective:\n - Clear Area of Zombies (" + gc.numZombiesKilled() + "/" + zombiesToKill +")";
        if (gc.numZombiesKilled() >= zombiesToKill)
        {
            objectiveDisplay.text += "\n - Enter The Hospital";
            objectiveComplete();
        }
    }

    private void objectiveComplete()
    {
        trigger.SetActive(true);
    }



}
