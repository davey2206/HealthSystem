using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField, HideInInspector] public bool UseShield;
    [SerializeField, HideInInspector] public bool UseArmor;
    [SerializeField, HideInInspector] public bool UseEvents;
    [SerializeField, HideInInspector] public float MaxHealth;
    [SerializeField, HideInInspector] public float MaxShield;
    [SerializeField, HideInInspector] public float Armor;
    [SerializeField, HideInInspector] public AnimationCurve DamageReduction;
    [SerializeField, HideInInspector] public AnimationCurve DamageReductionShield;
    [SerializeField, HideInInspector] public UnityEvent HitEvents;
    [SerializeField, HideInInspector] public UnityEvent HealEvents;
    [SerializeField, HideInInspector] public UnityEvent DieEvents;

    private float health;
    private float shield;

    private void Start()
    {
        health = MaxHealth;
        shield = MaxShield;
    }
    public void TakeDamage(float damage)
    {
        if (UseShield && shield > 0)
        {
            shield -= ApplyDamageReductionShield(damage);

            if (shield < 0)
            {
                float overkillDamage = damage + shield;
                health -= ApplyDamageReduction(overkillDamage);
            }
        }
        else
        {
            health -= ApplyDamageReduction(damage);
        }

        if (health <= 0)
        {
            DieEvents.Invoke();
        }
        else
        {
            HitEvents.Invoke();
        }
    }
    public void Heal(float hp)
    {
        health += hp;

        if (health > MaxHealth)
        {
            health = MaxHealth;
        }

        HealEvents.Invoke();
    }
    public void HealShield(float hp)
    {
        shield += hp;

        if (shield > MaxShield)
        {
            shield = MaxShield;
        }
    }
    public void AddArmor(float armor)
    {
        Armor += armor;
    }
    public void RemoveArmor(float armor)
    {
        Armor -= armor;

        if (Armor < 0)
        {
            Armor = 0;
        }
    }
    public void StartDamageOverTime(float tickTime, int totalTicks, float tickDamage)
    {
        StartCoroutine(DamageOverTime(tickTime, totalTicks, tickDamage));
    }
    private float ApplyDamageReduction(float damage)
    {
        if (!UseArmor)
        {
            return damage;
        }

        damage = damage - (damage / 100 * DamageReduction.Evaluate(Armor));

        return damage;
    }
    private float ApplyDamageReductionShield(float damage)
    {
        if (!UseArmor)
        {
            return damage;
        }

        damage = damage - (damage / 100 * DamageReductionShield.Evaluate(Armor));

        return damage;
    }
    IEnumerator DamageOverTime(float tickTime, int totalTicks, float tickDamage)
    {
        for (int i = 0; i < totalTicks; i++)
        {
            TakeDamage(tickDamage);
            yield return new WaitForSeconds(tickTime);
        }
    }
}
