using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float blastRadius = 4f;
    public float explosionDamage = 10f;
    public GameObject explosionEffect;
    public bool detonateOnTouch = false;

    public float countdown;
    public bool hasExploded = false;
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }

    }
    void Explode()
    {
        //Explosion Effect
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation); //Istantiates an explosion
        explosion.transform.up = Vector3.up;
        //Effect nearby objects
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius); //Gets an array of objects withing blastRadius of grenade
        
        foreach (Collider2D collider in colliders)
        {
            //BlastDamage
            Enemy zombieMelee = collider.GetComponent<Enemy>();
            RangedEnemy zombieRange = collider.GetComponent<RangedEnemy>();
            FirstBoss boss1 = collider.GetComponent<FirstBoss>();
            FirstBossTest boss1TEST = collider.GetComponent<FirstBossTest>();
            KatanaBoss secondBoss = collider.GetComponent<KatanaBoss>();
            if (zombieMelee)
            {
                zombieMelee.TakeDamage(explosionDamage);
            }
            else if (zombieRange)
            {
                zombieRange.TakeDamage(explosionDamage);
            }
            else if (boss1)
            {
                boss1.TakeDamage(explosionDamage);
            }
            /*TEMP FIX REMOVE LATER*/
            else if (boss1TEST)
            {
                boss1TEST.TakeDamage(explosionDamage);
            }
            else if (secondBoss)
            {
                secondBoss.TakeDamage(explosionDamage);
            }

        }

        //Grenade Removal
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!detonateOnTouch)
            return;

        Explode();
    }
}
