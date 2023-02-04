using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State currentState;
    public IEntity owner;

    public StateMachine(IEntity owner)
    {
        this.owner = owner;
    }

    public void SetState(State stateToChange)
    {
        if(currentState == null && stateToChange != null)
        {
            currentState = stateToChange;
            currentState.OnStart(owner);
        }
    }

    public void UpdateStates()
    {
        currentState.RunCurrentState(owner);
    }

    public void ExitState() 
    {
        currentState.OnExit(owner);
        currentState = null; 
    }
}
