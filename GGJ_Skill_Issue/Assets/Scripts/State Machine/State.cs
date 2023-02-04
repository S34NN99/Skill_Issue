using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public interface IEntity
{
    public StateMachine SM { get; }
    public GameObject curentEntity { get; set; }
}

[Serializable]
public class Condition
{
    public bool ConditionPassed;
    public Dictionary<string, UnityEvent> conditionList = new();


    public void AddCondition(string eventName, UnityAction action)
    {
        UnityEvent thisEvent;
        if (conditionList.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(action);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(action);
            conditionList.Add(eventName, thisEvent);
        }
    }

    public bool CheckListOfCondition(ref Dictionary<string, bool> dict)
    {
        foreach (var s in dict.Values.ToList())
        {
            if (!s)
                return false;
        }
        return true;
    }
}

public abstract class State : MonoBehaviour
{
    public UnityEvent OnStartActions;
    public UnityEvent OnUpdateActions;
    public UnityEvent OnEndActions;

    public Condition conditions;

    public abstract void OnStart(IEntity entity);
    public abstract void RunCurrentState(IEntity entity);
    public abstract void OnExit(IEntity entity);

}
