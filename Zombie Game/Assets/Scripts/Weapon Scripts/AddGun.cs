using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGun : MonoBehaviour
{
    public WeaponEquiped weaponControl;
    // Start is called before the first frame update
    void Start()
    {
        weaponControl.addWeapon(gameObject);
    }
}
