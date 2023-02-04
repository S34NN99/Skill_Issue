using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] private Damageable damageableLeft;
    [SerializeField] private Damageable damageableRight;
    [SerializeField] private Damageable boss;
    private void Start()
    {
        damageableLeft.DamageEvents += () => boss.Damage(damageableLeft.ToBeDamaged);
        damageableRight.DamageEvents += () => boss.Damage(damageableRight.ToBeDamaged);
    }
}
