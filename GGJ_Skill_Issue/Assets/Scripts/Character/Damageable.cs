using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent DamageEvents;
    public UnityEvent DeathEvents;

    int hp;
    public int HP => hp;

    public void Damage(int damagePoints)
    {
        hp -= damagePoints;
        if (hp<=0)
        {
            OnDeath();
        }
        OnDamage();
    }

    public void Heal(int healPoints)
    {
        hp += healPoints;
    }


    void OnDamage()
    {
        DamageEvents?.Invoke();
    }

    void OnDeath()
    {
        DeathEvents?.Invoke();
    }

}
