using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HealthBar))]
public class HealthBarEditor : Editor
{
    SerializedProperty UseSpriteBasedProperty;
    SerializedProperty UseBarSmoothingProperty;

    SerializedProperty GridProperty;
    SerializedProperty HeartImageProperty;
    SerializedProperty HeartProperty;
    SerializedProperty EmptyHeartProperty;

    SerializedProperty FillSliderProperty;
    SerializedProperty BarSpeedProperty;

    bool showOptions = false;

    private void OnEnable()
    {
        UseSpriteBasedProperty = serializedObject.FindProperty(nameof(HealthBar.UseSpriteBased));
        UseBarSmoothingProperty = serializedObject.FindProperty(nameof(HealthBar.UseBarSmoothing));

        GridProperty = serializedObject.FindProperty(nameof(HealthBar.Grid));
        HeartImageProperty = serializedObject.FindProperty(nameof(HealthBar.HeartImage));
        HeartProperty = serializedObject.FindProperty(nameof(HealthBar.Heart));
        EmptyHeartProperty = serializedObject.FindProperty(nameof(HealthBar.EmptyHeart));

        FillSliderProperty = serializedObject.FindProperty(nameof(HealthBar.FillSlider));
        BarSpeedProperty = serializedObject.FindProperty(nameof(HealthBar.BarSpeed));
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.Update();

        //Options
        showOptions = EditorGUILayout.BeginFoldoutHeaderGroup(showOptions, "Options");

        if (showOptions && !UseSpriteBasedProperty.boolValue)
        {
            EditorGUILayout.PropertyField(UseSpriteBasedProperty);
            EditorGUILayout.PropertyField(UseBarSmoothingProperty);
        }
        else if(showOptions)
        {
            EditorGUILayout.PropertyField(UseSpriteBasedProperty);
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space();

        if (UseSpriteBasedProperty.boolValue)
        {
            EditorGUILayout.PropertyField(GridProperty);
            EditorGUILayout.PropertyField(HeartImageProperty);
            EditorGUILayout.PropertyField(HeartProperty);
            EditorGUILayout.PropertyField(EmptyHeartProperty);
        }

        if (!UseSpriteBasedProperty.boolValue && !UseBarSmoothingProperty.boolValue)
        {
            EditorGUILayout.PropertyField(FillSliderProperty);
        }

        if (!UseSpriteBasedProperty.boolValue && UseBarSmoothingProperty.boolValue)
        {
            EditorGUILayout.PropertyField(FillSliderProperty);
            EditorGUILayout.PropertyField(BarSpeedProperty);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
