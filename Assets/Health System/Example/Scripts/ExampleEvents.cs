using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleEvents : MonoBehaviour
{
    //All events give the amount healed/damaged, health and max health (works the same for shield events).

    /// <summary>
    /// This function is linked to the hit event. 
    /// The hit event fires everytime the object with the health script takes damage to his health through the TakeDamage() function.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="health"></param>
    /// <param name="maxHealth"></param>
    public void HealthHit(float damage, float health, float maxHealth)
    {
        Debug.Log("Health hit event: Damage dealt " + damage + " Current Health " + health + " Max Health " + maxHealth);
    }

    /// <summary>
    /// This function is linked to the heal event. 
    /// The heal event fires everytime the object with the health script gets healed through the Heal() function.
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="health"></param>
    /// <param name="maxHealth"></param>
    public void HealthHeal(float amount, float health, float maxHealth)
    {
        Debug.Log("Health heal event: Heal amount " + amount + " Current Health " + health + " Max Health " + maxHealth);
    }

    /// <summary>
    /// This function is linked to the die event. 
    /// The die event fires everytime the object with the health script hits 0 health by taking damage through the TakeDamage() function.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="health"></param>
    /// <param name="maxHealth"></param>
    public void Die(float damage, float health, float maxHealth)
    {
        Debug.Log("Die event: Damage dealt " + damage + " Current Health " + health + " Max Health " + maxHealth);
    }

    /// <summary>
    /// This function is linked to the hit shield event. 
    /// The hit shield event fires everytime the object with the health script takes damage to his shield through the TakeDamage() function.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="Shield"></param>
    /// <param name="maxShield"></param>
    public void ShieldHit(float damage, float Shield, float maxShield)
    {
        Debug.Log("Shield hit event: Damage dealt " + damage + " Current Shield " + Shield + " Max Shield " + maxShield);
    }

    /// <summary>
    /// This function is linked to the heal shield event. 
    /// The heal shield event fires everytime the object with the health script gets healed through the HealShield() function.
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="Shield"></param>
    /// <param name="maxShield"></param>
    public void ShieldHeal(float amount, float Shield, float maxShield)
    {
        Debug.Log("Shield heal event: Heal amount " + amount + " Current Shield " + Shield + " Max Shield " + maxShield);
    }

    /// <summary>
    /// This function is linked to the Destroy Shield event. 
    /// The Destroy Shield event fires everytime the object with the health script hits 0 shield by taking damage through the TakeDamage() function.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="Shield"></param>
    /// <param name="maxShield"></param>
    public void ShieldDestoyed(float damage, float Shield, float maxShield)
    {
        Debug.Log("Shield destoyed event: Damage dealt " + damage + " Current Shield " + Shield + " Max Shield " + maxShield);
    }
}
