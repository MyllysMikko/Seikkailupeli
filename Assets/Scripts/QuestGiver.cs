using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] PlayerController pc;

    [SerializeField] string uri = "https://localhost:7055/quest";

    [SerializeField] int id;

    [SerializeField] public Quest quest;

    [SerializeField] string startingQuestText;

    [SerializeField] string inProgressQuestText;

    [SerializeField] string completedQuestText;

    [SerializeField] int collectibleAmmount;
    public int currentCollectibles;

    [SerializeField] bool inRange;

    // Start is called before the first frame update
    void Awake()
    {
        currentCollectibles = 0;
        inRange = false;
        GetQuest();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (inRange && pc.state == PlayerController.Playerstate.Alive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Talk();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    public void Talk()
    {
        if (quest == null || quest.id == 0)
        {
            GetQuest();
        }



        // Jos Quest ei ole alkanut. Aloita se
        if (!quest.questIsStarted)
        {
            // TODO: Puhu, kerro pelaajalle teht�v�
            // P�ivit� tietokanta

            Debug.Log($"{startingQuestText}");

            quest.questIsStarted = true;

            PutQuest();

            dialogueManager.ShowDialogue(new string[] { $"{startingQuestText}", $"Palkinto: {quest.questGoldReward} kultaa, {quest.questExpReward} kokemuspistett�" });

            return;
        }

        // Jos quest ei ole valmis.
        if (!quest.questIsCompleted)
        {
            // Tarkista onko pelaaja tehnyt kaiken tarvittavan
            if (CheckQuestRequirements())
            {
                quest.questIsCompleted = true;

                PutQuest();

                dialogueManager.ShowDialogue($"{completedQuestText}");

                dialogueManager.ShowDialogue(new string[] {$"{completedQuestText}", $"Sait {quest.questGoldReward} kultaa sek� {quest.questExpReward} kokemuspisteit�" });

                //dialogueManager.ShowDialogue($"Sait {quest.questGoldReward} kultaa sek� {quest.questExpReward} kokemuspisteit�");

                return;
            }
            else
            {
                //TODO Puhu, muistuta pelaajaa questista.

                Debug.Log($"{inProgressQuestText} {quest.questDescription}");
                dialogueManager.ShowDialogue($"{inProgressQuestText} {quest.questDescription}");

                return;

            }
        }

        Debug.Log($"{completedQuestText}");
        dialogueManager.ShowDialogue($"{completedQuestText}");

    }

    public void GetQuest()
    {
        string uri2 = uri + $"/{id}";
        QuestLoader questLoader = new QuestLoader();
        StartCoroutine(questLoader.LoadQuestFromDatabase(uri2, quest));
    }

    public void PutQuest()
    {
        string uri2 = "https://localhost:7055/quest/1";
        QuestLoader questLoader = new QuestLoader(quest.id, quest.questName, quest.questDescription, quest.questGoldReward, quest.questExpReward, quest.questIsStarted, quest.questIsCompleted);    
        StartCoroutine(questLoader.SaveQuestToDatabase(uri2, quest));
    }

    public virtual bool CheckQuestRequirements()
    {
        return currentCollectibles >= collectibleAmmount;
    }


    enum QuestStatus
    {
        NotStarted,
        Started,
        Completed
    }
}
