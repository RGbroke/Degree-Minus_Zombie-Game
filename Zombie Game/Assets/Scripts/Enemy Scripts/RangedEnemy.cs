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

    public Animator animator;
    [SerializeField] private float bulletSpeed;

    private float health;
    public float maxHealth = 3f;
    public SpriteRenderer sprite;
    public GameController gc;
    public GameObject healthBar;




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
            gc.gainScore();
            Destroy(this.gameObject);
        }
        else
        {
            healthBar.transform.localScale = new Vector3(health / maxHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }
        if (speed > 1)
        {
            speed--;
        }
    }


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        timeToFire = 0f;
        health = maxHealth;
    }

    private void Update()
    {
        if (target != null)
            {// Calculate the direction vector from the enemy to the player.
            Vector3 direction = (target.position - transform.position).normalized;

            // Determine if the player is on the left or right side of the zombie.
            float playerX = target.position.x;
            float zombieX = transform.position.x;
            bool playerIsOnLeft = playerX < zombieX;

            // Calculate an offset for the firing point based on the player's position.
            float xOffset = playerIsOnLeft ? -1f : 1f;

            Vector3 firingPointOffset = new Vector3(xOffset, 0f, 0f);
            animator.SetBool("playerIsRight", !playerIsOnLeft);
            // Apply the offset to the firingPoint's position.
            firingPoint.position = transform.position + firingPointOffset;

            // Rotate the firingPoint to face the player.
            RotateFiringPoint();

            if (!(Vector2.Distance(target.position, transform.position) <= distanceToStop))
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

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
            GameObject enemyProjectileObject = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            EnemyProjectile enemyProjectile = enemyProjectileObject.GetComponent<EnemyProjectile>(); // Get the EnemyBullet component

            if (enemyProjectile != null)
            {
                enemyProjectile.speed = bulletSpeed; // Set the speed using the EnemyBullet component
                enemyProjectile.GetComponent<Rigidbody2D>().AddForce(firingPoint.up * enemyProjectile.speed, ForceMode2D.Impulse);
            }

            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
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
}
