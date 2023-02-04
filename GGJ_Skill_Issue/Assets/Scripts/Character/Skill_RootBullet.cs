using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_RootBullet : Skill
{
    public GameObject bulletPrefab, oriPrefab;
    public RangedAttack rangedAttack;

    public override void OnSkillCast()
    {
        if (player.SpendMP(mpCost))
        {
            base.OnSkillCast();

            oriPrefab = rangedAttack.projectilePrefab;
            rangedAttack.SetProjectilePrefab(bulletPrefab);
            rangedAttack.UseAttack();
            rangedAttack.SetProjectilePrefab(oriPrefab);

        }
    }
}
