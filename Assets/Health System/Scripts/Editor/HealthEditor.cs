using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Health))]
public class HealthEditor : Editor
{
    Health hp;

    SerializedProperty HealthProperty;

    SerializedProperty UseShieldProperty;
    SerializedProperty ShieldProperty;

    SerializedProperty UseArmorProperty;
    SerializedProperty ArmorProperty;
    SerializedProperty DamageReductionProperty;
    SerializedProperty DamageReductionShieldProperty;

    SerializedProperty UseRegenProperty;
    SerializedProperty RegenAmountProperty;
    SerializedProperty RegenSpeedProperty;
    SerializedProperty RegenCooldownProperty;
    SerializedProperty HealthMustBeFullProperty;
    SerializedProperty RegenShieldAmountProperty;
    SerializedProperty RegenShieldSpeedProperty;
    SerializedProperty RegenShieldCooldownProperty;

    SerializedProperty UseEventProperty;
    SerializedProperty HitEventProperty;
    SerializedProperty HealEventsProperty;
    SerializedProperty DieEventsProperty;
    SerializedProperty SetupEventsProperty;
    SerializedProperty HitShieldEventProperty;
    SerializedProperty HealShieldEventsProperty;
    SerializedProperty DestroyShieldEventsProperty;
    SerializedProperty SetupShieldEventsProperty;

    SerializedProperty DebugButtonProperty;

    bool showOptions = false;
    bool showEvents = false;
    bool showShieldEvents = false;

    private void OnEnable()
    {
        //get properties from health script
        HealthProperty = serializedObject.FindProperty(nameof(Health.MaxHealth));
        ShieldProperty = serializedObject.FindProperty(nameof(Health.MaxShield));

        ArmorProperty = serializedObject.FindProperty(nameof(Health.Armor));
        DamageReductionProperty = serializedObject.FindProperty(nameof(Health.DamageReduction));
        DamageReductionShieldProperty = serializedObject.FindProperty(nameof(Health.DamageReductionShield));

        HitEventProperty = serializedObject.FindProperty(nameof(Health.HitEvents));
        HealEventsProperty = serializedObject.FindProperty(nameof(Health.HealEvents));
        DieEventsProperty = serializedObject.FindProperty(nameof(Health.DieEvents));
        HitShieldEventProperty = serializedObject.FindProperty(nameof(Health.HitShieldEvents));
        HealShieldEventsProperty = serializedObject.FindProperty(nameof(Health.HealShieldEvents));
        DestroyShieldEventsProperty = serializedObject.FindProperty(nameof(Health.DestroyShieldEvents));
        SetupEventsProperty = serializedObject.FindProperty(nameof(Health.SetupEvents));
        SetupShieldEventsProperty = serializedObject.FindProperty(nameof(Health.SetupShieldEvents));

        RegenAmountProperty = serializedObject.FindProperty(nameof(Health.RegenAmount));
        RegenSpeedProperty = serializedObject.FindProperty(nameof(Health.RegenSpeed));
        RegenCooldownProperty = serializedObject.FindProperty(nameof(Health.RegenCooldown));

        HealthMustBeFullProperty = serializedObject.FindProperty(nameof(Health.HealthMustBeFull));
        RegenShieldAmountProperty = serializedObject.FindProperty(nameof(Health.RegenShieldAmount));
        RegenShieldSpeedProperty = serializedObject.FindProperty(nameof(Health.RegenShieldSpeed));
        RegenShieldCooldownProperty = serializedObject.FindProperty(nameof(Health.RegenShieldCooldown));

        UseShieldProperty = serializedObject.FindProperty(nameof(Health.UseShield));
        UseArmorProperty = serializedObject.FindProperty(nameof(Health.UseArmor));
        UseEventProperty = serializedObject.FindProperty(nameof(Health.UseEvents));
        UseRegenProperty = serializedObject.FindProperty(nameof(Health.UseRegen));

        DebugButtonProperty = serializedObject.FindProperty(nameof(Health.DebugButtons));
    }

    public override void OnInspectorGUI()
    {
        hp = (Health)target;

        DrawDefaultInspector();

        if (UseShieldProperty.boolValue)
        {
            EditorGUI.ProgressBar(GUILayoutUtility.GetRect(25, 25, "TextField"), hp.CurrentHealth / hp.MaxHealth, hp.CurrentHealth + " Health");
            EditorGUILayout.Space();
            EditorGUI.ProgressBar(GUILayoutUtility.GetRect(25, 25, "TextField"), hp.CurrentShield / hp.MaxShield, hp.CurrentShield + " Shield");
        }
        else
        {
            EditorGUI.ProgressBar(GUILayoutUtility.GetRect(50, 50, "TextField"), hp.CurrentHealth / hp.MaxHealth, hp.CurrentHealth + " Health");
        }

        EditorGUILayout.Space();

        serializedObject.Update();

        //Options
        showOptions = EditorGUILayout.BeginFoldoutHeaderGroup(showOptions, "Options");

        if (showOptions)
        {
            EditorGUILayout.PropertyField(UseShieldProperty);
            EditorGUILayout.PropertyField(UseArmorProperty);
            EditorGUILayout.PropertyField(UseEventProperty);
            EditorGUILayout.PropertyField(UseRegenProperty);
            EditorGUILayout.PropertyField(DebugButtonProperty);
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space();

        //Health
        EditorGUILayout.LabelField("Health", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(HealthProperty);

        EditorGUILayout.Space();

        //shield
        if (UseShieldProperty.boolValue)
        {
            EditorGUILayout.LabelField("Shields", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(ShieldProperty);

            EditorGUILayout.Space();
        }

        //armor without shield
        if (UseArmorProperty.boolValue && !UseShieldProperty.boolValue)
        {
            EditorGUILayout.LabelField("Armor", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(ArmorProperty);
            EditorGUILayout.PropertyField(DamageReductionProperty);

            EditorGUILayout.Space();
        }

        //armor with shield
        if (UseArmorProperty.boolValue && UseShieldProperty.boolValue)
        {
            EditorGUILayout.LabelField("Armor", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(ArmorProperty);
            EditorGUILayout.PropertyField(DamageReductionProperty);
            EditorGUILayout.PropertyField(DamageReductionShieldProperty);

            EditorGUILayout.Space();
        }

        //regen
        if (UseRegenProperty.boolValue && !UseShieldProperty.boolValue)
        {
            EditorGUILayout.LabelField("Regen", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(RegenAmountProperty);
            EditorGUILayout.PropertyField(RegenSpeedProperty);
            EditorGUILayout.PropertyField(RegenCooldownProperty);

            EditorGUILayout.Space();
        }

        if (UseRegenProperty.boolValue && UseShieldProperty.boolValue)
        {
            EditorGUILayout.LabelField("Regen", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(RegenAmountProperty);
            EditorGUILayout.PropertyField(RegenSpeedProperty);
            EditorGUILayout.PropertyField(RegenCooldownProperty);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Shield Regen", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(HealthMustBeFullProperty);
            EditorGUILayout.PropertyField(RegenShieldAmountProperty);
            EditorGUILayout.PropertyField(RegenShieldSpeedProperty);
            EditorGUILayout.PropertyField(RegenShieldCooldownProperty);

            EditorGUILayout.Space();
        }

        //Events
        if (UseEventProperty.boolValue && !UseShieldProperty.boolValue)
        {
            showEvents = EditorGUILayout.BeginFoldoutHeaderGroup(showEvents, "Events");

            if (showEvents)
            {
                EditorGUILayout.PropertyField(HitEventProperty);
                EditorGUILayout.PropertyField(HealEventsProperty);
                EditorGUILayout.PropertyField(DieEventsProperty);
                EditorGUILayout.PropertyField(SetupEventsProperty);
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
            

            EditorGUILayout.Space();
        }

        if (UseEventProperty.boolValue && UseShieldProperty.boolValue)
        {
            showEvents = EditorGUILayout.BeginFoldoutHeaderGroup(showEvents, "Events");

            if (showEvents)
            {
                EditorGUILayout.PropertyField(HitEventProperty);
                EditorGUILayout.PropertyField(HealEventsProperty);
                EditorGUILayout.PropertyField(DieEventsProperty);
                EditorGUILayout.PropertyField(SetupEventsProperty);
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorGUILayout.Space();

            showShieldEvents = EditorGUILayout.BeginFoldoutHeaderGroup(showShieldEvents, "Shield Events");

            if (showShieldEvents)
            {
                EditorGUILayout.PropertyField(HitShieldEventProperty);
                EditorGUILayout.PropertyField(HealShieldEventsProperty);
                EditorGUILayout.PropertyField(DestroyShieldEventsProperty);
                EditorGUILayout.PropertyField(SetupShieldEventsProperty);
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorGUILayout.Space();
        }

        if (DebugButtonProperty.boolValue)
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Take 10% Damage"))
            {
                float damage = hp.MaxHealth * 0.1f;
                hp.TakeDamage(damage);
            }

            if (GUILayout.Button("Heal 10% health"))
            {
                float health = hp.MaxHealth * 0.1f;
                hp.Heal(health);
            }
            if (GUILayout.Button("Heal 10% Shield"))
            {
                float shield = hp.MaxShield * 0.1f;
                hp.HealShield(shield);
            }
            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();

        if (EditorApplication.isPlaying)
        {
            Repaint();
        }
    }
}
