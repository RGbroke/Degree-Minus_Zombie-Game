using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleHead : MonoBehaviour
{
    [HideInInspector]
    public int damage = 5;

    [HideInInspector]
    public float timeToDestroy = 5f;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
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
