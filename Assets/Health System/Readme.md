# Health System

This is a easy to configure health system that allows designers and develepers to quickly add the desired health system to any object. 

## How to use
1. Add the Health.cs to the object that needs a health system.
2. Configure the Health system to what is needed by using the Option toggles.
3. (optional) link events to the desired scripts (for example a health bar script).

## Options

The health system has alot of options this is what they do:

### Health
The basic health value of the object. you can damage and heal the health by using the TakeDamage() and Heal() functions.
### Shield
The shield works the same as health with the exception that when a object take damage with the TakeDamage() function it wil first damage the shield. You can heal the shield using the HealShield() function.
### Armor
Armor is a value that can be customized using the DamageReduction and DamageReductionShield AnimationCurve. In these animation curese the Time = Armor and the % damage reduction = value. 
DamageReduction wil reduce the damage taken to the health and the DamageReductionShield wil reduce the damage taken to the shield
### Regen
Regen allows the player to heal x amount of health every x amount of time after waiting x amount seconds after takeing damage. this works the same for the shield but here you can also decide if you want the shield only to start regening after the health is full or not.

Regen = Heal x (RegenAmount) health every x (RegenSpeed) seconds if not taking damage for x (RegenCooldown) seconds amount of time.

RegenShield = Heal x (RegenShieldAmount) shield every x (RegenShieldSpeed) seconds if not taking damage for x (RegenshieldCooldown) seconds amount of time And health is full (optional).
### Events
There are 6 events that can be triggered
- Hit Event = Triggers when the Health takes damage using the TakeDamage() function.
- Heal Event = Triggers when Health is healed using the Heal() function.
- Die Event = Triggers when Health <= 0 using the TakeDamage() function.
- Hit Shield Event = Triggers when the Shield takes damage using the TakeDamage() function.
- Heal Shield Event = Triggers when Shield is healed using the HealShiled() function.
- Destroy Shield Event = Triggers when Shield <= 0 using the TakeDamage() function.
