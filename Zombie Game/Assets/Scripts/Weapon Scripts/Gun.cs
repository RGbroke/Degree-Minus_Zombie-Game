using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Gun : MonoBehaviour
{
    //Gun Stats (Already Implemented)
    public float numBulletsPerShot;
    public float bulletLifespan;
    public float bulletSpeed;
    public float timeBetweenShots;
    public float bulletDamage;
    public float spread;
    public bool automatic;
    public float reloadTime;
    public int magSize;
    public int numBullets;
    public float bulletDrag;

    //Gun Cosmetics (TBD)
    public GameObject projectilePrefab; //Type of projectile the gun shoots
    public Sprite weaponInHand; //Sprite for rendering the gun in the player's hand
    public Sprite bulletSprite; //Sprite for bullet. ONLY add if nonrifle bullet (ie buckshot)
    public float bulletScale = -1;

    public AudioClip gunshotSound; //Sound played on each fire
    public AudioClip reloadSound;

    public float getNumBulletsPerShot()
    {
        return numBulletsPerShot;
    }
    public float getBulletLifespan()
    {
        return bulletLifespan;
    }
    public float getBulletSpeed()
    {
        return bulletSpeed;
    }
    public float getTimeBetweenShots() {
        return timeBetweenShots;
    }
    public float getBulletDamage()
    {
        return bulletDamage;
    }
    public float getSpread()
    {
        return spread;
    }
    public bool isAutomatic()
    {
        return automatic;
    }
    public float getReloadTime()
    {
        return reloadTime;
    }
    public int getMagSize()
    {
        return magSize;
    }
    public int getNumBullets()
    {
        return numBullets;
    }
    public float getBulletDrag()
    {
        return bulletDrag;
    }

    public GameObject getProjectilePrefab()
    {
        return projectilePrefab;
    }

    public Sprite getBulletSprite()
    {
        if (!bulletSprite)
            return projectilePrefab.GetComponent<SpriteRenderer>().sprite;

        return bulletSprite;
    }

    public float getBulletScale()
    {
        if(bulletScale == -1)
            return projectilePrefab.GetComponent<Transform>().localScale.x;

        return bulletScale;
    }

    public AudioClip getGunshotSoundEffect()
    {
        return gunshotSound;
    }
    public AudioClip getReloadSoundEffect()
    {
        return reloadSound;
    }


    public void setMagSize(int size)
    {
        magSize = size;
    }
    public void setNumBullets(int bullets)
    {
        numBullets = bullets;
    }

    

}
