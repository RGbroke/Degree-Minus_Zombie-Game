using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    //Targeting
    PlayerController player;
    private Transform target;
    NavMeshAgent agent;

    //System
    public GameController gc;
    public Dialogue dialogue;

    //Sprite
    public SpriteRenderer sprite;
    public Animator animator;
    private Color defaultColor;

    //Stats
    public GameObject healthBar;
    public float maxHealth = 3f;
    private float health;
    public float speed;
    bool isColliding = false;

    //Audio
    public AudioSource zombieNoise;
    public AudioClip zombieAttackNoise1;
    public AudioClip zombieAttackNoise2;
    public AudioClip zombieAttackNoise3;

    //PowerUpDrop
    public GameObject healthDrop;
    public float ZombiesToKill = 20f;
    private bool stopPickUp = false;

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

        animator.SetFloat("Horizontal", target.position.x - transform.position.x);
        animator.SetFloat("Speed", speed);

        if (isColliding)
        {
            player.TakeDamage(1);
            new WaitForSeconds(1f);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        StartCoroutine(FlashRed());
        health -= damageAmount;

        if (health <= 0)
        {
            if (gc.numZombiesKilled() % 20 == 0 && !stopPickUp)
            {
                stopPickUp = true;
                Instantiate(healthDrop, transform.position, transform.rotation);
            }
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
            int attackNoise = Random.Range(1, 3);
            switch (attackNoise)
            {
                case 1:
                    zombieNoise.clip = zombieAttackNoise1;
                    break;
                case 2:
                    zombieNoise.clip = zombieAttackNoise2;
                    break;
                case 3:
                    zombieNoise.clip = zombieAttackNoise3;
                    break;
            }
            zombieNoise.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent))
        {
            isColliding = false;
            animator.SetBool("isColliding", false);
            //playerComponent.TakeDamage(1);
        }
    }

    void OnDestroy()
    {
        gc.zombieKilled();
    }

}
