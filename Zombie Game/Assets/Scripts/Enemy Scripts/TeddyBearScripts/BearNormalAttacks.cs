using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearNormalAttacks : MonoBehaviour
{
    public Transform attackPoint;
    public Transform firingPoint;
    public LayerMask hitTarget;
    public GameObject puddlePrefab;


    public float attackRange = 3f;
    public int attackDamage = 5;


    public void MeleeAttack()
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitTarget);
        Debug.Log("You have been hit");
        foreach(Collider2D target in hitTargets)
        {
            /*Include scripts for destroying objects later*/
            target.GetComponent<PlayerController>().TakeDamage(attackDamage);
        }
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
