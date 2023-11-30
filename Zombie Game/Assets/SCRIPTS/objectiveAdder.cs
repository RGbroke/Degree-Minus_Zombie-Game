using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectiveAdder : MonoBehaviour
{
    [SerializeField] private ObjectiveController_Stage2 objectiveControl;
    [SerializeField] private string dialogOnCollision;
    [SerializeField] private string objectiveTag;
    [SerializeField] private string objectiveDescriptor;
    [SerializeField] private int stepsToComplete;
    private bool played = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player" || played)
            return;
        objectiveControl.alertPlayer(dialogOnCollision);
        objectiveControl.addObjective(objectiveTag, new Objective(objectiveDescriptor, stepsToComplete));
        played = true;
    }
}
