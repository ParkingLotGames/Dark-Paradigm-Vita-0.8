using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// This class is responsible for fading in the scene on start. It is attached to a game object in the scene and
    /// is automatically executed when the scene is loaded.
    /// </summary>
    public class FadeOnStart : MonoBehaviour
    {
        /// <summary>
        /// This function is called when the FadeOnStart script is started. It fades in the scene by calling the StaticShowContents()
        /// function from the FadeToBlack class.
        /// </summary>
        void Start()
        {
            // Call the StaticShowContents() function from the FadeToBlack class to fade in the scene.
            FadeToBlack.StaticShowContents();
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(FadeOnStart))]
    [CanEditMultipleObjects]

    public class CustomFadeOnStartInspector : Editor
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
            FadeOnStart FadeOnStart = (FadeOnStart)target;
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