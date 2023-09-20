using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] string dialogueText;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] GameObject dialoguePanel;

    [SerializeField] PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc.DialogueQuitPressed += OnCloseDialogue;

        dialoguePanel.SetActive(false);
        //ShowDialogue(dialogueText);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ShowDialogue(dialogueText);
        }
    }

    public void ShowDialogue(string dialogueText)
    {
        pc.state = PlayerController.Playerstate.Dialogue;
        dialoguePanel.SetActive(true);
        textMeshProUGUI.text = dialogueText;
    }

    public void OnCloseDialogue(object sender, EventArgs e)
    {
        dialoguePanel.SetActive(false);
        pc.state = PlayerController.Playerstate.Alive;
    }
}
