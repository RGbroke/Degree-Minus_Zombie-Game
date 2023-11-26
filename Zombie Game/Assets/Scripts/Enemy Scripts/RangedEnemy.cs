using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;


public class RangedEnemy : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.0025f;
    private Rigidbody2D rb;
    PlayerController player;

    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;

    public float fireRate;
    private float timeToFire;

    public Transform firingPoint;
    public GameObject bulletPrefab;

    // Animations
    public Animator animator;
    private bool moveRight = false;
    [SerializeField] private float bulletSpeed;
    private float attackDelay = 0.25f;

    private float health;
    public float maxHealth = 3f;
    public SpriteRenderer sprite;
    public GameController gc;
    public GameObject healthBar;

    UnityEngine.AI.NavMeshAgent agent;

    //PowerUpDrop
    public GameObject healthDrop;
    public float ZombiesToKill = 20f;
    private bool stopPickUp = false;

    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

    public void TakeDamage(float damageAmount)
    {
        StartCoroutine(FlashRed());
        health -= damageAmount;

        if (health <= 0)
        {
            if (gc.numZombiesKilled() % 20 == 0 && !stopPickUp && damageAmount < 5)
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
        /*
        if (speed > 1)
        {
            speed--;
        }
        */
    }


    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.stoppingDistance = distanceToStop;

        rb = gameObject.GetComponent<Rigidbody2D>();
        timeToFire = 0f;
        health = maxHealth;
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
            animator.SetBool("moveRight", moveRight);
            // Apply the offset to the firingPoint's position.
            firingPoint.position = transform.position + firingPointOffset;

            // Rotate the firingPoint to face the player.
            RotateFiringPoint();

            if (!(Vector2.Distance(target.position, transform.position) <= distanceToStop))
            {
                agent.SetDestination(target.position);

                //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                animator.SetFloat("Horizontal", target.position.x - transform.position.x);
                animator.SetFloat("Speed", speed);
            }
            else
            {
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Speed", 0);
            }

        }

        if (target != null && Vector2.Distance(target.position, transform.position) <= distanceToStop)
        {
            Shoot();
        }
    }

    private void RotateFiringPoint()
    {
        Vector2 targetDirection = target.position - firingPoint.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        firingPoint.localRotation = Quaternion.Slerp(firingPoint.localRotation, q, rotateSpeed);
    }

    private void Shoot()
    {
        if (timeToFire <= 0f)
        {
            animator.SetBool("isAttacking", true);
            StartCoroutine(AttackDelay());

            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);

        GameObject enemyProjectileObject = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        EnemyProjectile enemyProjectile = enemyProjectileObject.GetComponent<EnemyProjectile>(); // Get the EnemyBullet component

        if (enemyProjectile != null)
        {
            enemyProjectile.speed = bulletSpeed; // Set the speed using the EnemyBullet component
            enemyProjectile.GetComponent<Rigidbody2D>().AddForce(firingPoint.up * enemyProjectile.speed, ForceMode2D.Impulse);
        }
        animator.SetBool("isAttacking", false);
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
