using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character, IEntity
{
    public StateMachine SM { get; set; }

    public GameObject curentEntity { get; set; }

    [SerializeField] private List<Transform> hands;
    [SerializeField] private State StateToStart;

    private void Start()
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
