using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWall : MonoBehaviour
{
    public GameObject wall;

    void Start()
    {
        wall.SetActive(false);
        Transform[] allchildren = wall.GetComponentsInChildren<Transform>();
        foreach (Transform t in allchildren)
        {
            t.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (Random.Range(0f, 1f) > 0.5)
        {
            wall.SetActive(true);
            Transform[] allchildren = wall.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in allchildren)
            {
                t.gameObject.SetActive(true);
            }
        }
    }
}
