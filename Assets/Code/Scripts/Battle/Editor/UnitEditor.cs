namespace Nivandria.Battle.Editor
{
    using UnityEngine;
    using Nivandria.Battle.UnitSystem;

#if UNITY_EDITOR
    using UnityEditor;

    [CustomEditor(typeof(Unit))]
    public class UnitEditor : Editor
    {
        SerializedProperty characterName;
        SerializedProperty unitType;
        SerializedProperty baseHealth, currentHealth;
        SerializedProperty basePhysicalAttack, currentPhysicalAttack;
        SerializedProperty baseMagicalAttack, currentMagicalAttack;
        SerializedProperty basePhysicalDefense, currentPhysicalDefense;
        SerializedProperty baseMagicalDefense, currentMagicalDefense;
        SerializedProperty baseAgility, currentAgility;
        SerializedProperty hasCompletedTurn;
        SerializedProperty isSelected;
        SerializedProperty skinnedMeshRenderer;
        SerializedProperty moveType;
        SerializedProperty currentDirection;
        SerializedProperty hasMoved;
        SerializedProperty hasUseSkill;
        SerializedProperty baseEvasion;
        SerializedProperty currentEvasion;
        SerializedProperty baseAttackAccuracy;
        SerializedProperty currentAttackAccuracy;
        SerializedProperty unitIcon;
        SerializedProperty isEnemy;
        SerializedProperty isAlive;

        private bool showHealth = false;
        private bool showAttack = false;
        private bool showPhysicalAttack = true;
        private bool showMagicalAttack = true;
        private bool showDefense = false;
        private bool showPhysicalDefense = true;
        private bool showMagicalDefense = true;
        private bool showAgility = false;
        private bool showEvasion = false;
        private bool showAccuracy = false;
        private bool showStatus = true;

        private void OnEnable()
        {
            characterName = serializedObject.FindProperty("characterName");
            unitType = serializedObject.FindProperty("unitType");
            baseHealth = serializedObject.FindProperty("baseHealth");
            currentHealth = serializedObject.FindProperty("currentHealth");
            basePhysicalAttack = serializedObject.FindProperty("basePhysicalAttack");
            currentPhysicalAttack = serializedObject.FindProperty("currentPhysicalAttack");
            baseMagicalAttack = serializedObject.FindProperty("baseMagicalAttack");
            currentMagicalAttack = serializedObject.FindProperty("currentMagicalAttack");
            basePhysicalDefense = serializedObject.FindProperty("basePhysicalDefense");
            currentPhysicalDefense = serializedObject.FindProperty("currentPhysicalDefense");
            baseMagicalDefense = serializedObject.FindProperty("baseMagicalDefense");
            currentMagicalDefense = serializedObject.FindProperty("currentMagicalDefense");
            baseAgility = serializedObject.FindProperty("baseAgility");
            currentAgility = serializedObject.FindProperty("currentAgility");
            hasCompletedTurn = serializedObject.FindProperty("hasCompletedTurn");
            isSelected = serializedObject.FindProperty("isSelected");
            hasMoved = serializedObject.FindProperty("hasMoved");
            hasUseSkill = serializedObject.FindProperty("hasUseSkill");
            skinnedMeshRenderer = serializedObject.FindProperty("skinnedMeshRenderer");
            moveType = serializedObject.FindProperty("moveType");
            currentDirection = serializedObject.FindProperty("currentDirection");
            baseEvasion = serializedObject.FindProperty("baseEvasion");
            currentEvasion = serializedObject.FindProperty("currentEvasion");
            baseAttackAccuracy = serializedObject.FindProperty("baseAttackAccuracy");
            currentAttackAccuracy = serializedObject.FindProperty("currentAttackAccuracy");
            unitIcon = serializedObject.FindProperty("unitIcon");
            isEnemy = serializedObject.FindProperty("isEnemy");
            isAlive = serializedObject.FindProperty("isAlive");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(skinnedMeshRenderer);
            EditorGUILayout.PropertyField(unitIcon);

            GUILayout.Space(5);

            EditorGUILayout.PropertyField(characterName);
            EditorGUILayout.PropertyField(unitType);
            EditorGUILayout.PropertyField(moveType);

            showStatus = EditorGUILayout.Foldout(showStatus, "Status");
            if (showStatus)
            {
                EditorGUILayout.PropertyField(hasCompletedTurn);
                EditorGUILayout.PropertyField(isEnemy);
                EditorGUILayout.PropertyField(isAlive);

                FacingDirection facingDirection = (FacingDirection)currentDirection.enumValueIndex;
                EditorGUILayout.LabelField("Is Selected", isSelected.boolValue.ToString());
                EditorGUILayout.LabelField("Has Moved", hasMoved.boolValue.ToString());
                EditorGUILayout.LabelField("Has Use Skill", hasUseSkill.boolValue.ToString());
                EditorGUILayout.LabelField("Direction ", facingDirection.ToString());
                GUILayout.Space(5);
            }

            #region Health, Attack & Defense Variables
            showHealth = EditorGUILayout.Foldout(showHealth, "Health");
            if (showHealth)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label("Base", EditorStyles.boldLabel, GUILayout.Width(40));
                EditorGUILayout.PropertyField(baseHealth, GUIContent.none, GUILayout.Width(60));

                GUILayout.Space(20);

                GUILayout.Label("Current", EditorStyles.boldLabel, GUILayout.Width(60));
                EditorGUILayout.PropertyField(currentHealth, GUIContent.none, GUILayout.Width(60));

                GUILayout.EndHorizontal();
                GUILayout.Space(5);
            }

            showAttack = EditorGUILayout.Foldout(showAttack, "Attack");
            if (showAttack)
            {
                EditorGUI.indentLevel++;
                showPhysicalAttack = EditorGUILayout.Foldout(showPhysicalAttack, "Physical Attack");
                if (showPhysicalAttack)
                {
                    GUILayout.BeginHorizontal();

                    GUILayout.Space(15);
                    GUILayout.Label("Base", EditorStyles.boldLabel, GUILayout.Width(40));
                    EditorGUILayout.PropertyField(basePhysicalAttack, GUIContent.none, GUILayout.Width(60));

                    GUILayout.Space(20);

                    GUILayout.Label("Current", EditorStyles.boldLabel, GUILayout.Width(60));
                    EditorGUILayout.PropertyField(currentPhysicalAttack, GUIContent.none, GUILayout.Width(60));

                    GUILayout.EndHorizontal();
                }

                showMagicalAttack = EditorGUILayout.Foldout(showMagicalAttack, "Magical Attack");
                if (showMagicalAttack)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(15);

                    GUILayout.Label("Base", EditorStyles.boldLabel, GUILayout.Width(40));
                    EditorGUILayout.PropertyField(baseMagicalAttack, GUIContent.none, GUILayout.Width(60));

                    GUILayout.Space(20);

                    GUILayout.Label("Current", EditorStyles.boldLabel, GUILayout.Width(60));
                    EditorGUILayout.PropertyField(currentMagicalAttack, GUIContent.none, GUILayout.Width(60));

                    GUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel--;
                GUILayout.Space(5);
            }

            showDefense = EditorGUILayout.Foldout(showDefense, "Defense");
            if (showDefense)
            {
                EditorGUI.indentLevel++;
                showPhysicalDefense = EditorGUILayout.Foldout(showPhysicalDefense, "Physical Defense");
                if (showPhysicalDefense)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(15);

                    GUILayout.Label("Base", EditorStyles.boldLabel, GUILayout.Width(40));
                    EditorGUILayout.PropertyField(basePhysicalDefense, GUIContent.none, GUILayout.Width(60));

                    GUILayout.Space(20);

                    GUILayout.Label("Current", EditorStyles.boldLabel, GUILayout.Width(60));
                    EditorGUILayout.PropertyField(currentPhysicalDefense, GUIContent.none, GUILayout.Width(60));

                    GUILayout.EndHorizontal();
                }


                showMagicalDefense = EditorGUILayout.Foldout(showMagicalDefense, "Magical Defense");
                if (showMagicalDefense)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(15);

                    GUILayout.Label("Base", EditorStyles.boldLabel, GUILayout.Width(40));
                    EditorGUILayout.PropertyField(baseMagicalDefense, GUIContent.none, GUILayout.Width(60));

                    GUILayout.Space(20);

                    GUILayout.Label("Current", EditorStyles.boldLabel, GUILayout.Width(60));
                    EditorGUILayout.PropertyField(currentMagicalDefense, GUIContent.none, GUILayout.Width(60));

                    GUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel--;
                GUILayout.Space(5);
            }

            showAgility = EditorGUILayout.Foldout(showAgility, "Agility");
            if (showAgility)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label("Base", EditorStyles.boldLabel, GUILayout.Width(40));
                EditorGUILayout.PropertyField(baseAgility, GUIContent.none, GUILayout.Width(60));

                GUILayout.Space(20);

                GUILayout.Label("Current", EditorStyles.boldLabel, GUILayout.Width(60));
                EditorGUILayout.PropertyField(currentAgility, GUIContent.none, GUILayout.Width(60));

                GUILayout.EndHorizontal();
                GUILayout.Space(5);
            }

            showEvasion = EditorGUILayout.Foldout(showEvasion, "Evasion");
            if (showEvasion)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label("Base", EditorStyles.boldLabel, GUILayout.Width(40));
                EditorGUILayout.PropertyField(baseEvasion, GUIContent.none, GUILayout.Width(60));

                GUILayout.Space(20);

                GUILayout.Label("Current", EditorStyles.boldLabel, GUILayout.Width(60));
                EditorGUILayout.PropertyField(currentEvasion, GUIContent.none, GUILayout.Width(60));

                GUILayout.EndHorizontal();
                GUILayout.Space(5);
            }

            showAccuracy = EditorGUILayout.Foldout(showAccuracy, "Accuracy");
            if (showAccuracy)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label("Base", EditorStyles.boldLabel, GUILayout.Width(40));
                EditorGUILayout.PropertyField(baseAttackAccuracy, GUIContent.none, GUILayout.Width(60));

                GUILayout.Space(20);

                GUILayout.Label("Current", EditorStyles.boldLabel, GUILayout.Width(60));
                EditorGUILayout.PropertyField(currentAttackAccuracy, GUIContent.none, GUILayout.Width(60));

                GUILayout.EndHorizontal();
                GUILayout.Space(5);
            }
            #endregion

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif

}