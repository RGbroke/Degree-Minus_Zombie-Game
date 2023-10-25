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
    private int timesTriggerable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == target.name && timesTriggerable > 0)
        {
            spawner.spawnZombie();
            timesTriggerable--;
        }

    }
}
