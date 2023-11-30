using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallZom : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        animator.SetFloat("Speed", 1);
    }
}
