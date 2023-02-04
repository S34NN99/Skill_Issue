using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public Player player;
    public int attackDamage, attackRange, mpCost;
    public float cooldownInSecs;

    public virtual void OnSkillCast() { }



}
