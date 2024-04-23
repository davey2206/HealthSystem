using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Health))]
public class HealthEditor : Editor
{
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

    SerializedProperty UseEventProperty;
    SerializedProperty HitEventProperty;
    SerializedProperty HealEventsProperty;
    SerializedProperty DieEventsProperty;

    bool showOptions = false;

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

        RegenAmountProperty = serializedObject.FindProperty(nameof(Health.RegenAmount));
        RegenSpeedProperty = serializedObject.FindProperty(nameof(Health.RegenSpeed));
        RegenCooldownProperty = serializedObject.FindProperty(nameof(Health.RegenCooldown));

        UseShieldProperty = serializedObject.FindProperty(nameof(Health.UseShield));
        UseArmorProperty = serializedObject.FindProperty(nameof(Health.UseArmor));
        UseEventProperty = serializedObject.FindProperty(nameof(Health.UseEvents));
        UseRegenProperty = serializedObject.FindProperty(nameof(Health.UseRegen));
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.Update();


        //Options
        showOptions = EditorGUILayout.BeginFoldoutHeaderGroup(showOptions, "Options");

        if (showOptions)
        {
            EditorGUILayout.PropertyField(UseShieldProperty);
            EditorGUILayout.PropertyField(UseArmorProperty);
            EditorGUILayout.PropertyField(UseEventProperty);
            EditorGUILayout.PropertyField(UseRegenProperty);
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
        if (UseRegenProperty.boolValue)
        {
            EditorGUILayout.LabelField("Regen", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(RegenAmountProperty);
            EditorGUILayout.PropertyField(RegenSpeedProperty);
            EditorGUILayout.PropertyField(RegenCooldownProperty);
        }

        //Events
        if (UseEventProperty.boolValue)
        {
            EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(HitEventProperty);
            EditorGUILayout.PropertyField(HealEventsProperty);
            EditorGUILayout.PropertyField(DieEventsProperty);

            EditorGUILayout.Space();
        }

        if (GUILayout.Button("test"))
        {
            Health hp = target as Health;
            hp.TakeDamage(5);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
