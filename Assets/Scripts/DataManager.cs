using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : MonoBehaviour
{
    [SerializeField] string uri = "https://localhost:7055/quest";
    [field: SerializeField] public QuestManager MyQuestManager { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GetQuests(uri);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetQuest(uri, 1);
        }

    }

    void GetQuests(string uri)
    {
        QuestLoader questLoader = new QuestLoader();
        StartCoroutine(questLoader.LoadQuestsFromDataBase(uri));
    }

    void GetQuest(string uri, int id)
    {
        uri += $"/{id}";
        QuestLoader questLoader = new QuestLoader();
        StartCoroutine(questLoader.LoadQuestsFromDataBase(uri));
    }

    void GetQuestsCompleted(string uri)
    {
        uri += "/completed";
        QuestLoader questLoader = new QuestLoader();
        StartCoroutine(questLoader.LoadQuestsFromDataBase(uri));
    }

    void GetQuestsInProgress(string uri)
    {
        uri += "/inProgress";
    }

}
