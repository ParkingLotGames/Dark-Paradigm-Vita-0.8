/*
#if UNITY_EDITOR

#endif




#if UNITY_ANDROID

#endif

#if UNITY_PS4

#endif

#if UNITY_PSP2

#endif

#if UNITY_STANDALONE || UNITY_EDITOR

#endif

#if UNITY_WINRT

#endif

#if UNITY_XBOXONE

#endif


#if UNITY_STANDALONE

#endif

*/

using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// A script to ensure that the game object it is attached to is not destroyed when a new scene is loaded.
    /// </summary>
    public class DDOL : MonoBehaviour
    {
        /// <summary>
        /// This function is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            // Prevents the game object from being destroyed when a new scene is loaded.
            DontDestroyOnLoad(this);
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(DDOL))]
	[CanEditMultipleObjects]

    public class CustomDDOLInspector : Editor
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
        DDOL DDOL = (DDOL)target;
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