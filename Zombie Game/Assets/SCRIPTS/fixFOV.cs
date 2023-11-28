using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class fixFOV : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private flashlightDecay decayControl;
    [SerializeField]
    private FieldOfView fovControl;
    [SerializeField]
    private CinemachineVirtualCamera targetCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != target.name)
            return;

        decayControl.setActiveDecay(false);
        fovControl.setFOV(360f);

        targetCamera.m_Lens.OrthographicSize = 15;
    }
}
