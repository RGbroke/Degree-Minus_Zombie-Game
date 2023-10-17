using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameController gc;
    [SerializeField]
    private GameObject zombie;
    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    private float maxConcurrent;

    private float spawnTime = 0;

    void Update()
    {
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0 && gc.numActiveZombies() < maxConcurrent)
        {
            gc.addActiveZombies(1);
            Instantiate(zombie, transform.position, Quaternion.identity);
            spawnTime = UnityEngine.Random.Range(spawnDelay/2, spawnDelay);
        }
    }
}
