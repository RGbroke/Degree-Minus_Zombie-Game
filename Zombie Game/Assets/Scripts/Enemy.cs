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

    public GameController gc;


    public SpriteRenderer sprite;

    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

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
        float distance = Vector3.Distance (this.transform.position, target.transform.position);
        checkDistance();
        if (gc.firstZombie == true)
            resumeTimer();
        animator.SetFloat("Horizontal", target.position.x - transform.position.x);
        animator.SetFloat("Speed", speed);

        if (isColliding)
        {
            player.TakeDamage(1);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        StartCoroutine(FlashRed());
        health -= damageAmount;

        if (health <= 0)
        {
            gc.gainScore();
            Destroy(this.gameObject);
        }
        if(speed > 1)
        {
            speed--;
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


    public void checkDistance()
    {
        float distance = Vector3.Distance (this.transform.position, target.transform.position);
        if(distance <= 12 && gc.firstZombie!= true)
            seen();
    }

    public void resumeTimer()
    {
         if(Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1;
                Debug.Log(gc.firstZombie);
            }
    }

    private void seen()
    {
        gc.firstZombieSeen();
        Time.timeScale = 2;
    }

}
