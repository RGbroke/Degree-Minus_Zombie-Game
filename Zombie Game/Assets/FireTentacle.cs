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
    public float speed = 10f;

    public int damage = 5;
    public GameObject tentacleSegment;
    public float addSegmentTime = 1f;
    public float timeToDestroy = 5f;

    private void Start()
    {
        lastTime = Time.time;
    }

    void Update()
    {
        if (lastTime + triggerTime <= Time.time)
        {
            lastTime = Time.time;

            Vector2 targetDirection = player.position - transform.position;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
            GameObject tentaclehead = Instantiate(tentacle, transform.position, q);
            TentacleHead tentacleObj = tentacle.GetComponent<TentacleHead>();
            tentacleObj.damage = damage;
            tentacleObj.tentacleSegment = tentacleSegment;
            tentacleObj.addSegmentTime = addSegmentTime;
            tentacleObj.timeToDestroy = timeToDestroy;
            Rigidbody2D rb = tentaclehead.GetComponent<Rigidbody2D>();
            rb.velocity = rb.transform.up * speed;
        }
    }
}
