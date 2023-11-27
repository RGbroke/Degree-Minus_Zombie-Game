using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearNormalAttacks : MonoBehaviour
{
   
    //Assets
    public GameObject bulletPrefab;
    private FirstBossTest parent;

     //Melee Attacks
    public Transform attackPoint;
    public LayerMask hitTarget;
    public float attackRange;
    public int attackDamage = 5;

    //Range Attacks
    public float fireRate = 2f;
    [SerializeField] private float bulletSpeed;
    public float attackDelay = 0.25f;

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
        parent.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
    }   

    private void RangeAttack()
    {
        StartCoroutine(RangeAttackDelay());
        parent.timeToFire = fireRate;
    }

    public IEnumerator RangeAttackDelay()
    { 
        Debug.Log("Range Attacking");
        yield return new WaitForSeconds(attackDelay);
        GameObject enemyProjectileObject;
        if(parent.facingRight)
            {
                enemyProjectileObject = Instantiate(bulletPrefab, parent.firingPoint.position, parent.firingPoint.rotation);
            }
        else
            {
                enemyProjectileObject = Instantiate(bulletPrefab, parent.firingPoint.position, flipCoordinate(parent.firingPoint.rotation));
            }
        EnemyProjectile enemyProjectile = enemyProjectileObject.GetComponent<EnemyProjectile>(); // Get the EnemyBullet component

        if (enemyProjectile != null)
        {
            enemyProjectile.speed = bulletSpeed; 
            enemyProjectile.GetComponent<Rigidbody2D>().AddForce(parent.firingPoint.up * enemyProjectile.speed, ForceMode2D.Impulse);
        }
        parent.animator.SetBool("isSpitting", false);
        parent.isRangeAttacking = false;
        parent.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
    }

    private Quaternion flipCoordinate(Quaternion q)
    {
        return new Quaternion(-q.x, -q.y, -q.z, q.w);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
