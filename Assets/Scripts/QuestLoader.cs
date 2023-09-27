using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class QuestLoader
{

    public int id;
    public string questName;
    public string questDescription;
    public int questGoldReward;
    public int questExpReward;
    public bool questIsStarted;
    public bool questIsCompleted;



    public QuestLoader() { }

    public QuestLoader(int id, string questName, string questDescription, int questGoldReward, int questExpReward, bool questIsStarted, bool questIsCompleted)
    {
        this.id = id;
        this.questName = questName;
        this.questDescription = questDescription;
        this.questGoldReward = questGoldReward;
        this.questExpReward = questExpReward;
        this.questIsStarted = questIsStarted;
        this.questIsCompleted = questIsCompleted;
    }


    // Hakee Questit annetusta uri:sta ja tallentaa ne annetuun listaan.
    public IEnumerator LoadQuestsFromDataBase(string uri, List<Quest> questList)
    {
        using UnityWebRequest request = UnityWebRequest.Get(uri);

        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            Quest[] questList2 = JsonConvert.DeserializeObject<Quest[]>(json);
            foreach (Quest quest in questList2)
            {
                questList.Add(quest);
            }
        }
    
    }

    // Hakee Questit annetusta uri:sta ja tulostaa ne konsoliin.
    public IEnumerator PrintQuestsFromDataBase(string uri)
    {
        using UnityWebRequest request = UnityWebRequest.Get(uri);

        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            Debug.Log(json);

        }

    }


    // Hakee Questin annetusta uri:sta ja tallentaa sen annettuun muuttujaan.
    // HUOM! Olettaa että annetusta uri:sta tulee yksi tulos!
    public IEnumerator LoadQuestFromDatabase(string uri, Quest quest)
    {
        using UnityWebRequest request = UnityWebRequest.Get(uri);

        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.LogError(request.error);
        }
        else
        {
            //string json = request.downloadHandler.text.Remove(0, 1);
            //json = json.Remove(json.Length - 1, 1);
            string json = request.downloadHandler.text;
            Quest quest2 = JsonConvert.DeserializeObject<Quest>(json);
            quest.id = quest2.id;
            quest.questName = quest2.questName;
            quest.questDescription = quest2.questDescription;
            quest.questGoldReward = quest2.questGoldReward;
            quest.questExpReward = quest2.questExpReward;
            quest.questIsStarted = quest2.questIsStarted;
            quest.questIsCompleted = quest2.questIsCompleted;
        }
    }


    // Tallentaa annetun Quest muuttujan tietokantaan.
    public IEnumerator SaveQuestToDatabase(string uri, Quest quest)
    {
        string id = $"\"id\":{this.id},";
        string questIsStarted = $"\"questIsStarted\":{this.questIsStarted.ToString().ToLower()},";
        string questIsCompleted = $"\"questIsCompleted\":{this.questIsCompleted.ToString().ToLower()}";

        string bodyData = "{" + id + questIsStarted + questIsCompleted + "}" ;

        Debug.Log (bodyData);

        using UnityWebRequest request = UnityWebRequest.Put(uri, bodyData);

        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("Tallennus onnistui");
        }

    }


}
