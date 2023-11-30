using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnWallZom : MonoBehaviour
{
    public GameObject wallZombie;

    void Update()
    {
        Spawn();
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        GameObject zom = Instantiate(wallZombie, transform.position * Random.Range(0.95f, 1), transform.rotation);
        Rigidbody2D zomrb = zom.GetComponent<Rigidbody2D>();
        zom.GetComponent<WallZom>().animator.SetFloat("Horizontal", Random.Range(-1,1));
        zomrb.velocity = (-transform.up) * 10;
    }
}
