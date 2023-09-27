using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] DataManager dataManager;
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

    // Kun npc:lle puhutaan.
    public void Talk()
    {
        if (quest == null || quest.id == 0)
        {
            GetQuest();
        }



        // Jos Quest ei ole alkanut. Aloita se
        if (!quest.questIsStarted)
        {

            quest.questIsStarted = true;

            PutQuest();

            dialogueManager.ShowDialogue(new string[] { $"''{startingQuestText}''", $"Palkinto: {quest.questGoldReward} kultaa, {quest.questExpReward} kokemuspistettä" });

            return;
        }

        // Jos quest ei ole valmis.
        if (!quest.questIsCompleted)
        {
            // Tarkista onko pelaaja tehnyt kaiken tarvittavan. Jos kyllä, merkitse tehtävä valmiiksi.
            if (CheckQuestRequirements())
            {
                quest.questIsCompleted = true;

                PutQuest();

                dataManager.UpdateGold(quest.questGoldReward);
                dataManager.UpdateExp(quest.questExpReward);


                dialogueManager.ShowDialogue(new string[] {$"''{completedQuestText}''", $"Sait {quest.questGoldReward} kultaa sekä {quest.questExpReward} kokemuspistettä" });

                return;
            }
            // Muuten muistuta pelaajaa tehtävästä.
            else
            {
                dialogueManager.ShowDialogue(new string[] { $"''{inProgressQuestText}''",  $"{quest.questDescription}" });

                return;

            }
        }
        // Jos Quest on jo valmis, kiitä vain pelaajaa.

        dialogueManager.ShowDialogue($"''{completedQuestText}''");

    }

    // Pyydä QuestLoaderilta Questin tiedot npc:lle annetun Quest ID:n avulla.
    public void GetQuest()
    {
        string uri2 = uri + $"/{id}";
        QuestLoader questLoader = new QuestLoader();
        StartCoroutine(questLoader.LoadQuestFromDatabase(uri2, quest));
    }

    // Tallenna tietokantaan Questin tiedot.
    public void PutQuest()
    {
        string uri2 = uri + $"/{id}";
        QuestLoader questLoader = new QuestLoader(quest.id, quest.questName, quest.questDescription, quest.questGoldReward, quest.questExpReward, quest.questIsStarted, quest.questIsCompleted);    
        StartCoroutine(questLoader.SaveQuestToDatabase(uri2, quest));
    }

    // Palauttaa, onko Questin vaatimukset täytetty. Tämän voi ylikirjoittaa tarpeen mukaan.
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
