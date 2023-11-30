using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveController_Stage3 : MonoBehaviour
{
    [SerializeField] private PopupSystem notification;
    [SerializeField] private TextMeshProUGUI objectiveDisplay;
    [SerializeField] private GameObject[] elevator;
    [SerializeField] private GameObject cure;

    private bool elevatorOpen = false;


    private Dictionary<string, Objective> objectives = new Dictionary<string, Objective>();
    private void Update()
    {
        printObjectives();
        if(getObjective("escape") != null && !elevatorOpen)
            openElevator();
    }

    public void findCure() {
        alertPlayer("*GASP* Well if the doc isn't gonna be any help, I guess I'll find the cure on my own..");
        addObjective("cure", new Objective("Find the Cure"));
        if(cure != null)
            cure.SetActive(true);
    }
    public void openElevator()
    {
        getObjective("cure").completeObjective();
        elevator[0].SetActive(false);
        elevator[1].SetActive(true);
        elevator[2].SetActive(true);
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
            toDisplay += "<style=\"Normal\">" + objective.Value.getDescription() + "</style>\n\n";
        }
        objectiveDisplay.text = toDisplay;
    }
}
