using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public State StateToChangeTo;

    public override void OnStart(IEntity entity)
    {
        OnStartActions?.Invoke();
        Debug.Log(entity.SM.currentState.name + " entered");
    }

    public override void RunCurrentState(IEntity entity)
    {
        OnUpdateActions?.Invoke();

        if (conditions.ConditionPassed)
        {
            entity.SM.ExitState();
            entity.SM.SetState(StateToChangeTo);
        }
    }

    public override void OnExit(IEntity entity)
    {
        OnEndActions?.Invoke();
        Debug.Log(entity.SM.currentState.name + " exit");
    }
}
