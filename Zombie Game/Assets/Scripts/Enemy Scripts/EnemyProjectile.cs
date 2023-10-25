using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [HideInInspector]
    public float speed = 10f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    // NOTE * MAKE SURE ALL ENEMIES CONTAIN SUBSTRING "ENEMY"
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Contains("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent))
            {
                playerComponent.TakeDamage(1);
            }
            Destroy(gameObject);
        }
    }
}
