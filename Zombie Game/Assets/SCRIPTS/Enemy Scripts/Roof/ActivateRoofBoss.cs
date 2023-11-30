using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ActivateRoofBoss : MonoBehaviour
{
    public CinemachineVirtualCamera vCamera;
    public GameObject boss;
    public GameObject blocker;
    public float cameraSize = 12f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject ob = collision.gameObject;
        if (ob.tag == "Player")
        {
            vCamera.m_Lens.OrthographicSize = cameraSize;
            boss.SetActive(true);
            blocker.SetActive(true);
        }
    }
}
