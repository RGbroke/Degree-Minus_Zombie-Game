using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyTime = 3f;
    public float damage;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(0, 10);
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(damage);
        }
        else if (collision.gameObject.TryGetComponent<RangedEnemy>(out RangedEnemy enemyComp))
        {
            enemyComp.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

     private void OnCollisionExit2D(Collision2D collision)
     {
        Destroy(gameObject);
     }

}
