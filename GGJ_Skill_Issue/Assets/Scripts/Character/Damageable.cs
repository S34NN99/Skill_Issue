using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent DamageEvents;
    public UnityEvent DeathEvents;

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
        Debug.Log("Dieman");
        DeathEvents?.Invoke();
    }

    void DebugBugMan()
    {
        Destroy(gameObject);
    }
}
