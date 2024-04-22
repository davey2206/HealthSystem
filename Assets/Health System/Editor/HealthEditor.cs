using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Health))]
public class HealthEditor : Editor
{
    SerializedProperty UseShieldProperty;
    SerializedProperty ShieldProperty;

    SerializedProperty UseArmorProperty;
    SerializedProperty ArmorProperty;
    SerializedProperty DamageReductionProperty;
    SerializedProperty DamageReductionShieldProperty;

    SerializedProperty UseEventProperty;
    SerializedProperty HitEventProperty;
    SerializedProperty HealEventsProperty;
    SerializedProperty DieEventsProperty;

    private void OnEnable()
    {
        ShieldProperty = serializedObject.FindProperty(nameof(Health.MaxShield));
        ArmorProperty = serializedObject.FindProperty(nameof(Health.Armor));
        DamageReductionProperty = serializedObject.FindProperty(nameof(Health.DamageReduction));
        DamageReductionShieldProperty = serializedObject.FindProperty(nameof(Health.DamageReductionShield));
        HitEventProperty = serializedObject.FindProperty(nameof(Health.HitEvents));
        HealEventsProperty = serializedObject.FindProperty(nameof(Health.HealEvents));
        DieEventsProperty = serializedObject.FindProperty(nameof(Health.DieEvents));

        UseShieldProperty = serializedObject.FindProperty(nameof(Health.UseShield));
        UseArmorProperty = serializedObject.FindProperty(nameof(Health.UseArmor));
        UseEventProperty = serializedObject.FindProperty(nameof(Health.UseEvents));
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.Update();

        if (UseShieldProperty.boolValue)
        {
            EditorGUILayout.LabelField("Shields", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(ShieldProperty);
        }

        if (UseArmorProperty.boolValue && !UseShieldProperty.boolValue)
        {
            EditorGUILayout.LabelField("Armor", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(ArmorProperty);
            EditorGUILayout.PropertyField(DamageReductionProperty);
        }
        else if (UseArmorProperty.boolValue && UseShieldProperty.boolValue)
        {
            EditorGUILayout.LabelField("Armor", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(ArmorProperty);
            EditorGUILayout.PropertyField(DamageReductionProperty);
            EditorGUILayout.PropertyField(DamageReductionShieldProperty);
        }

        if (UseEventProperty.boolValue)
        {
            EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(HitEventProperty);
            EditorGUILayout.PropertyField(HealEventsProperty);
            EditorGUILayout.PropertyField(DieEventsProperty);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
