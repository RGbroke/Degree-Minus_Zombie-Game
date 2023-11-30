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
    public float xLeft = -40f;
    public float xRight = 5f;
    public float yUp = 0f;
    public float yDown = -26f;
    public float randomness = 0.04f;

    private void Start()
    {
        lastTime = Time.time;
    }

    void Update()
    {
        if (lastTime + triggerTime <= Time.time)
        {
            lastTime = Time.time;
            Vector2 adj = player.position;
            adj.y += 3.5f;

            Instantiate(tentacle, adj, transform.rotation);
        }
        if (lastTime + triggerTime + Random.Range(-randomness, 0f) <= Time.time)
        {
            Instantiate(tentacle, new Vector2(Random.Range(xLeft, xRight), Random.Range(yDown, yUp)), transform.rotation);
        }
    }
}
