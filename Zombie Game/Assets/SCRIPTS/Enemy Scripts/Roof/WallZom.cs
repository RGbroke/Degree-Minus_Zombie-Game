using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallZom : MonoBehaviour
{
    public Animator animator;
    [HideInInspector]
    public bool spin = false;
    public float rotateSpeed = 10f;

    void Update()
    {
        animator.SetFloat("Speed", 1);
        if (spin)
        {
            transform.Rotate(0, 0, rotateSpeed);
        }
    }
}
