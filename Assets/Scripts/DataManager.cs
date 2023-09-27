using Newtonsoft.Json;
using System;
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

        PrintAll();

    }

    // Update is called once per frame
    void Update()
    {

    }



    //Pyyt‰‰ QuestLoaderilta kaikki valmiit questit jotka tallentuvat "quests" lista muuttujaan
    void GetCompleted()
    {
        string uri2 = uri + "/completed";
        QuestLoader questLoader = new QuestLoader();
        StartCoroutine(questLoader.LoadQuestsFromDataBase(uri2, quests));
    }

    // Kutsuu QuestLoaderin metodia joka tulostaa kaikki questit konsoliin.
    public void PrintAll()
    {
        QuestLoader questLoader = new QuestLoader();
        StartCoroutine(questLoader.PrintQuestsFromDataBase(uri));
    }

    // Kutsuu QuestLoaderin metodia joka tulostaa valmiit questit konsoliin.
    public void PrintCompleted()
    {
        string uri2 = uri + "/completed";
        QuestLoader questLoader = new QuestLoader();
        StartCoroutine(questLoader.PrintQuestsFromDataBase(uri2));
    }

    //Kutsuu QuestLoaderin metodia joka tulostaa keskener‰iset questit konsoliin.
    public void PrintInProgress()
    {
        string uri2 = uri + "/inProgress";
        QuestLoader questLoader = new QuestLoader();
        StartCoroutine(questLoader.PrintQuestsFromDataBase(uri2));
    }


    // P‰ivitt‰‰ hudin kullan m‰‰r‰n.
    void UpdateGold()
    {
        goldText.text = $"Kulta: {gold}";
    }

    // Ensin kasvattaa kullan m‰‰r‰‰ annetulla luvulla, ja sen j‰lkeen p‰ivitt‰‰ hudin.
   public void UpdateGold(int gold)
    {
        this.gold += gold;
        UpdateGold();
    }

    // P‰ivitt‰‰ hudin exp:n m‰‰r‰n.
    void UpdateExp()
    {
        expText.text = $"Exp: {exp}";
    }

    // Ensin kasvattaa exp:n m‰‰r‰n annetulla luvulla, ja sen j‰lkeen p‰ivitt‰‰ hudin.
    public void UpdateExp(int exp)
    {
        this.exp += exp;
        UpdateExp();
    }


}
