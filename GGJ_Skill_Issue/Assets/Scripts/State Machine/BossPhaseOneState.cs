using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseOneState : State
{
    public override void OnStart(IEntity entity)
    {
        Debug.Log("Get the player");
    }

    public override void RunCurrentState(IEntity entity)
    {
        //bossAttack.UseAttack();
        OnUpdateActions?.Invoke();
    }

    public override void OnExit(IEntity entity)
    {
        Debug.Log("Get the player");

    }

}
