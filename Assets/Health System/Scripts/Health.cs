using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [HideInInspector] public bool UseShield;
    [HideInInspector] public bool UseArmor;
    [HideInInspector] public bool UseRegen;
    [HideInInspector] public bool UseEvents;
    [HideInInspector] public bool DebugButtons;
    [HideInInspector] public float MaxHealth;
    [HideInInspector] public float MaxShield;
    [HideInInspector] public float Armor;
    [HideInInspector] public AnimationCurve DamageReduction;
    [HideInInspector] public AnimationCurve DamageReductionShield;
    [HideInInspector] public float RegenAmount;
    [HideInInspector] public float RegenSpeed;
    [HideInInspector] public float RegenCooldown;
    [HideInInspector] public UnityEvent HitEvents;
    [HideInInspector] public UnityEvent HealEvents;
    [HideInInspector] public UnityEvent DieEvents;

    public float CurrentHealth => health;
    public float CurrentShield => shield;

    private float health;
    private float shield;
    private bool canRegen;
    private Coroutine regenStart;

    private void Start()
    {
        health = MaxHealth;
        shield = MaxShield;
        if (UseRegen)
        {
            canRegen = true;
            regenStart = StartCoroutine(StartRegen());
        }
    }
    public void TakeDamage(float damage)
    {
        if (UseRegen)
        {
            StopCoroutine(regenStart);
            canRegen = false;
            regenStart = StartCoroutine(StartRegen());
        }

        if (UseShield && shield > 0)
        {
            shield -= ApplyDamageReductionShield(damage);

            if (shield < 0)
            {
                float overkillDamage = damage + shield;
                health -= ApplyDamageReduction(overkillDamage);
                shield = 0;
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
    IEnumerator StartRegen()
    {
        yield return new WaitForSeconds(RegenCooldown);
        canRegen = true;
        StartCoroutine(RegenHealth());
    }
    IEnumerator RegenHealth()
    {
        while (canRegen)
        {
            Heal(RegenAmount);
            yield return new WaitForSeconds(RegenSpeed);
        }
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
