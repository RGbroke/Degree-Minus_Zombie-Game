using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleHead : MonoBehaviour
{
    [HideInInspector]
    public int damage = 5;
    [HideInInspector]
    public GameObject tentacleSegment;
    private float lastTime;
    [HideInInspector]
    public float addSegmentTime = 1f;
    [HideInInspector]
    public float timeToDestroy = 5f;

    private void Start()
    {
        lastTime = Time.time;
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        if (lastTime + addSegmentTime <= Time.time)
        {
            lastTime = Time.time;
            GameObject segment = Instantiate(tentacleSegment, transform.position, transform.rotation);
            Destroy(segment, timeToDestroy);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject ob = collision.gameObject;
        if (ob.tag == "Player")
        {
            PlayerController player = ob.GetComponent<PlayerController>();
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
