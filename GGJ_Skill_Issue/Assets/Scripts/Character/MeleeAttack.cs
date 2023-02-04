using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Attack
{
    public Damageable hostile;

    public void Update()
    {
        if(currentWaitTime > 0)
        {
            currentWaitTime -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Q) && currentWaitTime <= 0)
        {
            UseAttack();
        }
    }

    public override void UseAttack()
    {
        Collider2D[] collided = Physics2D.OverlapCircleAll(attackPoint.position, BaseAttackRange, enemyLayers);
        List<Collider2D> listOfHostiles = new List<Collider2D>(collided);
        hostile = listOfHostiles.Find(x => x.CompareTag("Enemy"))?.gameObject.GetComponent<Damageable>();
        if (hostile == null)
        {
            return;
        }
        hostile.Damage(BaseAttackDamager);

        currentWaitTime = WaitForNextAttack;
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, BaseAttackRange);
    }
}
