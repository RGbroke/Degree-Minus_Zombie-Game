using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;


public class FirstBossTest : MonoBehaviour
{
    //Targeting & Stats
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.0025f;
    private Rigidbody2D rb;
    PlayerController player;
    public HealthBar healthBar;
    private float health;
    public float maxHealth = 3f;
    private float startOfFight = 0f;
    private bool invincible = true;

    //Range Attacks
    [HideInInspector] public bool isRangeAttacking; 
    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;
    public float fireRate;
    public float timeToFire;
    public Transform firingPoint;
    [SerializeField] private bool rangeAttacker;

    //Melee Attacks
    public LayerMask hitTarget;
    private float meleeAttackDelay = 0;
    [SerializeField] private float nextMeleeAttack;
    [HideInInspector] public bool isMeleeAttacking;
    [SerializeField] private bool meleeAttacker;

    //Third Phase
    private bool thirdPhase = false;
    private float thirdPhaseCount = 1;
    private float retaliation;
    [SerializeField] GameObject children;
    private float killCount;

    // Animations
    public Animator animator;
    [HideInInspector] public bool moveRight = false;
    [HideInInspector] public bool facingRight = true;

    //UI + Sprites + System
    public SpriteRenderer sprite;
    public GameController gc;
    UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private bool boss;
    [SerializeField] private GameObject HPBar;
    [SerializeField] private ObjectiveController_Stage2 objectiveControl;

 

    //Flashing Red when Hit
    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

    //Taking Damage Script
    public void TakeDamage(float damageAmount)
    {
        if (startOfFight >= 2.1f && (!invincible || !boss))
        {
            StartCoroutine(FlashRed());
            health -= damageAmount;
            if (health <= 0)
            {
                if(boss)
                    healthBar.setActive(false);
                else
                    objectiveControl.teddyKilled();
                Destroy(this.gameObject);
            }
            else if (health >= 0 && boss)
            {
                healthBar.SetHealth(health);
            }
        }
    }


    //Initialize NavMesh Agent + update its parameters.
    //Setting time to fire + max HP
    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.stoppingDistance = distanceToStop;
        rb = gameObject.GetComponent<Rigidbody2D>();
        timeToFire = 0f;
        health = maxHealth;
        isMeleeAttacking = false;
        isRangeAttacking = false;
        if(healthBar != null && boss)
            {
                healthBar.SetMaxHealth(maxHealth);
                healthBar.SetHealth(health);
            }
        if(boss)
            gameObject.transform.GetChild(1).gameObject.SetActive(false);

    }


    private void Update()
    {
        Debug.Log(objectiveControl.killCount);
        if(objectiveControl.objectives.ContainsKey("boss"))
        {
        startOfFight += Time.deltaTime;
        if (thirdPhase && boss)
        {
            retaliation += Time.deltaTime;
            invincible = true;
            if(retaliation >= 1)
            {
                health += 2f;
                retaliation = 0;
            }
            healthBar.SetHealth(health);

            if(objectiveControl.killCount >= 6)
            {
                thirdPhase = false;
                gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
            }
            else if(health >= 180)
            {
                thirdPhase = false;
                gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
            }
        }
        else if (!thirdPhase)
        {
            //Activating HP Bar
            if(startOfFight >= 2.1f && boss)
            {
                HPBar.SetActive(true);
                invincible = false;
            }

            //bool agentIsMovingLeft = agentVelocity.x < 0;
            // Calculate movement direction for ranged enemy
            if (agent.hasPath && agent.velocity.magnitude > 0)
            {
                Vector3 agentForward = agent.transform.forward; // Get the forward direction of the agent
                Vector3 agentVelocity = agent.velocity.normalized; // Get the normalized velocity

                // Calculate the dot product of the agent's forward direction and its velocity
                float dotProduct = Vector3.Dot(agentForward, agentVelocity);
                if (agentVelocity.x > 0) // Agent is moving to the right
                {
                    moveRight = true;
                    // Do something for right movement
                }
                else if (agentVelocity.x < 0) // Agent is moving to the left
                {
                    moveRight = false;
                    // Do something for left movement
                }
            }
            else
            {
                moveRight = target.position.x - transform.position.x > 0 ? true : false;
            }

            //bool playerIsOnLeft = agentIsMovingLeft;

            // Calculate an offset for the firing point based on the player's position.
            float xOffset = !moveRight ? -1f : 1f;

            Vector3 firingPointOffset = new Vector3(xOffset, 0f, 0f);
            //animator.SetBool("moveRight", moveRight);
            // Apply the offset to the firingPoint's position.
            firingPoint.position = transform.position + firingPointOffset;

            // Rotate the firingPoint to face the player.
            RotateFiringPoint();

            float direction = target.position.x - transform.position.x;

            if(direction < 0 && facingRight)
            {
                Flip();
            }
            else if (direction > 0 && !facingRight)
            {
                Flip();
            }

            if (!(Vector2.Distance(target.position, transform.position) <= distanceToStop) && !isRangeAttacking && !isMeleeAttacking)
            {
                agent.SetDestination(target.position);
                animator.SetFloat("Speed", speed);
            }
            else
            {
                agent.SetDestination(transform.position);
                animator.SetFloat("Speed", 0);
            }

            if (target != null && Vector2.Distance(target.position, transform.position) <= distanceToStop && meleeAttackDelay >= nextMeleeAttack && !isRangeAttacking && meleeAttacker)
            {
                agent.SetDestination(transform.position);
                gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
                isMeleeAttacking = true;
                meleeAttackDelay = 0;
                animator.SetBool("isColliding", true);
            }
            else if (target != null && Vector2.Distance(target.position, transform.position) <= distanceToShoot && timeToFire <= 0f && !isMeleeAttacking && rangeAttacker && startOfFight >= 2)
            {
                agent.SetDestination(transform.position);
                gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
                isRangeAttacking = true;
                animator.SetBool("isSpitting", true);
            }

            //Adjusting speed of attacks depending on distance based off y-axis
            double distance = transform.position.y - target.position.y;
            meleeAttackDelay += Time.deltaTime;

            if(Math.Abs(distance) >= 5)
            {
                timeToFire -= Time.deltaTime * 2;
            }
            else
            {
                timeToFire -= Time.deltaTime;
            }

            //Ring Phase
            if(health <= 180 && boss)
            {
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }

            if(health <= 90 && thirdPhaseCount > 0 && boss)
            {
                gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                  /*Knocking Back Effect*/
                player = target.GetComponent<PlayerController>();
                Vector2 knockbackDirection = (target.position - transform.position).normalized;
                Vector2 knockback = knockbackDirection * 10f;
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                rb.AddForce(knockback, ForceMode2D.Impulse);
                player.knockbackImmune = false;
                children.SetActive(true);
                animator.SetFloat("Speed", 0);
                thirdPhaseCount = 0;
                thirdPhase = true;
            }
        }
        }


        //Third Phase

        //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void RotateFiringPoint()
    {
        Vector2 targetDirection = target.position - firingPoint.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        firingPoint.localRotation = Quaternion.Slerp(firingPoint.localRotation, q, rotateSpeed);
    }


    //Sprite Flip
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        firingPoint.position = localScale;
    }


  


    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
       
    }

 
  

    private void OnDestroy()
    {
        if(boss)
        {
        if (objectiveControl.getObjective("boss") != null)
            objectiveControl.getObjective("boss").completeObjective();
        objectiveControl.bossKilled();
        }
    }
}
