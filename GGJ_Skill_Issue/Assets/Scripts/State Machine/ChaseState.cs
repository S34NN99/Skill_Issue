using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ChaseState : State
{
    public State StateToChangeTo;

    private Dictionary<string, bool> ConditionDict = new Dictionary<string, bool>();
    private GameObject parent;
    private GameObject player;

    private void PutInConditions(string s)
    {
        ConditionDict.Add(s, false);
        conditions.AddCondition(s, () =>
        {
            Debug.Log("Testing");
            //ConditionDict[s] = true;
        });
    }

    public override void OnStart(IEntity entity)
    {
        player = FindAnyObjectByType<Player>().gameObject;
        PutInConditions("s");
        OnStartActions?.Invoke();
        Debug.Log(entity.SM.currentState.name + " entered");
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
        OnEndActions?.Invoke();
        ConditionDict.Clear();
        Debug.Log(entity.SM.currentState.name + " exit");
    }

    void MoveTowards()
    {
        //parent.transform = Vector2.MoveTowards()
    }
}
