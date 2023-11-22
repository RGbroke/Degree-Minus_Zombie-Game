using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingAttack : MonoBehaviour
{
    // The bullet prefab
    public GameObject bulletPrefab;

    // The speed of the bullets
    public float bulletSpeed = 10f;

    // The angle to rotate the firing points each frame
    public float rotationSpeed = 10f;

    // The array of firing points
    private Transform[] firingPoints;

    private float attackDelay = 0.25f;

    public float ringFireRate;
    private float ringTimeToFire;

    // Start is called before the first frame update
    void Start()
    {
        // Get the firing points from the children of this game object
        firingPoints = new Transform[transform.childCount];
        for (int i = 0; i < firingPoints.Length; i++)
        {
            firingPoints[i] = transform.GetChild(i);

            firingPoints[i].rotation = Quaternion.Euler(0, 0, 90 - i * 45);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the firing points by the rotation speed
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // for ring attack
        if (ringTimeToFire <= 0f)
        {
            RingAttackSpecial();
        }

        ringTimeToFire -= Time.deltaTime;
    }

    // Ring Attack Special ability
    private void RingAttackSpecial()
    {
        StartCoroutine(RingAttackDelay());
        ringTimeToFire = ringFireRate;
    }

    IEnumerator RingAttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        //GameObject enemyProjectileObject;

        foreach (Transform firingPoint in firingPoints)
        {
            // Instantiate the bullet prefab at the firing point position and rotation
            GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);

            // Get the rigidbody component of the bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Give the bullet a velocity in the direction of the firing point
            rb.velocity = firingPoint.forward * bulletSpeed;
        }

    }

}
