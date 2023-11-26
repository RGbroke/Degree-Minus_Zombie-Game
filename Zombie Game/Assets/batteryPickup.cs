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
    [SerializeField] private ObjectiveController_Stage2 objectiveControl;

    [SerializeField] private flashlightDecay fovDecayControl;
    private static bool dialogPlayed = false;
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

        if (dialogPlayed)
            return;

        objectiveControl.alertPlayer("AH PERFECT! A battery!");
        dialogPlayed = true;
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
        objectiveControl.getObjective("battery").progressObjective();
        this.gameObject.SetActive(false);
    }
}
