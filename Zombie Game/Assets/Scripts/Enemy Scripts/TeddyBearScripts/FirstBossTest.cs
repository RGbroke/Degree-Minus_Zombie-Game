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
    [SerializeField] private float bulletSpeed;
    public float knockbackForce = 100f;

    //Range Attacks
    public bool isRangeAttacking; 
    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;
    public float fireRate;
    public float timeToFire;
    public Transform firingPoint;
    public GameObject bulletPrefab;

    //Melee Attacks
    public LayerMask hitTarget;
    public float attackRange = 3f;
    public int attackDamage = 5;
    float meleeAttackDelay = 0;
    public bool isMeleeAttacking;


    // Animations
    public Animator animator;
    public bool moveRight = false;
    public bool facingRight = true;

    //UI + Sprites + System
    public SpriteRenderer sprite;
    public GameController gc;
    UnityEngine.AI.NavMeshAgent agent;
    public bool SecondPhase;
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
        StartCoroutine(FlashRed());
        health -= damageAmount;

        if (health <= 0)
        {
            healthBar.setActive(false);
            Destroy(this.gameObject);
        }
        else
        {
            healthBar.SetHealth(health);
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
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);
        isMeleeAttacking = false;
        isRangeAttacking = false;
        SecondPhase = false;
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }


    private void Update()
    {
        if(objectiveControl.objectives.ContainsKey("boss"))
        {
        if (!target)
        {
            GetTarget();
        }
        else
        {

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
                animator.SetFloat("Speed", 0);
            }
        }

        
        if (target != null && Vector2.Distance(target.position, transform.position) <= distanceToStop && meleeAttackDelay >= 4 && !isRangeAttacking)
        {
            agent.SetDestination(transform.position);
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            isMeleeAttacking = true;
            meleeAttackDelay = 0;
            animator.SetBool("isColliding", true);
        }
        else if (target != null && Vector2.Distance(target.position, transform.position) <= distanceToShoot && timeToFire <= 0f && !isMeleeAttacking)
        {
            agent.SetDestination(transform.position);
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            isRangeAttacking = true;
            animator.SetBool("isSpitting", true);
        }

        //Adjusting speed of attacks depending on distance based off y-axis
        double distance = transform.position.y - target.position.y;
        meleeAttackDelay += Time.deltaTime;

        Debug.Log(Math.Abs(distance));
        if(Math.Abs(distance) >= 5)
            {
                timeToFire -= Time.deltaTime * 2;
            }
        else
            {
                timeToFire -= Time.deltaTime;
            }

        if(health <= 1500)
        {
            SecondPhase = true;
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        }

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
        Collider2D collider = collision.collider;
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent))
        {
            player = playerComponent;

            /*Knocking Back Effect*/
            /*Vector2 direction = (collider.transform.position - transform.position).normalized;
            Vector2 knockback = direction * 10f;
            player.KnockBack(knockback);*/
        
        }
    }

 
  

    private void OnDestroy()
    {
        objectiveControl.getObjective("boss").completeObjective();
        objectiveControl.bossKilled();
    }
}
