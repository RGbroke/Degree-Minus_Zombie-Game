using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
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

    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = defaultColor;
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        health = maxHealth;
        defaultColor = sprite.color;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        animator.SetFloat("Horizontal", target.position.x - transform.position.x);
        animator.SetFloat("Speed", speed);

        if (isColliding)
        {
            zombieAttackNoise.Play();
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
}
