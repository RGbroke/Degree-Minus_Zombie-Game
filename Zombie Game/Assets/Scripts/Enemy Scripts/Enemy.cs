using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private float health;
    private Color defaultColor;

    public float speed;
    public float maxHealth = 3f;
    public Animator animator;
    bool isColliding = false;
    PlayerController player;

    public GameController gc;
    public Dialogue dialogue;
    public SpriteRenderer sprite;
    public GameObject healthBar;

    public AudioSource zombieAttackNoise;

    NavMeshAgent agent;

    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = defaultColor;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        health = maxHealth;
        defaultColor = sprite.color;
    }

    void Update()
    {
        agent.SetDestination(target.position);

        //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        animator.SetFloat("Horizontal", target.position.x - transform.position.x);
        animator.SetFloat("Speed", speed);

        if (isColliding)
        {
            zombieAttackNoise.Play();
            player.TakeDamage(1);
            new WaitForSeconds(1f);
        }

       /* float distance = Vector3.Distance(this.transform.position, target.transform.position);
        checkDistance();
        if (gc.firstZombie == true)
            resumeTimer();*/
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
        else
        {
            healthBar.transform.localScale = new Vector3(health / maxHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent))
        {
            isColliding = true;
            animator.SetBool("isColliding", true);
            player = playerComponent;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent))
        {
            isColliding = false;
            animator.SetBool("isColliding", false);
            playerComponent.TakeDamage(1);
        }
    }
    /*
    public void checkDistance()
    {
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        if (distance <= 10 && gc.firstZombie != true)
            seen();
    }

    public void resumeTimer()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Time.timeScale = 1;
            Debug.Log(gc.firstZombie);
        }
    }

    private void seen()
    {
        //gc.firstZombieSeen();
	    //dialogue.index++;
	    //Debug.Log(dialogue.index);
        //Time.timeScale = 0;
    }
    */

}
