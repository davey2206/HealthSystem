using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float MaxHealth;
    [SerializeField] private float MaxShield;
    [SerializeField] private float Armor;
    [SerializeField] private AnimationCurve DamageReduction;
    [SerializeField] private AnimationCurve DamageReductionShield;
    [SerializeField] private UnityEvent HitEvents;
    [SerializeField] private UnityEvent HealEvents;
    [SerializeField] private UnityEvent DieEvents;

    private float health;
    private float shield;

    public void TakeDamage(float damage)
    {

    }
    public void Heal(float hp)
    {

    }
    public void HealShield(float hp)
    {

    }
    public void AddArmor(float armor)
    {

    }
    public void RemoveArmor(float armor)
    {

    }
    public void StartDamageOverTime(float tickTime, float totalTime, float tickDamage)
    {

    }
    private float ApplyDamageReduction(float damage)
    {
        return 0;
    }
    private float ApplyDamageReductionShield(float damage)
    {
        return 0;
    }
    IEnumerator DamageOverTime(float tickTime, float totalTime, float tickDamage)
    {
        return null;
    }
}
