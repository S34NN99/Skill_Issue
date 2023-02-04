using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour, IEntity
{
    public StateMachine SM { get; set; }

    public GameObject curentEntity { get; set; }
    public State StateToStart;

    // Start is called before the first frame update
    void Start()
    {
        curentEntity = this.gameObject;
        SM = new(this);
        SM.owner = this;

        SM.SetState(StateToStart);
    }

    private void Update()
    {
        SM.UpdateStates();
    }

}
