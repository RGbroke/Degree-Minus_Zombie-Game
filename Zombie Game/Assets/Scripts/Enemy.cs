using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private float health;
    public float speed;
    public float maxHealth = 3f;
    public Animator animator;
    bool isColliding = false;
    PlayerController player;
    public float damageRate;
    float lastDamageTime;

    public GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        animator.SetFloat("Horizontal", target.position.x - transform.position.x);
        animator.SetFloat("Speed", speed);

        if (isColliding && Time.time > lastDamageTime + damageRate)
        {
            lastDamageTime = Time.time;
            player.TakeDamage(1);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            gc.gainScore();
            Destroy(gameObject);
            PlayerController scorekeep = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            scorekeep.score += 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent))
        {
            isColliding = true;
            player = playerComponent;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent))
        {
            isColliding = false;
            playerComponent.TakeDamage(1);
        }
    }
}