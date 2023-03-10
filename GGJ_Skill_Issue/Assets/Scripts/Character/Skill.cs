using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum SkillType
{
    RootBullet,
    RootSpike
}

public class Skill : MonoBehaviour
{
    public Player player;
    public int mpCost, attackDamage;
    public float cooldownInSecs, attackRange, attackDuration, cooldownCache, durationCache;
    protected bool isOnCooldown = false, drainCooldown = false;
    public KeyCode castKey;
    public SkillType type;

    private void Start()
    {
    }

    public virtual void OnSkillCast() 
    {
        if (!player.unlockedSkills.Contains(type))
        {
            return;
        }
        SetOnCooldown(true);
    }

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
        UpdateFunc();
    }

    protected void UpdateFunc()
    {
        DrainCooldown();
        if (Input.GetKeyDown(castKey) && !isOnCooldown)
        {
            OnSkillCast();
        }
    }

}
