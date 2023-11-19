using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveController_Stage2 : MonoBehaviour
{
    [SerializeField] private PopupSystem notification;
    [SerializeField] private TextMeshProUGUI objectiveDisplay;

    
    private string[] dialog =
    {
        "Its dark.. need to use my flashlight. Eats up batteries like nothing else, so I'll have to use it sparingly",
        "Crap, the experimental weapons broke. Too bad, would probably have been useful in here",
        "A shotgun? Might be useful, maybe there are more guns around here",
        "An smg! Some sort of SWAT unit must've dropped this here",
        "An RPG??? What type of hospital is this!?",
        "Oh God! What the hell is that thing?",
    };

    private float weaponsDiscovered = 0;
    private string[] objectives = 
    {
        "Optional: Find batteries for your flashlight",
        "Optional: Discover new weapons",
        "",
        "Defeat the bear!"
    };

    private bool[] objectiveAdded = new bool[4];

    void Start()
    {
        for (int i = 0; i < objectiveAdded.Length; i++)
        {
            objectiveAdded[i] = false;
        }
        stageIntro();
    }

    private void stageIntro()
    {
        notification.PopUp(dialog[0]);
        //notification.PopUp(dialog[1]);
    }

    private void printObjectives()
    {
        string toDisplay = "<style=\"Title\">Objective:</style>\n\n";
        for (int i = 0; i < objectives.Length; i++) {
            if (!objectiveAdded[i])
                return;
            toDisplay += "<style=\"Normal\">"+ objectives[i] +"</style>\n";
        }
        objectiveDisplay.text = toDisplay;
    }
}
