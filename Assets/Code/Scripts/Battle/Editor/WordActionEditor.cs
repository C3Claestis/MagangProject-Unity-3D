namespace Nivandria.Battle.Editor
{
#if UNITY_EDITOR
    using Nivandria.Battle.UI;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(WordActionUI))]
    public class WordActionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            WordActionUI wordActionUI = (WordActionUI)target;

            base.OnInspectorGUI();

            if (GUILayout.Button("Generate New Random Buttons"))
            {
                try { wordActionUI.NewButtons(); }
                catch { Debug.Log("Game Haven't Started yet!"); }
            }
        }
    }
#endif

}