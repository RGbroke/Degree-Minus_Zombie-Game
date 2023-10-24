using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject muzzle;
    public AudioSource gunshot;

    private static float lastShootTime;
    public Sprite FlashSprite;
    public int FramesToFlash;

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
}
