using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public Player player;
    int attackDamage, attackRange, mpCost;
    float cooldownInSecs, cooldownCache;
    bool isOnCooldown, drainCooldown;

    public virtual void OnSkillCast() { }
    void SetOnCooldown(bool state)
    {
        drainCooldown = state;
        if (drainCooldown && !isOnCooldown)
        {
            cooldownCache = cooldownInSecs;
        }
        if (!state)
        {
            isOnCooldown = false;
        }
    }

    void DrainCooldown()
    {
        if (drainCooldown)
        {
            isOnCooldown = drainCooldown;
            if (cooldownCache <= 0)
            {
                drainCooldown = false;
                isOnCooldown = false;
            }
            else
            {
                cooldownCache -= Time.deltaTime;
            }
        }
    }

    private void Update()
    {
        DrainCooldown();
    }

}
