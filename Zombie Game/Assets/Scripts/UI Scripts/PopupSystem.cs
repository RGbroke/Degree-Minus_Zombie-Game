using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupSystem : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;

    public void PopUp(string text, float time)
    {
        StartCoroutine(PoppingUp(text, time));
    }

    IEnumerator PoppingUp(string text, float time)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
        animator.SetTrigger("pop");
        yield return new WaitForSeconds(1);
        Time.timeScale = time;
    }
}
