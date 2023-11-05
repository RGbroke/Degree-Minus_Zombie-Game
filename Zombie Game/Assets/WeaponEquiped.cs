using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using static System.Net.Mime.MediaTypeNames;

public class WeaponEquiped : MonoBehaviour
{
    [SerializeField]
    private GameObject startingWeapon;

    [SerializeField]
    private UnityEngine.UI.Image weapon1_Display;

    [SerializeField]
    private UnityEngine.UI.Image weapon2_Display;

    [SerializeField]
    private UnityEngine.UI.Image weapon3_Display;

    [SerializeField]
    private Animator reloadVisual;

    [SerializeField]
    private Animator grenadeVisual;

    [SerializeField]
    private TextMeshProUGUI ammoDisplay;

    [SerializeField]
    private TextMeshProUGUI grenadeDisplay;

    [SerializeField]
    private Weapon currentGun;

    [SerializeField]
    private Animator switchVisual;

    private List<GameObject> weapons = new List<GameObject>();
    private int windowPos = 0;
    private int equiped = 0;

    void Start()
    {
        if (!startingWeapon)
            return;

        weapons.Add(startingWeapon);
        currentGun.WeaponChanged();
        updateWindow();
        updateAmmoDisplay();
    }
    public void grenadeUsed(int grenadesLeft)
    {
        grenadeVisual.SetTrigger("GrenadeUsed");
    }
    public void updateGrenadeDisplay(int grenadesLeft, int grenadeCapacity)
    {
        grenadeVisual.SetBool("GrenadesLeft", grenadesLeft > 0);
        grenadeDisplay.text = grenadesLeft + "/" + grenadeCapacity;
    }

    public void updateAmmoDisplay()
    {
        int numBullets = getEquiped().GetComponent<Gun>().getNumBullets();
        int ammoCapacity = getEquiped().GetComponent<Gun>().getMagSize();
        ammoDisplay.text = numBullets + "/" + ammoCapacity;
        reloadVisual.SetBool("NeedToReload", numBullets <= 0);
    }
    public void reloading()
    {
        reloadVisual.SetTrigger("Reloading");
    }
    public void doneReloading()
    {
        Debug.Log("DONE!");
        updateAmmoDisplay();
        reloadVisual.SetTrigger("ReloadFinished");
    }


    private void updateWindow()
    {
        //Setting window
        switch (windowPos % 3)
        {
            case 0:
                setWeaponWindow(weapon3_Display, weapon1_Display, weapon2_Display);
                break;
            case 1:
                setWeaponWindow(weapon1_Display, weapon2_Display, weapon3_Display);
                break;
            case 2:
                setWeaponWindow(weapon2_Display, weapon3_Display, weapon1_Display);
                break;
        }
    }
    private void setWeaponWindow(UnityEngine.UI.Image prev, UnityEngine.UI.Image equip, UnityEngine.UI.Image next)
    {
        equip.sprite = getEquiped().GetComponent<SpriteRenderer>().sprite;
        equip.color = getEquiped().GetComponent<SpriteRenderer>().color;

        prev.sprite = getPrevWeapon().GetComponent<SpriteRenderer>().sprite;
        prev.color = getPrevWeapon().GetComponent<SpriteRenderer>().color;

        next.sprite = getNextWeapon().GetComponent<SpriteRenderer>().sprite;
        next.color = getNextWeapon().GetComponent<SpriteRenderer>().color;
    }
    public void addWeapon(GameObject newWeapon)
    {
        if (!newWeapon)
            return;

        weapons.Add(newWeapon);
        updateWindow();

        UnityEngine.Debug.Log("Weapon Equiped " + equiped);
    }
    public void nextWeapon(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        nextWeapon();
    }
    
    
    public void nextWeapon()
    {
        if (weapons.Count <= 0)
            return;

        equiped = (equiped + 1) % weapons.Count;
        windowPos = (windowPos + 1) % 3;
        currentGun.WeaponChanged();
        updateWindow();
        updateAmmoDisplay();
        switchVisual.SetTrigger("Next");
        UnityEngine.Debug.Log("Weapon Equiped " + equiped);
    }

    public void prevWeapon(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        prevWeapon();
    }
    public void prevWeapon()
    {
        if (weapons.Count <= 0)
            return;

        equiped = (equiped + weapons.Count - 1) % weapons.Count;
        windowPos = (windowPos + 2) % 3;
        currentGun.WeaponChanged();
        updateWindow();
        updateAmmoDisplay();
        switchVisual.SetTrigger("Prev");
        UnityEngine.Debug.Log("Weapon Equiped " + equiped);
    }

    public GameObject getPrevWeapon()
    {
        if (weapons.Count == 0)
        {
            return null;
        }
        int prevIndex = (equiped + weapons.Count - 1) % weapons.Count;
        return weapons[prevIndex];
    }

    public GameObject getNextWeapon()
    {
        if (weapons.Count == 0)
        {
            return null;
        }
        int nextIndex = (equiped + 1) % weapons.Count;
        return weapons[nextIndex];
    }

    public GameObject getEquiped()
    {
        if (weapons.Count == 0)
        {
            return null;
        }
        return weapons[equiped];
    }
}
