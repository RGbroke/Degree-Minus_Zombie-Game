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

    //Gun Stats, must update from weapon script(TBD)
    public float reloadTime;
    public int magSize;
    public int numBullets;

    //Gun Cosmetics (TBD)
    public Sprite weaponInHand; //Sprite for rendering the gun in the player's hand
    public Sprite bullet; //Sprite for bullet. ONLY add if nonrifle bullet (ie buckshot)
    public AudioSource gunshotSound; //Sound played on each fire
    public AudioSource gunshotTrail; //Can be null, ONLY applies when automatic is true

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

    public void setMagSize(int size)
    {
        magSize = size;
    }
    public void setNumBullets(int bullets)
    {
        numBullets = bullets;
    }

}
