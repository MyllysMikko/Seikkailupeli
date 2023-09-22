using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : MonoBehaviour
{
    [SerializeField] string uri = "https://localhost:7055/quest";
    [SerializeField] int gold;
    [SerializeField] int exp;
    [SerializeField] List<Quest> quests = new List<Quest>();

    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI expText;

    // Start is called before the first frame update
    void Awake()
    {
        GetCompleted();
    }

    void Start()
    {
        foreach (Quest quest in quests)
        {
            gold += quest.questGoldReward;
            exp += quest.questExpReward;
        }
        UpdateGold();
        UpdateExp();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetCompleted()
    {
        string uri2 = uri + "/completed";
        QuestLoader questLoader = new QuestLoader();
        StartCoroutine(questLoader.LoadQuestsFromDataBase(uri2, quests));
    }

    public void PrintCompleted()
    {
        string uri2 = uri + "/completed";
        QuestLoader questLoader = new QuestLoader();
        StartCoroutine(questLoader.PrintQuestsFromDataBase(uri2));
    }

    public void PrintInProgress()
    {
        string uri2 = uri + "/inProgress";
        QuestLoader questLoader = new QuestLoader();
        StartCoroutine(questLoader.PrintQuestsFromDataBase(uri2));
    }

    /*void GetQuests(string uri)
    {
        QuestLoader questLoader = new QuestLoader();
        StartCoroutine(questLoader.LoadQuestsFromDataBase(uri));
    }
    */

    void UpdateGold()
    {
        goldText.text = $"Kulta: {gold}";
    }

   public void UpdateGold(int gold)
    {
        this.gold += gold;
        UpdateGold();
    }

    void UpdateExp()
    {
        expText.text = $"Exp: {exp}";
    }

    public void UpdateExp(int exp)
    {
        this.exp += exp;
        UpdateExp();
    }


    void GetQuestsInProgress(string uri)
    {
        uri += "/inProgress";
    }

}
