using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectiveAdder : MonoBehaviour
{
    [SerializeField] private ObjectiveController_Stage2 objectiveControl;
    [SerializeField] private string dialogOnCollision;
    [SerializeField] private string objective;
    private bool played = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player" || played)
            return;

        objectiveControl.alertPlayer(dialogOnCollision);
        objectiveControl.addObjective(objective);
        played = true;
    }
}
