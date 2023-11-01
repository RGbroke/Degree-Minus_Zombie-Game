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
    public int FramesToFlash;
    public float meleeDelay = 0.3f;
    private bool meleeBlocked;
    public SpriteRenderer weaponSprite;

    public void Fire(float fireForce, float fireRate)
    {
        if(Time.time > lastShootTime + fireRate)
        {
            StartCoroutine(DoFlash());
            gunshot.Play();
            lastShootTime = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }
    }

    public void ThrowGrenade(float fireForce)
    {
        lastShootTime = Time.time;
        GameObject grenade = Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);
        grenade.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }

    IEnumerator DoFlash()
    {
        var renderer = muzzle.GetComponent<SpriteRenderer>();
        var originalSprite = renderer.sprite;
        renderer.sprite = FlashSprite;

        var framesFlashed = 0;
        while (framesFlashed < FramesToFlash)
        {
            framesFlashed++;
            yield return null;
        }

        renderer.sprite = originalSprite;
    }

    public void Melee()
    {
        if(meleeBlocked)
            return;
        meleeBlocked = true;
        weaponSprite.color = Color.red;
        StartCoroutine(MeleeDelay());
    }

    private IEnumerator MeleeDelay()
    {
        yield return new WaitForSeconds(meleeDelay);
        meleeBlocked = false;
        weaponSprite.color = Color.white;
    }
}
