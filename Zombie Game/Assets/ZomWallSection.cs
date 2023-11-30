using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZomWallSection : MonoBehaviour
{
    void Update()
    {
        if (Random.Range(0f, 1f) > 0.8f)
        {
            gameObject.SetActive(false);
        }
        else { gameObject.SetActive(true); }
    }
}
