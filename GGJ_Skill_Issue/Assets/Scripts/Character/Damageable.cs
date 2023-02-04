using System;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityAction DamageEvents;
    public UnityAction DeathEvents;

    [SerializeField] private int maxHP;
    public int MaxHP => maxHP;
    private int hp;
    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    [SerializeField] private int toBeDamaged;
    public int ToBeDamaged => toBeDamaged;

    [SerializeField] private bool willNotDie;
    public bool WillNotDie => willNotDie;

    public bool Testing;

    private void Awake()
    {
        hp = MaxHP;
        Debug.Log(hp + " " + MaxHP + " " + gameObject.name);
    }

    private void Update()
    {
        if (Testing)
        {
            Damage(5);
            Testing = false;
        }
    }

    public void Damage(int damagePoints)
    {
        Debug.Log($"{damagePoints} hit to {this.gameObject.name} and {hp} and {willNotDie}");
        HP -= damagePoints;
        toBeDamaged = damagePoints;
        if (HP <= 0 && !WillNotDie)
        {
            OnDeath();
        }
        OnDamage();
    }

    public void Heal(int healPoints)
    {
        HP += healPoints;
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
