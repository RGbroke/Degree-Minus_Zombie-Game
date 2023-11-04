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
    private float spawnDelayMin;
    [SerializeField]
    private float spawnDelayMax;
    

    private bool isActive = false;
    private float spawnTime = 0;

    void Update()
    {
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0 && isActive)
        {
            spawnZombie();
            spawnTime = UnityEngine.Random.Range(spawnDelayMin, spawnDelayMax);
        }
    }
    public void spawnZombie()
    {
        gc.addActiveZombies(1);
        Instantiate(zombie, transform.position, Quaternion.identity);
    }

    public void activate() { isActive = true; }
    public void deactivate() { isActive = false; }

}
