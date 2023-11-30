using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossManager : MonoBehaviour
{
    public TriggerWall wall;
    public SpawnTentacles tent;
    public Enemy boss;
    public GameObject escape;
    public float holeTime = 3f;

    private float lastTime;
    public float wallPhaseTime = 20f;
    public float underTentTime = 10f;
    public float xReturn;
    public float yReturn;

    public UnityEvent OnWall, OnTent, OnRage;
    private bool wallPhase = false, underPhase = true;

    void Start()
    {
        lastTime = Time.time;
    }

    void Update()
    {
        if (lastTime + wallPhaseTime <= Time.time && wallPhase && boss.health > boss.maxHealth/2)
        {
            lastTime = Time.time;
            wallPhase = false;
            OnTent.Invoke();
            underPhase = true;
            transform.position = new Vector3(xReturn, yReturn, 0);
            GameObject hole1 = Instantiate(escape, new Vector3(xReturn, yReturn + 1.5f, 0), transform.rotation);
            Destroy(hole1, holeTime);
        }
        if(lastTime + underTentTime <= Time.time && underPhase && boss.health > boss.maxHealth / 2)
        {
            lastTime = Time.time;
            underPhase = false;
            OnWall.Invoke();
            wallPhase = true;
            transform.position = new Vector3(-999, 0, 0);
            GameObject hole2 = Instantiate(escape, new Vector3(xReturn, yReturn + 1.5f, 0), transform.rotation);
            Destroy(hole2, holeTime);
        }
        if (boss.health <= boss.maxHealth / 2)
        {
            OnRage.Invoke();
            wallPhase = false;
            underPhase = false;
        }
    }
}
