using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SpawnerActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private EnemySpawner spawner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == target.name)
            spawner.activate();
    }
}
