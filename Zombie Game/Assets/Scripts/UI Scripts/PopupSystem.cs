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
    public bool currentlyDisplayed;

    [SerializeField] private InputActionReference actionReference;

    void Start()
    {
        currentlyDisplayed = false;

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

        this.GetComponent<Animator>().SetTrigger("close");
        Time.timeScale = 1;
    }

    void setDisplayToClosed()
    {
        currentlyDisplayed = false;
    }
}
