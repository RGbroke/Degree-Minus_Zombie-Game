using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveController_Stage2 : MonoBehaviour
{
    [SerializeField] private PopupSystem notification;
    [SerializeField] private TextMeshProUGUI objectiveDisplay;

    private string[] dialog =
    {
        "Its dark.. need to use my flashlight. Eats up batteries like nothing else, so I'll have find batteries if I want to keep using it",
        "Oh God! What the hell is that thing?",
        "Hmm interesting, a key"
    };

    private float weaponsDiscovered = 0;
    private List<string> objectives = new List<string>();

    void Start()
    {
        stageIntro();
    }

    private void Update()
    {
        printObjectives();
    }

    public void alertPlayer(string dialog)
    {
        notification.PopUp(dialog);
    }

    public void addObjective(string objective)
    {
        objectives.Add(objective);
    }

    private void stageIntro()
    {
        alertPlayer("Crap, the experimental weapons broke. Might be a stretch, but maybe there'll be something I can use in here");
        addObjective("Optional: Discover new weapons");
    }

    private void printObjectives()
    {
        string toDisplay = "<style=\"Title\">Objective:</style>\n\n";
        foreach (string objective in objectives) {
            toDisplay += "<style=\"Normal\">"+ objective +"</style>\n\n";
        }
        objectiveDisplay.text = toDisplay;
    }
}
