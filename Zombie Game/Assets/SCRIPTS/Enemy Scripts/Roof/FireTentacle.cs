using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class FireTentacle : MonoBehaviour
{
    public GameObject tentacle;
    public Transform player;
    [HideInInspector]
    private float lastTime;
    public float triggerTime = 5f;
    public float spinRate = 1f;
    public float spinAngle = 17f;
    public float spinTimes = 20f;
    public float incrementAngleTime = 1f;
    public float speed = 10f;

    public int damage = 5;
    public float timeToDestroy = 5f;

    private int fireTimes = 0;
    private int i = 0;
    public int maxFireTimes = 5;
    public int multiTentaclesNum = 8;

    private bool busy = false;

    [HideInInspector]
    public float used = 0;

    private void Start()
    {
        lastTime = Time.time;
    }

    void Update()
    {
        if (lastTime + triggerTime <= Time.time && fireTimes < maxFireTimes)
        {
            lastTime = Time.time;

            Vector2 targetDirection = player.position - transform.position;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
            GameObject tentaclehead = Instantiate(tentacle, transform.position, q);
            TentacleHead tentacleObj = tentacle.GetComponent<TentacleHead>();
            TentacleLine tentacleLine = tentaclehead.GetComponent<TentacleLine>();
            tentacleObj.damage = damage;
            tentacleObj.timeToDestroy = timeToDestroy;
            tentacleLine.boss = gameObject;
            Rigidbody2D rb = tentaclehead.GetComponent<Rigidbody2D>();
            rb.velocity = rb.transform.up * speed;

            fireTimes++;
        }
        if (lastTime + spinRate <= Time.time && fireTimes >= maxFireTimes)
        {
            lastTime = Time.time;

            for (int j = 0; j < multiTentaclesNum; j++)
            {
                GameObject tentaclehead = Instantiate(tentacle, transform.position, transform.rotation * Quaternion.Euler(0, 0, ((360f / multiTentaclesNum) * j) + spinAngle * i));
                TentacleHead tentacleObj = tentacle.GetComponent<TentacleHead>();
                TentacleLine tentacleLine = tentaclehead.GetComponent<TentacleLine>();
                tentacleObj.damage = damage;
                tentacleObj.timeToDestroy = timeToDestroy;
                tentacleLine.boss = gameObject;
                Rigidbody2D rb = tentaclehead.GetComponent<Rigidbody2D>();
                rb.velocity = rb.transform.up * speed;
            }

            i++;
        }
        if(i >= spinTimes)
        {
            i = 0;
            fireTimes = 0;
            used++;
        }
    }
}
