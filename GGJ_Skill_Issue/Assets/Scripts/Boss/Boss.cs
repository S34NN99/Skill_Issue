using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character, IEntity
{
    public StateMachine SM { get; set; }
    public GameObject curentEntity { get; set; }

    [SerializeField] private State StateToStart;
    public int damageToHit;
    public GameObject endScene;

    private void Start()
    {
        curentEntity = this.gameObject;
        SM = new(this);
        SM.owner = this;

        SM.SetState(StateToStart);
        DeathEvents += () => endScene.SetActive(true);
        DeathEvents += () => Time.timeScale = 0;
    }

    private void Update()
    {
        SM.UpdateStates();
    }



}
