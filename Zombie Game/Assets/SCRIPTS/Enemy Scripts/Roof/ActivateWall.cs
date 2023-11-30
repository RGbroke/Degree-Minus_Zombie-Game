using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWall : MonoBehaviour
{
    public TriggerWall boss;
    private bool opening = false;
    public float indicateTime = 1f;
    public float openrate = 0.5f;
    public GameObject wallZombie;
    public float spread = 6f;
    public float spawnrate = 0.5f;
    public float zomSpeed = 5f;
    public float destroyTime = 20f;
    public float activeTime = 1f;

    void OnEnable()
    {
        SpawnWallZom[] allchildren = this.GetComponentsInChildren<SpawnWallZom>(true);

        int i = 0;
        foreach (SpawnWallZom t in allchildren)
        {
            i++;
            t.gameObject.SetActive(true);
            if (Random.Range(0f,1f) > 1 - openrate && !opening)
            {
                opening = true;
                t.gameObject.SetActive(false);
            }
            if (i == allchildren.Length && !opening)
            {
                opening = true;
                t.gameObject.SetActive(false);
            }
        }

        opening = false;
        i = 0;

        StartCoroutine(Wait(activeTime));

    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
        boss.on = false;
    }
}
