using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCompleter : MonoBehaviour
{
    [SerializeField] private ObjectiveController_Stage3 objectiveControl;
    [SerializeField] private string objectiveTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player" || objectiveControl.getObjective(objectiveTag) == null)
            return;

        objectiveControl.getObjective(objectiveTag).completeObjective();
    }
}
