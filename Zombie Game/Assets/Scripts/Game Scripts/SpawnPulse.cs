using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SpawnerPulse : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private EnemySpawner spawner;
    [SerializeField]
    private int spawnAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == target.name)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                spawner.spawnZombie();
            }
        }
    }
}
