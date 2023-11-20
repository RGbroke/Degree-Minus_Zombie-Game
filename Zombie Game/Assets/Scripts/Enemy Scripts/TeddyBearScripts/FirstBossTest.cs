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
    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;
    private float attackDelay = 0.25f;
    public HealthBar healthBar;
    private float health;
    public float maxHealth = 3f;
    public float fireRate;
    private float timeToFire;
    [SerializeField] private float bulletSpeed;
    public Transform firingPoint;
    public GameObject bulletPrefab;

    // Animations
    public Animator animator;
    private bool moveRight = false;
    private bool facingRight = true;

    //UI + Sprites + System
    public SpriteRenderer sprite;
    public GameController gc;
    UnityEngine.AI.NavMeshAgent agent;

 

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
    }


    private void Update()
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

            if (!(Vector2.Distance(target.position, transform.position) <= distanceToShoot))
            {
                agent.SetDestination(target.position);
                animator.SetFloat("Speed", speed);
            }
            else
            {
                animator.SetFloat("Speed", 0);
            }

        }

        if (target != null && Vector2.Distance(target.position, transform.position) <= distanceToShoot)
        {
            Shoot();
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


    //Shooting projectile
    private void Shoot()
    {
        if (timeToFire <= 0f)
        {
            animator.SetBool("isSpitting", true);
            StartCoroutine(AttackDelay());
            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
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

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        GameObject enemyProjectileObject;
        if(facingRight)
            {
                enemyProjectileObject = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            }
        else
            {
                enemyProjectileObject = Instantiate(bulletPrefab, firingPoint.position, flipCoordinate(firingPoint.rotation));
            }
        EnemyProjectile enemyProjectile = enemyProjectileObject.GetComponent<EnemyProjectile>(); // Get the EnemyBullet component

        if (enemyProjectile != null)
        {
            enemyProjectile.speed = bulletSpeed; // Set the speed using the EnemyBullet component
            enemyProjectile.GetComponent<Rigidbody2D>().AddForce(firingPoint.up * enemyProjectile.speed, ForceMode2D.Impulse);
        }
        animator.SetBool("isSpitting", false);
    }

    private Quaternion flipCoordinate(Quaternion q)
    {
        return new Quaternion(-q.x, -q.y, -q.z, q.w);
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            if (Vector2.Distance(target.position, transform.position) >= distanceToStop)
            {
                rb.velocity = transform.up * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
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
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent))
        {
            player = playerComponent;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent))
        {
            playerComponent.TakeDamage(1);
        }
    }
    void OnDestroy()
    {
        gc.zombieKilled();
    }
}
