using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TentacleLine : MonoBehaviour
{
    public LineRenderer _lineRenderer;
    [HideInInspector]
    public GameObject boss;


    void Update()
    {
        _lineRenderer.SetPosition(0, boss.transform.position);
        _lineRenderer.SetPosition(1, transform.position);
        _lineRenderer.enabled = true;
    }

}
