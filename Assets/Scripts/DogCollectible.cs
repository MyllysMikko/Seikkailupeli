using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCollectible : QuestCollectible
{
    // Start is called before the first frame update

    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] string[] dialogues;

    public override void Interact()
    {
        questGiver.currentCollectibles++;
        gameObject.SetActive(false);

        dialogueManager.ShowDialogue(dialogues);
    }
}
