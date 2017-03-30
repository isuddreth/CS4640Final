using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestDB
{
    public static List<Quest> questList;

    public static void init(){
        questList = new List<Quest>();
        //addKillQuest();
    }

    private static void addKillQuest()
    {
        //KillQuest killQ = new KillQuest(GameObject.FindGameObjectsWithTag("Wolf"),50); // add another database for enemies w/ enemy id
    }

}

public class Quest {
    
    

public Quest()
    {
        
    }
    
}
