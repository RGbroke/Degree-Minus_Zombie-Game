using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [HideInInspector]
    public float speed = 10f;

    private Rigidbody2D rb;
    public GameObject pool;

    public float destroyTime = 0.1f;
    public float acidDuration = 5f;
    public int acidDamage = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        //Destroy(gameObject, destroyTime);
        StartCoroutine(spawnAcid());
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    public IEnumerator spawnAcid()
    {
        yield return new WaitForSeconds(destroyTime);
        GameObject acidPool = Instantiate(pool, transform.position, Quaternion.Euler(0, 0, 0));
        acidPool.GetComponent<acidPool>().damage = acidDamage;
        Destroy(acidPool, acidDuration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.tag.Contains("Enemy"))
        {
            if (other.TryGetComponent<PlayerController>(out PlayerController playerComponent))
            {
                playerComponent.TakeDamage(1);
            }
            GameObject acidPool = Instantiate(pool, transform.position, Quaternion.Euler(0,0,0));
            acidPool.GetComponent<acidPool>().damage = acidDamage;
            Destroy(acidPool, acidDuration);
            Destroy(gameObject);
        }
    }

    /*
    // NOTE * MAKE SURE ALL ENEMIES CONTAIN SUBSTRING "ENEMY"
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Contains("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent))
            {
                playerComponent.TakeDamage(1);
            }
            Destroy(gameObject);
        }
    }
    */
}
