using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class KatanaBoss : MonoBehaviour
{
    //Targeting
    PlayerController player;
    private Transform target;
    NavMeshAgent agent;

    //System
    public GameController gc;
    public ObjectiveController_Stage3 objectiveControl;

    //Sprite
    public SpriteRenderer sprite;
    //public Animator animator;
    private Color defaultColor;

    //Stats
    public HealthBar healthBar;
    public float maxHealth = 3f;
    private float health;
    public float speed;
    bool isColliding = false;

    bool changingSpeed = false;
    bool animationToRight = false;
    bool hasTeleported = false;

    // Declare a variable to store the teleport cooldown
    public float teleportDelay = 5f;
    public Transform[] spawnPoints;

    // Declare a variable to track the last teleport time
    private float lastTeleportTime;

    public AudioSource audioSource;
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
        healthBar.setActive(true);
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);
        defaultColor = sprite.color;

        lastTeleportTime = 0f;

    }

    void Update()
    {
        agent.SetDestination(target.position);

        //animator.SetFloat("Horizontal", target.position.x - transform.position.x);
        //animator.SetFloat("Speed", speed);

        if (isColliding)
        {
            gameObject.GetComponent<Animator>().SetBool("nearPlayer",true);
            player.TakeDamage(1);
            new WaitForSeconds(1f);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("nearPlayer", false);
        }

        if (!changingSpeed && !isColliding)
        {
            // Start the coroutine
            StartCoroutine(ChangeSpeed());
            
        }

        
        //Dirty animation fix
        if (target.position.x - gameObject.transform.position.x < 0 && animationToRight)
        {
            //Debug.Log(target.position.x - gameObject.transform.position.x);
            gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f); //Points the doc to the left
            animationToRight = false;
        }
        else if(target.position.x - gameObject.transform.position.x > 0 && !animationToRight)
        {
            //Debug.Log(target.position.x - gameObject.transform.position.x);
            gameObject.transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f); //Points the doc to the right
            animationToRight = true;
        }
    }

    IEnumerator ChangeSpeed()
    {
        // Set the boolean variable to true to indicate the coroutine is running
        changingSpeed = true;

        // Set the agent's speed to 0
        agent.speed = 0;

        // Wait for a few seconds
        yield return new WaitForSeconds(1.25f);

        // Set the agent's speed to 50
        agent.speed = 50;

        // Wait for 2 seconds
        yield return new WaitUntil(() => isColliding);

        // Set the boolean variable to false to indicate the coroutine is finished
        changingSpeed = false;
    }

    public void TakeDamage(float damageAmount)
    {
        if (Time.time - lastTeleportTime > teleportDelay)
        {
            // Select a random spawn point from the array
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Teleport the agent to the spawn point position
            transform.position = randomSpawnPoint.position;

            // Update the last teleport time to the current time
            lastTeleportTime = Time.time;

            if (!hasTeleported)
            {
                objectiveControl.alertPlayer("Did he just dodge my attack!?");
                hasTeleported = true;
            }

            //Avoids damage by teleporting away
            return;
        }

        StartCoroutine(FlashRed());
        health -= damageAmount;

        if (health <= 0)
        {
            healthBar.setActive(false);
            audioSource.Stop();
            Destroy(this.gameObject);
        }
        else
        {
            healthBar.SetHealth(health);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent))
        {
            isColliding = true;
            //animator.SetBool("isColliding", true);
            player = playerComponent;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent))
        {
            isColliding = false;
            //animator.SetBool("isColliding", false);
            //playerComponent.TakeDamage(1);
        }
    }

    private void OnDestroy()
    {
        if (objectiveControl.getObjective("boss") == null)
            return;

        objectiveControl.getObjective("boss").completeObjective();
        objectiveControl.findCure();
    }
}
