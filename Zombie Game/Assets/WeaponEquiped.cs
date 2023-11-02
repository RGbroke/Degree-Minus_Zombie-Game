using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponEquiped : MonoBehaviour
{
    [SerializeField]
    GameObject weapon1;

    [SerializeField]
    GameObject weapon2;

    [SerializeField]
    GameObject weapon3;

    [SerializeField]
    Animator switchVisual;

    private int equiped = 0;

    public void nextWeapon(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        nextWeapon();
    }
    public void nextWeapon()
    {
        switchVisual.SetTrigger("Next");
        equiped = (equiped + 1) % 3;
        UnityEngine.Debug.Log("Weapon: " + equiped);
    }

    public void prevWeapon(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        prevWeapon();
    }
    public void prevWeapon()
    {
        switchVisual.SetTrigger("Prev");
        equiped = (equiped + 2) % 3;
        UnityEngine.Debug.Log("Weapon: " + equiped);
    }

    public GameObject getEquiped(InputAction.CallbackContext context)
    {
        switch (equiped)
        {
            case 0:
                return weapon1;
            case 1:
                return weapon2;
            case 2:
                return weapon3;
        }
        return null; //Should never run but just in case
    }
}
