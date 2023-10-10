using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform target;
    public float speed;
    [SerializeField] float health, maxHealth = 3f;
    private Animator animator;
    private bool isMovingLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction of movement
        Vector3 moveDirection = (target.position - transform.position).normalized;
        isMovingLeft = moveDirection.x < 0;

        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        animator.SetFloat("Speed", distanceToTarget > 0.1f ? 1f : 0f);

        // Set move direction for left and right
        animator.SetBool("isMovingLeft", isMovingLeft);
        animator.SetBool("isMovingRight", !isMovingLeft);

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
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
