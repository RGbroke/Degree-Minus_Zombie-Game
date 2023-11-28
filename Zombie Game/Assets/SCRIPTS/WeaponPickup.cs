using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject interactionAlert;
    [SerializeField] private GameObject weapon;
    [SerializeField] private WeaponEquiped weapons;
    [SerializeField] private InputActionReference actionReference;

    [SerializeField] private PopupSystem notification;
    [SerializeField] private string dialog;
    [SerializeField] private bool dialogPlayed = false;

    private bool playerTouching = false;
    private void Start()
    {
        actionReference.action.performed += context =>
        {
            interact(context);
        };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name != player.name)
            return;

        playerHover();

        if (dialogPlayed || !notification)
            return;

        dialogPopup(dialog);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name != player.name)
            return;

        playerHover();
    }

    private void dialogPopup(string dialog)
    {
        dialogPlayed = true;
        notification.PopUp(dialog);
    }

    private void playerHover()
    {
        playerTouching = !playerTouching;
        interactionAlert.SetActive(playerTouching);
    }

    public void interact(InputAction.CallbackContext context)
    {
        if (!playerTouching)
            return;

        weapon.SetActive(true);
        weapons.addWeapon(weapon);
        gameObject.SetActive(false);
    }
}
