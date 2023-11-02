using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject grenadePrefab;
        public Transform firePoint;
    public GameObject muzzle;
    public AudioSource gunshot;

    private static float lastShootTime;
    public Sprite FlashSprite;
    private bool meleeBlocked;
    public SpriteRenderer weaponSprite;

    public Transform circleOrigin;
    public GameObject meleePoint;
    public float bulletDamage = 1f;

    public Animator animator;
    public bool IsAttacking { get; private set; }

    [HideInInspector]
    public float meleeDelay;
    public float meleeDamage;
    public float knockOutTime;
    public float meleeForce;
    public float meleeRadius;

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    public void Fire(float fireForce, float fireRate)
    {
        if(Time.time > lastShootTime + fireRate)
        {
            StartCoroutine(DoFlash(fireRate));
            gunshot.Play();
            lastShootTime = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            bullet.GetComponent<Bullet>().damage = bulletDamage;
        }
    }

    public void ThrowGrenade(float fireForce)
    {
        lastShootTime = Time.time;
        GameObject grenade = Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);
        grenade.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }

    IEnumerator DoFlash(float flashrate)
    {
        var renderer = muzzle.GetComponent<SpriteRenderer>();
        var originalSprite = renderer.sprite;
        renderer.sprite = FlashSprite;
        yield return new WaitForSeconds(flashrate/2); ;

        renderer.sprite = originalSprite;
    }

    public void Melee()
    {
        if(meleeBlocked)
            return;
        animator.SetTrigger("Attack");
        IsAttacking = true;
        meleeBlocked = true;
        StartCoroutine(MeleeDelay());
    }

    private IEnumerator MeleeDelay()
    {
        yield return new WaitForSeconds(meleeDelay);
        meleeBlocked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, meleeRadius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, meleeRadius))
        {
            Enemy enemy;
            RangedEnemy rangedEnemy;
            if(enemy = collider.GetComponent<Enemy>())
            {
                enemy.TakeDamage(meleeDamage);
                KnockBackFeedBack kb = enemy.GetComponent<KnockBackFeedBack>();
                kb.delay = knockOutTime;
                kb.strength = meleeForce;
                kb.PlayFeedback(meleePoint);
            }
            if(rangedEnemy = collider.GetComponent<RangedEnemy>())
            {
                rangedEnemy.TakeDamage(meleeDamage);
                KnockBackFeedBack kb = rangedEnemy.GetComponent<KnockBackFeedBack>();
                kb.delay = knockOutTime;
                kb.strength = meleeForce;
                kb.PlayFeedback(meleePoint);
            }
        }
    }
}
