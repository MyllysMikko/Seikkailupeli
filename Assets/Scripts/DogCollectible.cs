using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCollectible : QuestCollectible
{

    // Perii QuestCollectible luokan.
    // Erona on ett‰ "Interact" n‰ytt‰‰ dialogia.

    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] string[] dialogues;

    public override void Interact()
    {
        questGiver.currentCollectibles++;
        gameObject.SetActive(false);

        dialogueManager.ShowDialogue(dialogues);
    }
}
