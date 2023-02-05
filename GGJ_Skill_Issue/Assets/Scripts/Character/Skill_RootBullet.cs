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
            if (!player.unlockedSkills.Contains(type))
            {
                return;
            }
            oriPrefab = rangedAttack.projectilePrefab;
            rangedAttack.SetProjectilePrefab(bulletPrefab);
            rangedAttack.UseAttack();
            rangedAttack.SetProjectilePrefab(oriPrefab);

        }
    }
}
