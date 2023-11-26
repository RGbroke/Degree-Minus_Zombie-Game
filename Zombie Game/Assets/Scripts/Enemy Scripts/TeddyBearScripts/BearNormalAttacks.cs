using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearNormalAttacks : MonoBehaviour
{
    public Transform attackPoint;
    public Transform firingPoint;
    public LayerMask hitTarget;
    public GameObject puddlePrefab;
    private FirstBossTest parent;

    public float attackRange = 3f;
    public int attackDamage = 5;

    void Awake()
    {
        parent = gameObject.GetComponent<FirstBossTest>();
    }

    public IEnumerator MeleeAttack()
    {
        Debug.Log("Test");
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitTarget);
        foreach(Collider2D target in hitTargets)
        {
            /*Include scripts for destroying objects later*/
            target.GetComponent<PlayerController>().TakeDamage(attackDamage);
        }
        parent.animator.SetBool("isColliding", false);
        yield return new WaitForSeconds(1f);
        parent.isMeleeAttacking = false;
    }   

    public IEnumerator RangeAttack(float bulletSpeed)
    { 
        yield return new WaitForSeconds(0.5f);
        GameObject enemyProjectileObject = Instantiate(puddlePrefab, firingPoint.position, firingPoint.rotation);
        EnemyProjectile enemyProjectile = enemyProjectileObject.GetComponent<EnemyProjectile>(); 
        if (enemyProjectile != null)
        {
            enemyProjectile.speed = bulletSpeed; 
            enemyProjectile.GetComponent<Rigidbody2D>().AddForce(firingPoint.up * enemyProjectile.speed, ForceMode2D.Impulse);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
