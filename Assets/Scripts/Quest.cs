using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quest
{
    public int id;
    public string questName;
    public string questDescription;
    public int questGoldReward;
    public int questExpReward;
    public bool questIsStarted;
    public bool questIsCompleted;


}
