using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    private float textspeed;
    public int index;
    public GameController gc;

    void Start()
    {
        textComponent.text = lines[index];	

    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = lines[index];	
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
        }

        /* else{
            gameObject.SetActive(false);
        }*/
    }
}
