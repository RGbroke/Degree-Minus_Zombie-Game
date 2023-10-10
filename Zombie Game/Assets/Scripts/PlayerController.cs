using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Healthbar stuff
    public int maxHealth = 10;
    public HealthBar healthBar;

    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    public int currHealth;
    public int score = 0;

    bool hit = false;
    public float damageRate = 1f;
    float lastDamageTime;
    int damageTaken;

    Vector2 moveDirection;

    void Start()
    {
        currHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if(Time.time > lastDamageTime + damageRate && hit)
        {
            lastDamageTime = Time.time;
            if (currHealth - damageTaken > 0)
            {
                currHealth -= damageTaken;
            }
            else
            {
                currHealth = 0;
            }
            healthBar.SetHealth(currHealth);
        }
        hit = false;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public void TakeDamage(int damage)
    {
        damageTaken = damage;
        hit = true;
    }
}
