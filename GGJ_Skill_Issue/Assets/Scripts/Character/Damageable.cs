using System;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityAction DamageEvents;
    public UnityAction DeathEvents;

    [SerializeField] int hp;
    public int HP => hp;

    [SerializeField] private bool willNotdie;
    public bool WillNotDie => willNotdie;

    private void Start()
    {
        DeathEvents.AddListener( () => Destroy(this.gameObject));
    }

    public void Damage(int damagePoints)
    {
        hp -= damagePoints;
        if (hp<=0 && !WillNotDie)
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
        DeathEvents += DeathToThis;
        DeathEvents?.Invoke();
    }

    public void DeathToThis()
    {
        Destroy(gameObject);
    }
}
