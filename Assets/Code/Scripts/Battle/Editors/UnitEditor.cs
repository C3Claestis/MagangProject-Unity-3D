namespace Nivandria.Battle.Editor
{
    using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    [CustomEditor(typeof(Unit))]
    public class UnitEditor : Editor
    {
        SerializedProperty characterName;
        SerializedProperty baseHealth, currentHealth;
        SerializedProperty basePhysicalAttack, currentPhysicalAttack;
        SerializedProperty baseMagicalAttack, currentMagicalAttack;
        SerializedProperty basePhysicalDefense, currentPhysicalDefense;
        SerializedProperty baseMagicalDefense, currentMagicalDefense;
        SerializedProperty baseAgility, currentAgility;
        SerializedProperty hasMove;
        SerializedProperty isSelected;
        SerializedProperty skinnedMeshRenderer;

        private bool showHealth = false;
        private bool showAttack = false;
        private bool showPhysicalAttack = false;
        private bool showMagicalAttack = false;
        private bool showDefense = false;
        private bool showPhysicalDefense = false;
        private bool showMagicalDefense = false;
        private bool showAgility = false;
        private bool showStatus = false;

        private void OnEnable()
        {
            characterName = serializedObject.FindProperty("characterName");

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

            hasMove = serializedObject.FindProperty("hasMove");
            isSelected = serializedObject.FindProperty("isSelected");

            skinnedMeshRenderer = serializedObject.FindProperty("skinnedMeshRenderer");

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(characterName);

            #region 
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
            #endregion

            showStatus = EditorGUILayout.Foldout(showStatus, "Status");
            if (showStatus)
            {
                EditorGUILayout.PropertyField(hasMove);
                EditorGUILayout.PropertyField(isSelected);
                GUILayout.Space(5);
            }

            EditorGUILayout.PropertyField(skinnedMeshRenderer);

            serializedObject.ApplyModifiedProperties();
        }
    }
}