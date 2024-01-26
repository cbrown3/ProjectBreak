using UnityEditor;

[CustomEditor(typeof(FightLogic.CharController))]
public class CharControllerInspector : Editor
{
    SerializedProperty currentState;
    SerializedProperty currentVelocity;
    SerializedProperty playerData;
    SerializedProperty interuptible,
    canDash,
    canAttack,
    isDashing;
    SerializedProperty groundSpeed,
    dashSpeed;
    SerializedProperty dashFrameLength,
    dashStartup,
    pushbackFrameLength,
    nNeutralGFrames,
    nSideGFrames,
    nUpGFrames,
    nDownGFrames;
    SerializedProperty moveInput;

    SerializedProperty glowLight;

    SerializedProperty colliders, playerCollider, charBlockerCollider;

    static bool showCharInfo, showMovementInfo, showNormals, showSpecials, showLighting, showColliders = false;

    private void OnEnable()
    {
        currentState = serializedObject.FindProperty("stateSerializationHelper");
        currentVelocity = serializedObject.FindProperty("velocitySerializationHelper");
        playerData = serializedObject.FindProperty("playerData");
        interuptible = serializedObject.FindProperty("interuptible");
        canDash = serializedObject.FindProperty("canDash");
        canAttack = serializedObject.FindProperty("canAttack");
        isDashing = serializedObject.FindProperty("isDashing");
        moveInput = serializedObject.FindProperty("moveInput");
        groundSpeed = serializedObject.FindProperty("groundSpeed");
        dashSpeed = serializedObject.FindProperty("dashSpeed");
        dashFrameLength = serializedObject.FindProperty("dashFrameLength");
        dashStartup = serializedObject.FindProperty("dashStartup");
        pushbackFrameLength = serializedObject.FindProperty("pushbackFrameLength");
        nNeutralGFrames = serializedObject.FindProperty("nNeutralGFrames");
        nSideGFrames = serializedObject.FindProperty("nSideGFrames");
        nUpGFrames = serializedObject.FindProperty("nUpGFrames");
        nDownGFrames = serializedObject.FindProperty("nDownGFrames");
        glowLight = serializedObject.FindProperty("glowLight");
        colliders = serializedObject.FindProperty("colliders");
        playerCollider = serializedObject.FindProperty("playerCollider");
        charBlockerCollider = serializedObject.FindProperty("charBlockerCollider");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        showCharInfo = EditorGUILayout.BeginFoldoutHeaderGroup(showCharInfo, "Character Info");
        if (showCharInfo)
        {
            EditorGUILayout.PropertyField(currentState);
            EditorGUILayout.PropertyField(currentVelocity);
            EditorGUILayout.PropertyField(playerData);
            EditorGUILayout.PropertyField(moveInput);
            EditorGUILayout.PropertyField(canAttack);
            EditorGUILayout.PropertyField(isDashing);
            EditorGUILayout.PropertyField(canDash);
            EditorGUILayout.PropertyField(interuptible);
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        showMovementInfo = EditorGUILayout.BeginFoldoutHeaderGroup(showMovementInfo, "Movement Info/Frames");
        if (showMovementInfo)
        {
            EditorGUILayout.PropertyField(dashSpeed);
            EditorGUILayout.PropertyField(dashFrameLength);
            EditorGUILayout.PropertyField(pushbackFrameLength);
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

        showLighting = EditorGUILayout.BeginFoldoutHeaderGroup(showLighting, "Lighting Effects");
        if (showLighting)
        {
            EditorGUILayout.PropertyField(glowLight);
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        showColliders = EditorGUILayout.BeginFoldoutHeaderGroup(showColliders, "Colliders");
        if (showColliders)
        {
            EditorGUILayout.PropertyField(colliders);
            EditorGUILayout.PropertyField(playerCollider);
            EditorGUILayout.PropertyField(charBlockerCollider);
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        serializedObject.ApplyModifiedProperties();

    }
}
