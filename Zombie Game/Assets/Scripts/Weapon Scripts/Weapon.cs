using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    //Attack trackers
    public int bulletShot;
    public int grenadeThrew;
    public int meleeCount;
    
    //Bullet & Grenade
    public GameObject bulletPrefab;
    public GameObject grenadePrefab;

    //Muzzle Flash
    public GameObject muzzle;
    public AudioSource gunshot;
    public Sprite FlashSprite;

    //Weapon Control
    public WeaponEquiped weaponControl;

    //Gun Stats
    private float amtBullet = 1f;
    private float destroyTime = 0.5f;
    private float bulletSpeed = 30f;
    private float fireRate = 0.3f;
    private float bulletDamage = 1f;
    private float spread = 0.05f;
    public bool automatic = false;

    //Ammo
    private float reloadTime;
    private float reloadProgress;
    private int magazineSize;
    private int currAmmo;
    public bool isReloading;

    //Timer
    private static float lastShootTime;

    //Melee
    [HideInInspector]
    public float meleeDelay, meleeDamage, knockOutTime, meleeForce, meleeRadius;
    private bool meleeBlocked;
    public bool IsAttacking { get; private set; }
    public GameObject meleePoint;
    public Animator animator;

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }
    public void updateAmmoStats()
    {
        weaponControl.getEquiped().GetComponent<Gun>().setNumBullets(currAmmo);
        weaponControl.updateAmmoDisplay();
    }

    public void WeaponChanged()
    {
        Gun newGun = weaponControl.getEquiped().GetComponent<Gun>();

        if (!newGun) //Exit if the current equiped gun is null
            return;

        //Reinitialize these values for the new gun being equiped.
        amtBullet = newGun.getNumBulletsPerShot();
        destroyTime = newGun.getBulletLifespan();
        bulletSpeed = newGun.getBulletSpeed();
        fireRate = newGun.getTimeBetweenShots();
        bulletDamage = newGun.getBulletDamage();
        spread = newGun.getSpread();
        automatic = newGun.isAutomatic();
        reloadTime = newGun.getReloadTime();
        magazineSize = newGun.getMagSize();
        currAmmo = newGun.getNumBullets();
    }

    public void Fire()
    {
        if(Time.time > lastShootTime + fireRate && Time.timeScale != 0 && currAmmo > 0 && !isReloading)
        {
            StartCoroutine(DoFlash(0.1f));
            gunshot.Play();
            lastShootTime = Time.time;
            bulletShot++;
            currAmmo--;
            updateAmmoStats();
            for (int i = 0; i < amtBullet; i++)
            {
                Transform firePoint = muzzle.transform;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
                Vector2 dir = firePoint.up;
                Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-spread, spread);
                bulletrb.velocity = (dir + pdir) * bulletSpeed;
                bullet.GetComponent<Bullet>().damage = bulletDamage;
                Destroy(bullet, destroyTime);
            }
        }
    }

    void Update()
    {
        if (!isReloading)
            return;

        reloadProgress += Time.deltaTime;

        if(reloadProgress >= reloadTime)
        {
            currAmmo = magazineSize;
            updateAmmoStats();
            isReloading = false;
            reloadProgress = 0;
            weaponControl.doneReloading();
        }
    }

    public void reload()
    {
        weaponControl.reloading();
        isReloading = true;
        reloadProgress = 0;
    }

    public void ThrowGrenade(float fireForce)
    {
        lastShootTime = Time.time;
        Transform firePoint = muzzle.transform;
        GameObject grenade = Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);
        grenade.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        grenadeThrew++;
    }

    IEnumerator DoFlash(float flashrate)
    {
        var renderer = muzzle.GetComponent<SpriteRenderer>();
        var originalSprite = renderer.sprite;
        renderer.sprite = FlashSprite;
        yield return new WaitForSeconds(flashrate); ;

        renderer.sprite = originalSprite;
    }

    public void Melee()
    {
        if(meleeBlocked)
            return;
        animator.SetTrigger("Attack");
        meleeCount++;
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
        Transform circleOrigin = muzzle.transform;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, meleeRadius);
    }

    public void DetectColliders()
    {
        Transform circleOrigin = muzzle.transform;
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
