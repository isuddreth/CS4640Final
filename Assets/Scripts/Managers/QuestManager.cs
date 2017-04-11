using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

    public static QuestManager questManager;
    public Text questText;

    public List<Quest> questList = new List<Quest>(); // master list
    public List<Quest> currentQuests = new List<Quest>(); // current quests

    // private vars for our quests

    private void Awake()
    {
        if (questManager == null)
        {
            questManager = this;
        }
        else if (questManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        //initialize current;
        addActiveQuest();
    }

    private void updateText()
    {
        questText.text = "";
        foreach (Quest quest in currentQuests)
        {
            questText.text += quest.description + "\n";
        }
    }

    //add active quests
    public void addActiveQuest()
    {
        //foreach (Quest quest in questList)
        //{
        //    if (quest.progress == Quest.QuestProgress.ACTIVE)
        //    {
        //        currentQuests.Add(quest);
        //    }
        //}
        //updateText();

        Quest tempQuest = new Quest();

        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].progress == Quest.QuestProgress.ACTIVE)
            {
                tempQuest = questList[i];
                currentQuests.Add(tempQuest);
            }
        }
        updateText();

    }

    //remove done quests
    public void removeDoneQuest()
    {
        //foreach (Quest quest in questList)
        //{
        //    if (quest.progress == Quest.QuestProgress.DONE)
        //    {
        //        currentQuests.Remove(quest);
        //    }
        //}
        //updateText();

        Quest tempQuest = new Quest();

        for (int i = 0; i < currentQuests.Count; i++)
        {
            if (currentQuests[i].progress == Quest.QuestProgress.DONE)
            {
                Console.WriteLine(currentQuests[i].title);
                tempQuest = currentQuests[i];
                currentQuests.Remove(tempQuest);
            }
        }
        updateText();

    }

    //add quests
    public void setActive(int questID)
    {
        foreach (Quest quest in questList)
        {
            if (quest.id == questID)
            {
                quest.progress = Quest.QuestProgress.ACTIVE;
            }
        }
    }

    //remove quests
    //public void removeQuest(int questID)
    //{
    //    foreach (Quest quest in currentQuests)
    //    {
    //        if (quest.id == questID)
    //        {
    //            currentQuests.Remove(quest);
    //        }
    //    }
    //    updateText();
    //}

    //get quests

    // Checks
    public bool RequestAvailableQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestAcceptedQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.ACTIVE)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestCompleteQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.COMPLETE)
            {
                return true;
            }
        }
        return false;
    }


}
