using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyTime = 3f;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(0, 10);
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(1);
        }
        else if (collision.gameObject.TryGetComponent<RangedEnemy>(out RangedEnemy enemyComp))
        {
            enemyComp.TakeDamage(1);
        }

        Destroy(gameObject);
    }

     private void OnCollisionExit2D(Collision2D collision)
     {
        Destroy(gameObject);
     }

}
