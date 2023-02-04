using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class BossPhaseOneState : State
{
    [SerializeField] private State StateToChangeTo;
    [SerializeField] private Boss bossHP;
    private Dictionary<string, bool> ConditionDict = new Dictionary<string, bool>();


    private void PutInConditions(string s)
    {
        ConditionDict.Add(s, false);
        conditions.AddCondition(s, () =>
        {
            if (bossHP.HP <= bossHP.MaxHP / 2)
            {
                Debug.Log($"{bossHP.HP} is current and max is {bossHP.MaxHP/2}");
                ConditionDict[s] = true;
            }
        });
    }


    public override void OnStart(IEntity entity)
    {
        PutInConditions("Below 50");
        OnStartActions?.Invoke();
        Debug.Log("Phase 1");
    }

    public override void RunCurrentState(IEntity entity)
    {
        //bossAttack.UseAttack();

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
        Debug.Log("Exit Phase 1");

    }

}
