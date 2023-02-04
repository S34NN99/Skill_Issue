using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public List<string> hostileTags;

    [SerializeField] private int baseAttackDamage;
    public int BaseAttackDamager => baseAttackDamage;

    [SerializeField] private int baseAttackRange;
    public int BaseAttackRange => baseAttackRange;
     
    [SerializeField] private float waitForNextAttack;
    public float WaitForNextAttack => waitForNextAttack;

    [SerializeField] protected float currentWaitTime;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask enemyLayers;

    public abstract void UseAttack();
}
