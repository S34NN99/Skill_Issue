using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Attack
{

    public void Update()
    {
        if(currentWaitTime > 0)
        {
            currentWaitTime -= Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.Q) && currentWaitTime <= 0)
        {
            UseAttack();
        }
    }

    public override void UseAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, BaseAttackRange, enemyLayers);

        foreach (var enemy in hitEnemies)
        {
            Debug.Log(enemy.name + " is hit");
        }

        currentWaitTime = WaitForNextAttack;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, BaseAttackRange);
    }
}
