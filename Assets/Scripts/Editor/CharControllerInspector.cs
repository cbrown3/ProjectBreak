using FightLogic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

[CustomEditor(typeof(FightLogic.CharController))]
public class CharControllerInspector : Editor
{
    SerializedProperty currentState;
    SerializedProperty currentVelocity;
    SerializedProperty guardHeight;
    SerializedProperty attackHeight;
    SerializedProperty playerData;
    SerializedProperty interuptible,
    canDash,
    canAttack,
    isDashing,
    canAttackCancel;
    SerializedProperty groundSpeed,
    dashSpeed;
    SerializedProperty dashFrameLength,
    dashStartup,
    pushbackFrameLength,
    throwFrameLength,
    nNeutralGFrames,
    nSideGFrames,
    nUpGFrames,
    nDownGFrames;
    SerializedProperty moveInput;

    SerializedProperty glowLight;

    SerializedProperty colliders, playerCollider;

    static bool showCharInfo, showCharSettings, showMovement, showNormals, showSpecials, showSerialized = false;

    private void OnEnable()
    {
        currentState = serializedObject.FindProperty("StateType");
        currentVelocity = serializedObject.FindProperty("velocitySerializationHelper");
        guardHeight = serializedObject.FindProperty("guardHeight");
        attackHeight = serializedObject.FindProperty("attackHeight");
        playerData = serializedObject.FindProperty("playerData");
        interuptible = serializedObject.FindProperty("interuptible");
        canDash = serializedObject.FindProperty("canDash");
        canAttack = serializedObject.FindProperty("canAttack");
        isDashing = serializedObject.FindProperty("isDashing");
        canAttackCancel = serializedObject.FindProperty("canAttackCancel");
        moveInput = serializedObject.FindProperty("moveInput");
        groundSpeed = serializedObject.FindProperty("groundSpeed");
        dashSpeed = serializedObject.FindProperty("dashSpeed");
        dashFrameLength = serializedObject.FindProperty("dashFrameLength");
        dashStartup = serializedObject.FindProperty("dashStartup");
        pushbackFrameLength = serializedObject.FindProperty("pushbackFrameLength");
        throwFrameLength = serializedObject.FindProperty("throwFrameLength");
        nNeutralGFrames = serializedObject.FindProperty("nNeutralGFrames");
        nSideGFrames = serializedObject.FindProperty("nSideGFrames");
        nUpGFrames = serializedObject.FindProperty("nUpGFrames");
        nDownGFrames = serializedObject.FindProperty("nDownGFrames");
        glowLight = serializedObject.FindProperty("glowLight");
        colliders = serializedObject.FindProperty("colliders");
        playerCollider = serializedObject.FindProperty("playerCollider");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        showCharInfo = EditorGUILayout.BeginFoldoutHeaderGroup(showCharInfo, "Character Info");
        if (showCharInfo)
        {
            EditorGUILayout.LabelField("Current State:", currentState.GetEnumName<StateType>());
            EditorGUILayout.LabelField("Current Velocity:", currentVelocity.vector2Value.ToString());
            EditorGUILayout.LabelField("Move Input:", moveInput.floatValue.ToString());
            EditorGUILayout.LabelField("Can Attack:", canAttack.boolValue.ToString());
            EditorGUILayout.LabelField("Is Dashing:", isDashing.boolValue.ToString());
            EditorGUILayout.LabelField("Can Dash:", canDash.boolValue.ToString());
            EditorGUILayout.LabelField("Interuptible:", interuptible.boolValue.ToString());
            EditorGUILayout.LabelField("Can Attack Cancel:", canAttackCancel.boolValue.ToString());
            EditorGUILayout.LabelField("Current Guard Height:", guardHeight.GetEnumName<CharController.Height>());
            EditorGUILayout.LabelField("Current Attack Height:", attackHeight.GetEnumName<CharController.Height>());
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        showCharSettings = EditorGUILayout.BeginFoldoutHeaderGroup(showCharSettings, "Character Settings");
        if (showCharSettings)
        {
            
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        showMovement = EditorGUILayout.BeginFoldoutHeaderGroup(showMovement, "Movement Frames");
        if (showMovement)
        {
            EditorGUILayout.PropertyField(dashSpeed);
            EditorGUILayout.PropertyField(dashFrameLength);
            EditorGUILayout.PropertyField(pushbackFrameLength);
            EditorGUILayout.PropertyField(throwFrameLength);
            EditorGUILayout.PropertyField(dashStartup);
            EditorGUILayout.PropertyField(groundSpeed);
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        showNormals = EditorGUILayout.BeginFoldoutHeaderGroup(showNormals, "Normal Attacks Frames");
        if (showNormals)
        {
            EditorGUILayout.PropertyField(nNeutralGFrames);
            EditorGUILayout.PropertyField(nSideGFrames);
            EditorGUILayout.PropertyField(nUpGFrames);
            EditorGUILayout.PropertyField(nDownGFrames);
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        showSpecials = EditorGUILayout.BeginFoldoutHeaderGroup(showSpecials, "Special Attacks Frames");
        if (showSpecials)
        {
            EditorGUILayout.LabelField("TODO: Add Specials");
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        /*
        showLighting = EditorGUILayout.BeginFoldoutHeaderGroup(showLighting, "Lighting Effects");
        if (showLighting)
        {
        }

        EditorGUILayout.EndFoldoutHeaderGroup();
        */

        showSerialized = EditorGUILayout.BeginFoldoutHeaderGroup(showSerialized, "Serialized Fields");
        if (showSerialized)
        {
            EditorGUILayout.PropertyField(playerData);
            EditorGUILayout.PropertyField(glowLight);
            EditorGUILayout.PropertyField(colliders);
            EditorGUILayout.PropertyField(playerCollider);
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        serializedObject.ApplyModifiedProperties();

    }
}
