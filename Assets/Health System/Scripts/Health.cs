using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] public bool UseShield;
    [SerializeField] public bool UseArmor;
    [SerializeField] public bool UseEvents;

    [Header("Health")]
    [SerializeField] private float MaxHealth;

    [SerializeField, HideInInspector] public float MaxShield;
    [SerializeField, HideInInspector] public float Armor;
    [SerializeField, HideInInspector] public AnimationCurve DamageReduction;
    [SerializeField, HideInInspector] public AnimationCurve DamageReductionShield;
    [SerializeField, HideInInspector] public UnityEvent HitEvents;
    [SerializeField, HideInInspector] public UnityEvent HealEvents;
    [SerializeField, HideInInspector] public UnityEvent DieEvents;

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
