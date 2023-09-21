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
            Debug.Log(json);
            Quest[] questList2 = JsonConvert.DeserializeObject<Quest[]>(json);
            foreach (Quest quest in questList2)
            {
                questList.Add(quest);
            }
        }
    
    }
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
            Debug.Log(json);
            Quest perse = JsonConvert.DeserializeObject<Quest>(json);
            quest.id = perse.id;
            quest.questName = perse.questName;
            quest.questDescription = perse.questDescription;
            quest.questGoldReward = perse.questGoldReward;
            quest.questExpReward = perse.questExpReward;
            quest.questIsStarted = perse.questIsStarted;
            quest.questIsCompleted = perse.questIsCompleted;
        }
    }


    public IEnumerator SaveQuestToDatabase(string uri, Quest quest)
    {
        string id = $"\"id\":{this.id},";
        //string questName = $"\"questName\":{this.questName},";
        //string questDescription = $"\"questDescription\":{this.questDescription},";
        //string questGoldReward = $"\"questGoldReward\":{this.questGoldReward},";
        //string questExpReward = $"\"questExpReward\":{this.questExpReward},";
        string questIsStarted = $"\"questIsStarted\":{this.questIsStarted.ToString().ToLower()},";
        string questIsCompleted = $"\"questIsCompleted\":{this.questIsCompleted.ToString().ToLower()}";

        //string bodyData = "{" + id + questName + questDescription + questGoldReward + questExpReward + questIsStarted + questIsCompleted + "}";
        string bodyData = "{" + id + questIsStarted + questIsCompleted + "}" ;

        Debug.Log (bodyData);
        //string bodyData = "{" + questIsStarted + questIsCompleted + "}" ;

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
