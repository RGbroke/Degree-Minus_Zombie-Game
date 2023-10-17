using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnFirstSeen();
    public int index = 0;
    public static event OnFirstSeen PopUp;

    public void Dialogue()
    {
        if(Time.timeScale == 0)
            {
                if(PopUp != null)
                    PopUp();
            }
    }

   
}
