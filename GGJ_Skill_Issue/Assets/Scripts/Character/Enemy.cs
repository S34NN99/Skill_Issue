using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IEntity
{
    public StateMachine SM { get; set; }
    public GameObject curentEntity { get; set; }

    [SerializeField] private State StateToStart;
    public int damageToHit;

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
