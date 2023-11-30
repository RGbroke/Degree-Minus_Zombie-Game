using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    public int damage = 999;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject ob = collision.gameObject;
        if (ob.tag == "Player")
        {
            PlayerController player = ob.GetComponent<PlayerController>();
            player.TakeDamage(damage);
        }
        if (ob.GetComponent<WallZom>() != null)
        {
            ob.GetComponent<WallZom>().spin = true;
        }
    }
}
