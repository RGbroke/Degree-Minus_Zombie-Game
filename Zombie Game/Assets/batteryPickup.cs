using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class batteryPickup : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject interactionAlert;
    [SerializeField] private InputActionReference actionReference;

    [SerializeField] private flashlightDecay fovDecayControl;
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
        if (collision.gameObject.name != player.name)
            return;

        playerHover();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name != player.name)
            return;

        playerHover();
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

        fovDecayControl.batteryObtained();
        this.gameObject.SetActive(false);
    }
}
