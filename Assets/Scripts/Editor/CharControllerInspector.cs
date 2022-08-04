using UnityEditor;

[CustomEditor(typeof(CharController))]
public class CharControllerInspector : Editor
{
    SerializedProperty currentState;
    SerializedProperty currentVelocity;
    SerializedProperty interuptible,
    isGrounded,
    canDash,
    canAttack;
    SerializedProperty groundSpeed,
    dashSpeed,
    jumpVelocity,
    lowJumpMultiplier,
    fallGravityMultiplier,
    aerialDrift;
    SerializedProperty dashFrameLength,
    dashStartup,
    jumpSquatFrames,
    nNeutralGFrames,
    nSideGFrames,
    nUpGFrames,
    nDownGFrames,
    nNeutralAFrames,
    nSideAFrames,
    nUpAFrames,
    nDownAFrames;
    SerializedProperty jumpInput,
    moveInput;

    SerializedProperty glowLight;

    SerializedProperty colliders, playerCollider, charBlockerCollider;

    static bool showCharInfo, showMovementInfo, showNormals, showSpecials, showLighting, showColliders = false;

    private void OnEnable()
    {
        currentState = serializedObject.FindProperty("stateSerializationHelper");
        currentVelocity = serializedObject.FindProperty("velocitySerializationHelper");
        interuptible = serializedObject.FindProperty("interuptible");
        isGrounded = serializedObject.FindProperty("isGrounded");
        canDash = serializedObject.FindProperty("canDash");
        canAttack = serializedObject.FindProperty("canAttack");
        jumpInput = serializedObject.FindProperty("jumpInput");
        moveInput = serializedObject.FindProperty("moveInput");
        groundSpeed = serializedObject.FindProperty("groundSpeed");
        dashSpeed = serializedObject.FindProperty("dashSpeed");
        jumpVelocity = serializedObject.FindProperty("jumpVelocity");
        lowJumpMultiplier = serializedObject.FindProperty("lowJumpMultiplier");
        fallGravityMultiplier = serializedObject.FindProperty("fallGravityMultiplier");
        aerialDrift = serializedObject.FindProperty("aerialDrift");
        dashFrameLength = serializedObject.FindProperty("dashFrameLength");
        dashStartup = serializedObject.FindProperty("dashStartup");
        jumpSquatFrames = serializedObject.FindProperty("jumpSquatFrames");
        nNeutralGFrames = serializedObject.FindProperty("nNeutralGFrames");
        nSideGFrames = serializedObject.FindProperty("nSideGFrames");
        nUpGFrames = serializedObject.FindProperty("nUpGFrames");
        nDownGFrames = serializedObject.FindProperty("nDownGFrames");
        nNeutralAFrames = serializedObject.FindProperty("nNeutralAFrames");
        nSideAFrames = serializedObject.FindProperty("nSideAFrames");
        nUpAFrames = serializedObject.FindProperty("nUpAFrames");
        nDownAFrames = serializedObject.FindProperty("nDownAFrames");
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
            EditorGUILayout.PropertyField(moveInput);
            EditorGUILayout.PropertyField(jumpInput);
            EditorGUILayout.PropertyField(canAttack);
            EditorGUILayout.PropertyField(canDash);
            EditorGUILayout.PropertyField(isGrounded);
            EditorGUILayout.PropertyField(interuptible);
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        showMovementInfo = EditorGUILayout.BeginFoldoutHeaderGroup(showMovementInfo, "Movement Info/Frames");
        if (showMovementInfo)
        {
            EditorGUILayout.PropertyField(dashSpeed);
            EditorGUILayout.PropertyField(dashFrameLength);
            EditorGUILayout.PropertyField(dashStartup);
            EditorGUILayout.PropertyField(jumpVelocity);
            EditorGUILayout.PropertyField(lowJumpMultiplier);
            EditorGUILayout.PropertyField(fallGravityMultiplier);
            EditorGUILayout.PropertyField(jumpSquatFrames);
            EditorGUILayout.PropertyField(groundSpeed);
            EditorGUILayout.PropertyField(aerialDrift);
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        showNormals = EditorGUILayout.BeginFoldoutHeaderGroup(showNormals, "Normal Attacks Frames");
        if (showNormals)
        {
            EditorGUILayout.PropertyField(nNeutralGFrames);
            EditorGUILayout.PropertyField(nSideGFrames);
            EditorGUILayout.PropertyField(nUpGFrames);
            EditorGUILayout.PropertyField(nDownGFrames);
            EditorGUILayout.PropertyField(nNeutralAFrames);
            EditorGUILayout.PropertyField(nSideAFrames);
            EditorGUILayout.PropertyField(nUpAFrames);
            EditorGUILayout.PropertyField(nDownAFrames);
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
