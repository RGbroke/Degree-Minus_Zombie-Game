using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    private float textspeed;
    private int index;
    public GameController gc;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log(gc.firstZombie);
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
             else
            {
            StopAllCoroutines();
            textComponent.text = lines[index];
            }
        }

    }

    public void Trigger()
    {
    
        if(textComponent.text == lines[index])
            NextLine();
        else
            StopAllCoroutines();
            textComponent.text = lines[index];
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textspeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }

        /* else{
            gameObject.SetActive(false);
        }*/
    }
}
