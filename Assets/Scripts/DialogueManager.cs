using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] string dialogueText;
    [SerializeField] List<string> dialogueList;
    [SerializeField] int listIndex;

    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] GameObject dialoguePanel;

    [SerializeField] PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        // Kuuntelee PlayerControllerin "DialogueQuitPressed" eventti‰.
        // Kun eventti‰ "nostetaan", suoritetaan OnCloseDialogue metodi.
        pc.DialogueQuitPressed += OnCloseDialogue;

        dialoguePanel.SetActive(false);
    }


    // N‰ytt‰‰ annetun tekstin dialogi laatikossa.
    public void ShowDialogue(string dialogueText)
    {
        pc.state = PlayerController.Playerstate.Dialogue;
        dialoguePanel.SetActive(true);
        textMeshProUGUI.text = dialogueText;
    }

    // Tallentaa annetut tekstit listaan, ja n‰ytt‰‰ listan ensimm‰isen dialogina.
    public void ShowDialogue(string[] inputList)
    {
        dialogueList = inputList.ToList();
        listIndex = 0;

        ShowDialogue(dialogueList[0]);

    }

    // Kun dialogi yritet‰‰n sulkea, jos dialogi listassa on seuraava teksti, se n‰ytet‰‰n seuraavaksi.
    // Muulloin dialogi laatikko suljetaan.
    public void OnCloseDialogue(object sender, EventArgs e)
    {
        if (listIndex < dialogueList.Count - 1)
        {
            listIndex++;
            ShowDialogue(dialogueList[listIndex]);
            return;
        }

        dialoguePanel.SetActive(false);
        pc.state = PlayerController.Playerstate.Alive;
    }
}
