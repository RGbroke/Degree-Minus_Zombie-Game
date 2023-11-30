using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWall : MonoBehaviour
{
    public GameObject wall;
    [HideInInspector]
    public bool on = false;
    private float lastTime;
    public float triggerTime = 5f;

    [HideInInspector]
    public float used = 0;

    private void Start()
    {
        lastTime = Time.time;
    }

    void Update()
    {
        if (!on && lastTime + triggerTime <= Time.time)
        {
            used++;
            on = true;
            lastTime = Time.time;
            wall.SetActive(true);
        }
    }
}
