using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class PopupSystem : MonoBehaviour
{
    public GameObject popUpBox;
    public TMP_Text popUpText;
    public bool currentlyDisplayed = false;
    public string delayedText;

    [SerializeField] private InputActionReference actionReference;


    void Start()
    {
        actionReference.action.performed += context =>
        {
            resume();
        };
    }
    private void OnDisable()
    {
        actionReference.action.Dispose();
    }

    public void PopUp(string text)
    {
        if (popUpBox == null)
            return; 

        StartCoroutine(opening(text));
    }

    IEnumerator opening(string text)
    {
        currentlyDisplayed = true;
        popUpBox.SetActive(true);
        popUpText.text = text;
        this.GetComponent<Animator>().SetTrigger("pop");
        yield return new WaitForSeconds(1f);
        pause();
    }

    public void pause()
    {
        Time.timeScale = 0f;
    }

    public void resume()
    {
        if (Time.timeScale != 0 || !this)
            return;
        Time.timeScale = 1;
        this.GetComponent<Animator>().SetTrigger("close");
        
    }
    private void setDisplayToClosed()
    {
        currentlyDisplayed = false;
    }


    public bool isDisplayed()
    {
        return currentlyDisplayed;
    }
}
