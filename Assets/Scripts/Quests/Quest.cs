using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{

    public enum QuestProgress { NOT_AVAILABLE, AVAILABLE, ACTIVE, COMPLETE, DONE}

    public string title;                //title of the quest
    public int id;                      //ID Number of quest
    public QuestProgress progress;      //state of the current quest
    public string description;          //string from the quest giver
    public string hint;                 //string from the quest giver
    public string congratulations;      //string from the quest giver
    public string summary;              //string from the quest giver
    public int nextQuest;               //the id for the next quest in chain
    public List<Dialog> dialog = new List<Dialog>();

    public string questObjective;       //the description of the current quest objective
    public float currentFood;
    public int questObjectiveCount;     //the current quantity of objective
    public int questObjectRequirement;  //the quantity requirement of quest

    public int foodReward;
    public string itemReward;

}