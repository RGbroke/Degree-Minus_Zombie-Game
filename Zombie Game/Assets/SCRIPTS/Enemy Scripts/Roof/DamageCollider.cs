using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public int damage = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject ob = collision.gameObject;
        if (ob.tag == "Player")
        {
            PlayerController player = ob.GetComponent<PlayerController>();
            player.TakeDamage(damage);
        }
    }
}
