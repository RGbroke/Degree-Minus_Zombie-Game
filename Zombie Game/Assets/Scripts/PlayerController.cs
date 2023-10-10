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

        //Code to test healthbar. Remove later
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.TakeDamage(2);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public void TakeDamage(int damage)
    {
        if(currHealth-damage > 0)
        {
            currHealth -= damage;
        }
        else
        {
            currHealth = 0;
            Destroy(gameObject);
        }
        healthBar.SetHealth(currHealth);
    }
}
