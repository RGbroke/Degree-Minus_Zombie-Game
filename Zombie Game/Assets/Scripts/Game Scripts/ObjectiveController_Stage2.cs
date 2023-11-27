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
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject carpetInteraction;
    [SerializeField] private GameObject levelSwitch;


    public Dictionary<string,Objective> objectives = new Dictionary<string, Objective>();


    void Start()
    {
        stageIntro();
    }

    private void Update()
    {
        printObjectives();
    }

    public void bossKilled()
    {
        key.SetActive(true);
    }
    public void keyObtained()
    {
        carpetInteraction.SetActive(true);
    }
    public void carpetMoved()
    {
        levelSwitch.SetActive(true);
    }



    public void alertPlayer(string dialog)
    {
        notification.PopUp(dialog);
    }

    public void addObjective(string objectiveTag, Objective objective)
    {
        objectives.Add(objectiveTag, objective);
    }

    public void removeObjective(string objectiveTag)
    {
        objectives.Remove(objectiveTag);
    }

    public Objective getObjective(string objectiveTag)
    {
        if (!objectives.ContainsKey(objectiveTag))
            return null;

        return objectives[objectiveTag];
    }

    private void stageIntro()
    {
        alertPlayer("Phew.. that was close. Don't wanna stick around to see if the door will hold. Had to ditch the heavy equipment so I'll have to make due");
        addObjective("discoverWeapons", new Objective("Optional: Discover new weapons", 3));
    }

    private void printObjectives()
    {
        string toDisplay = "<style=\"Title\">Objectives:</style>\n\n";
        foreach (KeyValuePair<string, Objective> objective in objectives)
        { 
            if (objective.Value.isObjectiveComplete()) 
            {
                if (objective.Value.reduceTime(Time.deltaTime) < 0)
                {
                    continue;
                }
                toDisplay += "<style=\"Complete\">" + objective.Value.getDescription() + "</style>\n\n";
                continue;
            }
            toDisplay += "<style=\"Normal\">"+ objective.Value.getDescription() + "</style>\n\n";
        }
        objectiveDisplay.text = toDisplay;
    }
}
