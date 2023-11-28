using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightActivator : MonoBehaviour
{
    [SerializeField] flashlightDecay flashlightControl;
    private bool played = false;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player" || played)
            return;

        flashlightControl.flashlightToggle(true);
        played = true;
    }
}
