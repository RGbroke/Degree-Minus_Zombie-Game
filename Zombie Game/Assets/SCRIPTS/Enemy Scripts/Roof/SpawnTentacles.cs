using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTentacles : MonoBehaviour
{
    public GameObject tentacle;
    public Transform player;
    [HideInInspector]
    private float lastTime;
    public float triggerTime = 5f;

    private void Start()
    {
        lastTime = Time.time;
    }

    void Update()
    {
        if (lastTime + triggerTime <= Time.time)
        {
            lastTime = Time.time;
            GameObject zom = Instantiate(tentacle, player.position, transform.rotation);
        }
    }
}
