using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Class for fading out to black.
    /// </summary>
    public class FadeOut : MonoBehaviour
    {
        /// <summary>
        /// Fades out to black.
        /// </summary>
        public void FadeOutToBlack()
        {
            // Calls the static method StaticShowContents in the FadeToBlack class to fade to black.
            FadeToBlack.StaticShowContents();
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(FadeOut))]
    [CanEditMultipleObjects]

    public class CustomFadeOutInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            #region GUIStyles
            //Define GUIStyles

            #endregion

            #region Layout Widths
            GUILayoutOption width32 = GUILayout.Width(32);
            GUILayoutOption width40 = GUILayout.Width(40);
            GUILayoutOption width48 = GUILayout.Width(48);
            GUILayoutOption width64 = GUILayout.Width(64);
            GUILayoutOption width80 = GUILayout.Width(80);
            GUILayoutOption width96 = GUILayout.Width(96);
            GUILayoutOption width112 = GUILayout.Width(112);
            GUILayoutOption width128 = GUILayout.Width(128);
            GUILayoutOption width144 = GUILayout.Width(144);
            GUILayoutOption width160 = GUILayout.Width(160);
            #endregion

            base.OnInspectorGUI();
            FadeOut FadeOut = (FadeOut)target;
            //SerializedProperty example = serializedObject.FindProperty("Example");
            serializedObject.Update();

            EditorGUILayout.BeginHorizontal();

            EditorGUIUtility.labelWidth = 80;
            //EditorGUILayout.PropertyField(example);

            EditorGUILayout.EndHorizontal();


            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
    #endregion
}