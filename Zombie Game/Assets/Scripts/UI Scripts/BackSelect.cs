using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackSelect : MonoBehaviour
{
    public Button firstSelect;
    void OnEnable()
    {
        firstSelect.Select();
    }
}
