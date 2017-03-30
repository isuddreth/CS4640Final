using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuest : Quest {

    public GameObject target;
    public bool status;
    public int reward;

    public KillQuest(GameObject t, int r)
    {
        target = t;
        status = false;
        reward = r;
    }
}
