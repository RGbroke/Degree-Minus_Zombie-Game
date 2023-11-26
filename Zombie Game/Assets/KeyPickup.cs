using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject interactionAlert;
    [SerializeField] private GameObject key;
    [SerializeField] private ObjectiveController_Stage2 objectiveControl;
    [SerializeField] private InputActionReference actionReference;
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

        objectiveControl.keyObtained();
        key.SetActive(false);
    }
}
