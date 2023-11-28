using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acidPool : MonoBehaviour
{
    [HideInInspector]
    public int damage;
    private bool isColliding = false;

    private PlayerController player;

    private void Update()
    {
        if (isColliding)
        {
            player.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            isColliding = true;
            player = collision.gameObject.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            isColliding = false;
        }

    }
}
