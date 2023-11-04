using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Gun Stats
    public float numBulletsPerShot;
    public float bulletLifespan;
    public float bulletSpeed;
    public float timeBetweenShots;
    public float bulletDamage;
    public float spread;
    public bool automatic;

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
}
