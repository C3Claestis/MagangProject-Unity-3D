namespace Nivandria.Battle.Editor
{
    using UnityEngine;
    using Nivandria.Battle.Action;

#if UNITY_EDITOR
    using UnityEditor;

    [CustomEditor(typeof(BaseAction), true)]
    public class BaseActionEditor : Editor
    {
        bool actionStatus = false;
        public override void OnInspectorGUI()
        {
            BaseAction baseAction = (BaseAction)target;

            actionStatus = EditorGUILayout.Foldout(actionStatus, "Action Status");
            if (actionStatus)
            {
                string actionDescription = baseAction.GetDescription();
                ActionCategory actionCategory = baseAction.GetActionCategory();
                ActionType actionType = baseAction.GetActionType();

                EditorGUILayout.LabelField("Name", baseAction.GetName());
                if (actionCategory != ActionCategory.Move)
                {
                    EditorGUILayout.LabelField("Action Category", actionCategory.ToString());
                    EditorGUILayout.LabelField("Action Type", actionType.ToString());
                }

                GUILayout.BeginHorizontal();
                GUILayout.Label("Description");
                GUILayout.TextArea(actionDescription, GUILayout.MaxHeight(50));
                GUILayout.EndHorizontal();

                GUILayout.Space(10);
            }

            base.OnInspectorGUI();
        }
    }
#endif
}