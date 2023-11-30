using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossManager : MonoBehaviour
{
    public TriggerWall wall;
    public SpawnTentacles tent;
    public FireTentacle shoot;
    public KatanaBoss boss;
    public GameObject escape;
    public GameObject teleport;
    public float holeTime = 3f;

    public float maxWallUse = 3f;
    public float maxTentUse = 10f;
    public float maxShootUse = 1f;

    public UnityEvent OnWall, OnTent, OnShoot, OnRage, OnCharge;
    private bool wallPhase = false, underPhase = false, shootPhase = true;

    void Update()
    {
        if (wall.used >= maxWallUse && wallPhase && boss.health > boss.maxHealth/2)
        {
            wallPhase = false;
            wall.used = 0;
            OnShoot.Invoke();
            shootPhase = true;
            GameObject hole1 = Instantiate(escape, transform.position + new Vector3(0, 1.5f, 0), transform.rotation);
            Destroy(hole1, holeTime);
        }
        if(shoot.used >= maxShootUse && shootPhase && boss.health > boss.maxHealth / 2)
        {
            shootPhase = false;
            shoot.used = 0;
            OnTent.Invoke();
            underPhase = true;
        }
        if (tent.used >= maxTentUse && underPhase && boss.health > boss.maxHealth / 2)
        {
            underPhase = false;
            tent.used = 0;
            OnWall.Invoke();
            wallPhase = true;
            GameObject hole2 = Instantiate(escape, transform.position + new Vector3(0, 1.5f, 0), transform.rotation);
            Destroy(hole2, holeTime);
        }
        if (boss.health <= boss.maxHealth / 2 && boss.health > boss.maxHealth / 3)
        {
            OnRage.Invoke();
            wallPhase = false;
            underPhase = false;
            shootPhase = false;
        }

        if (boss.health <= boss.maxHealth / 3)
        {
            OnCharge.Invoke();
            teleport.SetActive(true);
            wallPhase = false;
            underPhase = false;
            shootPhase = false;
        }
    }
}
