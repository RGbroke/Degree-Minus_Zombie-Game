using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float blastRadius = 4f;
    public float explosionDamage = 10f;

    public GameObject explosionEffect;

    float countdown;
    bool hasExploded = false;
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
        Instantiate(explosionEffect, transform.position, transform.rotation); //Istantiates an explosion

        //Effect nearby objects
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius); //Gets an array of objects withing blastRadius of grenade
        
        foreach (Collider2D collider in colliders)
        {
            //BlastDamage
            Enemy zombieMelee = collider.GetComponent<Enemy>();
            if (zombieMelee)
            {
                zombieMelee.TakeDamage(explosionDamage);
            }
            RangedEnemy zombieRange = collider.GetComponent<RangedEnemy>();
            if (zombieRange)
            {
                zombieRange.TakeDamage(explosionDamage);
            }
            PlayerController player = collider.GetComponent<PlayerController>();
            if (player)
            {
                player.TakeDamage((int)explosionDamage);
            }

        }

        //Grenade Removal
        Destroy(gameObject);
    }
}
