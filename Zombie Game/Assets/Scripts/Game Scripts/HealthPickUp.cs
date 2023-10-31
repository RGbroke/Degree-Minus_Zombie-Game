using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player) && player.currHealth != player.maxHealth)
        {
            player.addHealth(healAmount);

            Destroy(gameObject);
        }
    }
}
