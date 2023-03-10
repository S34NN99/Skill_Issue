using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class IdleState : State
{
    public State StateToChangeTo;
    private Dictionary<string, bool> ConditionDict = new Dictionary<string, bool>();
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private int detectRange;

    private void PutInConditions(string s)
    {
        ConditionDict.Add(s, false);
        conditions.AddCondition(s, () =>
        {
            ConditionDict[s] = SeenPlayer();
            //ConditionDict[s] = true;
        });
    }

    public bool SeenPlayer()
    {
        //Collider2D[] collided = Physics2D.OverlapCircleAll(transform.position, detectRange, playerMask);
        Collider2D[] collided = Physics2D.OverlapBoxAll(transform.position, transform.localScale * detectRange, playerMask);
        List<Collider2D> listOfHostiles = new List<Collider2D>(collided);
        Collider2D go = listOfHostiles.Find(x => x.CompareTag("Player"));

        if (go == null)
            return false;
        else
            return true;
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(transform.position, detectRange);
        //Gizmos.DrawCube(transform.position, transform.localScale * detectRange);
    }

    public override void OnStart(IEntity entity)
    {
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
}
