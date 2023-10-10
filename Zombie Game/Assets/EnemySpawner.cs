using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject zombie;
    [SerializeField]
    private float minSpawnTime;
    [SerializeField]
    private float maxSpawnTime;

    private float spawnTime;
    private float zombies = 0;
    private float maxZombies = 2;

    void Start()
    {
        StartCoroutine(spawnEnemy(minSpawnTime, zombie));
    }


    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
