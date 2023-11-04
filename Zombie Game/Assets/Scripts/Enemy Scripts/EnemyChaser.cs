using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class EnemyChaser : MonoBehaviour
{
    private Transform target;
    private float health;
    public float speed;
    public float maxHealth = 3f;
    public Animator animator;
    bool isColliding = false;
    PlayerController player;

    public GameController gc;
    public Dialogue dialogue;
    public SpriteRenderer sprite;

    public AudioSource zombieAttackNoise;

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

        animator.SetFloat("Horizontal", target.position.x - transform.position.x);
        animator.SetFloat("Speed", speed);

        if (isColliding)
        {
            zombieAttackNoise.Play();
            player.TakeDamage(1);
            new WaitForSeconds(1f);
        }

        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        checkDistance();
        if (gc.secondZombie == true)
            resumeTimer();
    }

    public void TakeDamage(float damageAmount)
    {
        StartCoroutine(FlashRed());
        health -= damageAmount;

        if (health <= 0)
        {
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

    public void checkDistance()
    {
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        if (distance <= 12 && gc.secondZombie != true)
            seen();
    }

    public void resumeTimer()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Time.timeScale = 1;
            Debug.Log(gc.secondZombie);
        }
    }

    private void seen()
    {
        gc.secondZombieSeen();
	dialogue.index++;
        Time.timeScale = 0;
    }

    void OnDestroy()
    {
        gc.zombieKilled();
    }
}
