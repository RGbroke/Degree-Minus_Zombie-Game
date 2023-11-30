using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnWallZom : MonoBehaviour
{
    public ActivateWall spawner;

    void Update()
    {
        if (Random.Range(0f, 1f) > 1 - spawner.spawnrate)
        {
            Vector3 spawnpoint = new Vector3(transform.position.x + Random.Range(-spawner.spread, spawner.spread), transform.position.y);
            GameObject zom = Instantiate(spawner.wallZombie, spawnpoint, transform.rotation);
            Rigidbody2D zomrb = zom.GetComponent<Rigidbody2D>();
            zom.GetComponent<WallZom>().animator.SetFloat("Horizontal", Random.Range(-1, 1));
            zomrb.velocity = (-transform.up) * spawner.zomSpeed;
            Destroy(zom, spawner.destroyTime);
        }
    }
}
