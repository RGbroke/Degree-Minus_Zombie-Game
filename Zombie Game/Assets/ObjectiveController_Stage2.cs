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

    private Dictionary<string,Objective> objectives = new Dictionary<string, Objective>();

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
        alertPlayer("Crap, the experimental weapons broke. Might be a stretch, but maybe there'll be something I can use in here");
        addObjective("discoverWeapons", new Objective("Optional: Discover new weapons", 3));
    }

    private void printObjectives()
    {
        string toDisplay = "<style=\"Title\">Objectives:</style>\n\n";
        foreach (KeyValuePair<string, Objective> objective in objectives)
        { 
            if (objective.Value.isObjectiveComplete()) 
            { 
                toDisplay += "<style=\"Complete\">" + objective.Value.getDescription() + "</style>\n\n";
                if(objective.Value.reduceTime(Time.deltaTime) < 0)
                {
                    removeObjective(objective.Key);
                    break;
                }
                continue;
            }
            toDisplay += "<style=\"Normal\">"+ objective.Value.getDescription() + "</style>\n\n";
        }
        objectiveDisplay.text = toDisplay;
    }
}
