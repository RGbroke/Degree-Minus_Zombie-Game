using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressObjective : MonoBehaviour
{
    [SerializeField] string objectiveTag;
    [SerializeField] ObjectiveController_Stage2 objectiveControl;

    private void OnEnable()
    {
        objectiveControl.getObjective(objectiveTag).progressObjective();
    }
}
