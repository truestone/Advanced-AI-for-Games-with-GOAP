using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor : GAgent
{
    protected override void Start()
    {
        base.Start();

        SubGoal s1 = new SubGoal("research", 1, false);
        goals.Add(s1, 1);      

        SubGoal s2 = new SubGoal("rested", 3, false);
        goals.Add(s2, 3);           

        Invoke("GetTired", Random.Range(10, 20));
    }

    void GetTired()
    {
        beliefs.ModifyState("exhausted", 0);
        Invoke("GetTired", Random.Range(10, 20));
    }
}
