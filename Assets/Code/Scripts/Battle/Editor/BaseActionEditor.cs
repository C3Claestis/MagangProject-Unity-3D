namespace Nivandria.Battle.Editor
{
    using UnityEngine;
    using Nivandria.Battle.Action;

#if UNITY_EDITOR
    using UnityEditor;
    using Nivandria.Battle.Grid;

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



#if UNITY_EDITOR

    [CustomEditor(typeof(LevelGrid))]
    public class LevelGridEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            LevelGrid levelGrid = (LevelGrid)target;

            base.OnInspectorGUI();

            if (GUILayout.Button("CHECK RELATION"))
            {
                levelGrid.unitBase.UpdateUnitDirection();
                levelGrid.uniTarget.UpdateUnitDirection();
                levelGrid.unitBase.UpdateUnitGridPosition();
                levelGrid.uniTarget.UpdateUnitGridPosition();


                int number = LevelGrid.Instance.RelativeFacingChecker(levelGrid.unitBase, levelGrid.uniTarget);

                switch (number)
                {
                    case 100:
                        Debug.Log(levelGrid.uniTarget.GetCharacterName() + " is on the front " + levelGrid.unitBase.GetCharacterName());
                        break;
                    case 150:
                        Debug.Log(levelGrid.uniTarget.GetCharacterName() + " is on the side " + levelGrid.unitBase.GetCharacterName());
                        break;
                    case 200:
                        Debug.Log(levelGrid.uniTarget.GetCharacterName() + " is on the back " + levelGrid.unitBase.GetCharacterName());
                        break;
                }
            }
            GUILayout.Space(20);

            if (GUILayout.Button("Rotate Base NORTH"))
            {
                levelGrid.unitBase.GetUnitTransform().rotation = Quaternion.Euler(0, 0, 0);
            }

            if (GUILayout.Button("Rotate Base EAST"))
            {
                levelGrid.unitBase.GetUnitTransform().rotation = Quaternion.Euler(0, 90, 0);
            }

            if (GUILayout.Button("Rotate Base SOUTH"))
            {
                levelGrid.unitBase.GetUnitTransform().rotation = Quaternion.Euler(0, 180, 0);
            }


            if (GUILayout.Button("Rotate Base WEST"))
            {
                levelGrid.unitBase.GetUnitTransform().rotation = Quaternion.Euler(0, 270, 0);
            }


            GUILayout.Space(20);

            if (GUILayout.Button("Rotate Target NORTH"))
            {
                levelGrid.uniTarget.GetUnitTransform().rotation = Quaternion.Euler(0, 0, 0);
            }


            if (GUILayout.Button("Rotate Target EAST"))
            {
                levelGrid.uniTarget.GetUnitTransform().rotation = Quaternion.Euler(0, 90, 0);
            }

            if (GUILayout.Button("Rotate Target SOUTH"))
            {
                levelGrid.uniTarget.GetUnitTransform().rotation = Quaternion.Euler(0, 180, 0);
            }


            if (GUILayout.Button("Rotate Target WEST"))
            {
                levelGrid.uniTarget.GetUnitTransform().rotation = Quaternion.Euler(0, 270, 0);
            }


        }


    }
#endif
#endif
}


