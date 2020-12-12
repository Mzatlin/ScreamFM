﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueController : MonoBehaviour, IEndDialogue
{
    public Canvas dialogueCanvas;
    public TextMeshProUGUI textDialogue;
    [TextArea(2,3)]
    public string content;
    [SerializeField]
    float typingSpeed = 0.1f;

    bool isActive = false;
    IEnumerator dialogueCoroutine = null;
    public event Action OnEndDialogue = delegate { };


    IActivateDialogue Activate => GetComponent<IActivateDialogue>();

    // Start is called before the first frame update
    void Awake()
    {
        if(dialogueCanvas != null && textDialogue != null)
        {
            dialogueCanvas.enabled = false;
            textDialogue.text = "";
        }

        if(Activate != null)
        {
            Activate.OnActivateDialogue += HandleActivateDialogue;
        }
    }

   void OnDestroy()
    {
        if (Activate != null)
        {
            Activate.OnActivateDialogue -= HandleActivateDialogue;
        }
        if(dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }
    }


    void HandleActivateDialogue()
    {
        if (dialogueCanvas != null)
        {
            dialogueCanvas.enabled = true;
            isActive = true;
            dialogueCoroutine = TypeDelay(content);
            StartCoroutine(dialogueCoroutine);

        }
    }

    IEnumerator TypeDelay(string sentence)
    {
        foreach(char letter in sentence)
        {
            textDialogue.text += letter;
            yield return new WaitForSeconds(typingSpeed*Time.deltaTime);
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            EndDialogueOnInput();
        }
    }

    void EndDialogueOnInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsTextFinished())
            {
                OnEndDialogue();
                dialogueCanvas.enabled = false;
                textDialogue.text = "";
                isActive = false;
            }
            else
            {
                StopCoroutine(dialogueCoroutine);
                textDialogue.text = content;
            }
           
        }

    }

    bool IsTextFinished()
    {
        if(textDialogue.text == content)
        {
            return true;
        }
        return false;
    }

}