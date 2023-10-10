using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyFollow : MonoBehaviour
{
    private Transform target;
    private float health;
    public float speed;
    public float maxHealth = 3f;
    public Animator animator;
    public Rigidbody2D zombie;

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
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
