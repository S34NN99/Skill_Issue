using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class BossPhaseTwoState : State
{
    [SerializeField] private State StateToChangeTo;
    [SerializeField] private Damageable bossHP;
    private Dictionary<string, bool> ConditionDict = new Dictionary<string, bool>();


    private void PutInConditions(string s)
    {
        ConditionDict.Add(s, false);
        conditions.AddCondition(s, () =>
        {
            ConditionDict[s] = false;
        });
    }


    public override void OnStart(IEntity entity)
    {
        PutInConditions("Testing");
        OnStartActions?.Invoke();
        Debug.Log("Phase 2");
    }

    public override void RunCurrentState(IEntity entity)
    {
        OnUpdateActions?.Invoke();
        foreach (var s in ConditionDict.Keys.ToList())
        {
            UnityEvent thisEvent;
            if (conditions.conditionList.TryGetValue(s, out thisEvent))
            {
                thisEvent?.Invoke();
            }
        }

        if (conditions.CheckListOfCondition(ref ConditionDict))
        {
            entity.SM.ExitState();
            entity.SM.SetState(StateToChangeTo);
        }
    }

    public override void OnExit(IEntity entity)
    {
        Debug.Log("Exit phase 2");

    }

}
