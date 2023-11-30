using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TentacleHelper : MonoBehaviour
{
    public int damage = 5;
    public UnityEvent OnAttack, OnDone, OnDelete;

    public void TriggerAttack()
    {
        OnAttack?.Invoke();
    }

    public void Finish()
    {
        OnDone?.Invoke();
    }

    public void Delete()
    {
        OnDelete?.Invoke();
    }

    public void DeleteTentacle()
    {
        Destroy(gameObject);
    }
}
