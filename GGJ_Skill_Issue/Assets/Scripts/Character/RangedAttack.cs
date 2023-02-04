using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : Attack
{
    Character attacker;
    public GameObject projectilePrefab;
    List<Projectile> activeProjectileList = new List<Projectile>();
    public int activeProjectileLimit;

    void Start()
    {
        attacker = gameObject.GetComponentInParent<Character>();
    }
    public void Update()
    {
        if (currentWaitTime > 0)
        {
            currentWaitTime -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.J) && currentWaitTime <= 0)
        {
            UseAttack();
        }

        ProcessProjectilesCourse();
    }

    public void SetBulletPrefab(GameObject inPrefab)
    {
        projectilePrefab = inPrefab;
    }

    public override void UseAttack()
    {
        Vector3 shotDirection = (attackPoint.position - attacker.transform.position).normalized;

        if (activeProjectileList.Count < activeProjectileLimit)
        {
            Projectile tempProjectile = Instantiate(projectilePrefab, transform).GetComponent<Projectile>();

            tempProjectile.SetDirection(new Vector3(shotDirection.x, shotDirection.y, shotDirection.z));

            activeProjectileList.Add(tempProjectile);
        }

        currentWaitTime = WaitForNextAttack;
    }

    void ProcessProjectilesCourse()
    {
        List<int> deathIndex = new List<int>();
        foreach (Projectile proj in activeProjectileList)
        {
            proj.ProcessCourse();
            hostileTags.ForEach(proj.CheckTagsAndDamage);
            
            if (proj.ToDestroy)
            {
                deathIndex.Add(activeProjectileList.IndexOf(proj));
                proj.ProcessUnaliveSelf();
            }
        }

        deathIndex.ForEach(activeProjectileList.RemoveAt);
    }
}
