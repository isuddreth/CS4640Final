using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

    public static QuestManager questManager;
    public FoodManager myFood;
    public TPCharController playerController;

    public Text questText;
    public GameObject speechContainer;
    public Text speakerText;
    public Text dialogText;
    public float timer = 0;

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
        clearSpeech();
        DontDestroyOnLoad(gameObject);

        //initialize current;
        addActiveQuest();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                clearSpeech();
            }
        }
    }

    private void updateText()
    {
        questText.text = "";
        foreach (Quest quest in currentQuests)
        {
            questText.text += quest.description + "\n";
        }
    }


    public void endQuest(Quest q)
    {
        q.progress = Quest.QuestProgress.DONE;

        // quest reward
        switch (q.id)
        {
            case 1:
                playerController.walkingSpeed = 8;
                break;
            case 2:
                myFood.playerCapacity = 50;
                myFood.UpdateFood();
                break;
            default:
                break;

        }

        questText.text = "";
        speakerText.text = q.dialog[0].speaker;
        dialogText.text = q.dialog[0].dialog;
        setActive(q.nextQuest);
        timer = 15;
        speechContainer.SetActive(true);
    }


    private void clearSpeech()
    {
        speechContainer.SetActive(false);
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
        currentQuests = new List<Quest>();

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
                quest.currentFood = myFood.addCastleCount;
            }
        }
        updateText();
    }

    public void AddCastleFood(float f)
    {
        myFood.AddCastleFood(f);
        foreach (Quest quest in currentQuests)
        {
            if (quest.questObjective == "Food")
            {
                if (myFood.addCastleCount >= quest.currentFood + quest.questObjectiveCount)
                {
                    myFood.addCastleCount = 0;
                    endQuest(quest);
                }
            }
        }

        //update quest tracker text
        removeDoneQuest();
        addActiveQuest();
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
