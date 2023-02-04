using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent DamageEvents;
    public UnityEvent DeathEvents;

    [SerializeField] int hp;
    public int HP => hp;

    private void Start()
    {
        DeathEvents.AddListener( () => Destroy(this.gameObject));
        DeathEvents.AddListener(() => Debug.Log("hello"));

    }

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
        Debug.Log("Dieman");
        DeathEvents?.Invoke();
    }

    void DebugBugMan()
    {
        Destroy(gameObject);
    }
}
