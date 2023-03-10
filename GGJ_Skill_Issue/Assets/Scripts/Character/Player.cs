using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public int mp, sp;
    public List<SkillType> unlockedSkills;
    public bool SpendMP(int cost)
    {
        if (cost > mp)
        {
            return false;
        }
        mp -= cost;
        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Boss"))
        {
            Boss boss = collision.gameObject.GetComponentInParent<Boss>();
            Damage(boss.damageToHit);
        }
    }
}
