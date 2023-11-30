using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyTime = 3f;
    [HideInInspector]
    public float damage;

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
        else if (collision.gameObject.TryGetComponent<FirstBossTest>(out FirstBossTest bossTest))
        {
            bossTest.TakeDamage(damage); 
        }
        else if (collision.gameObject.TryGetComponent<KatanaBoss>(out KatanaBoss kboss))
        {
            kboss.TakeDamage(damage);
        }
        /*Does absolutely nothing rn*/
        else if (collision.gameObject.TryGetComponent<FirstBoss>(out FirstBoss boss))
        {
            boss.TakeDamage(damage);
        }
        



        Destroy(gameObject);
    }

}
