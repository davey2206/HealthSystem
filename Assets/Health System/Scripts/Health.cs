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
    [HideInInspector] public bool HealthMustBeFull;
    [HideInInspector] public float RegenShieldAmount;
    [HideInInspector] public float RegenShieldSpeed;
    [HideInInspector] public float RegenShieldCooldown;
    [HideInInspector] public UnityEvent<float, float, float> HitEvents;
    [HideInInspector] public UnityEvent<float, float, float> HealEvents;
    [HideInInspector] public UnityEvent<float, float, float> DieEvents;
    [HideInInspector] public UnityEvent<float, float, float> DestroyShieldEvents;
    [HideInInspector] public UnityEvent<float, float, float> HitShieldEvents;
    [HideInInspector] public UnityEvent<float, float, float> HealShieldEvents;

    public float CurrentHealth => health;
    public float CurrentShield => shield;

    private float health;
    private float shield;
    private bool canRegen;
    private bool canShieldRegen;
    private Coroutine regenStart;
    private Coroutine regenShieldStart;

    private void Start()
    {
        health = MaxHealth;
        shield = MaxShield;

        canRegen = true;
        canShieldRegen = true;
        regenStart = StartCoroutine(StartRegen());
        regenShieldStart = StartCoroutine(StartShieldRegen());
    }
    public void TakeDamage(float damage)
    {
        checkRegen();

        if (UseShield && shield > 0)
        {
            shield -= ApplyDamageReductionShield(damage);

            if (shield < 0)
            {
                health -= ApplyDamageReduction(Mathf.Abs(shield));
                shield = 0;

                DestroyShieldEvents.Invoke(damage, shield, MaxShield);
            }
            else
            {
                HitShieldEvents.Invoke(damage, shield, MaxShield);
            }
        }
        else
        {
            health -= ApplyDamageReduction(damage);
        }

        if (health <= 0)
        {
            health = 0;
            DieEvents.Invoke(damage, health, MaxHealth);
        }
        
        if(health > 0 && shield <= 0)
        {
            HitEvents.Invoke(damage, health, MaxHealth);
        }
    }
    public void Heal(float hp)
    {
        health += hp;

        if (health > MaxHealth)
        {
            health = MaxHealth;
        }

        HealEvents.Invoke(hp, health, MaxHealth);
    }
    public void HealShield(float hp)
    {
        shield += hp;

        if (shield > MaxShield)
        {
            shield = MaxShield;
        }

        HealShieldEvents.Invoke(hp, shield, MaxShield);
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
    private void checkRegen()
    {
        StopCoroutine(regenStart);
        canRegen = false;
        regenStart = StartCoroutine(StartRegen());

        StopCoroutine(regenShieldStart);
        canShieldRegen = false;
        regenShieldStart = StartCoroutine(StartShieldRegen());
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
    private IEnumerator StartRegen()
    {
        yield return new WaitForSeconds(RegenCooldown);
        canRegen = true;
        StartCoroutine(RegenHealth());
    }
    private IEnumerator RegenHealth()
    {
        while (canRegen)
        {
            if (UseRegen)
            {
                Heal(RegenAmount);
            }
            if (RegenSpeed <= 0)
            {
                RegenSpeed = 0.1f;
            }
            yield return new WaitForSeconds(RegenSpeed);
        }
    }
    private IEnumerator StartShieldRegen()
    {
        yield return new WaitForSeconds(RegenShieldCooldown);
        canShieldRegen = true;
        StartCoroutine(RegenShieldHealth());
    }
    private IEnumerator RegenShieldHealth()
    {
        while (canShieldRegen)
        {
            if (HealthMustBeFull && health == MaxHealth && UseRegen)
            {
                HealShield(RegenShieldAmount);
            }

            if(!HealthMustBeFull && UseRegen)
            {
                HealShield(RegenShieldAmount);
            }

            if (RegenShieldSpeed <= 0)
            {
                RegenShieldSpeed = 0.1f;
            }

            yield return new WaitForSeconds(RegenShieldSpeed);
        }
    }
    private IEnumerator DamageOverTime(float tickTime, int totalTicks, float tickDamage)
    {
        for (int i = 0; i < totalTicks; i++)
        {
            TakeDamage(tickDamage);
            yield return new WaitForSeconds(tickTime);
        }
    }
}
