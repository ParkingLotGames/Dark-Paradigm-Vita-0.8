using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.PSVita
{
    /// <summary>
    /// KyuHENScreen class is responsible for controlling the behavior of the KyuHENScreen game object.
    /// </summary>
    public class KyuHENScreen : MonoBehaviour
    {
        #region Methods

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
#if !UNITY_PSP2
            // If the application is not running on PlayStation Vita, destroy the game object to prevent it from being instantiated.
            Destroy(gameObject);
#endif
        }

        #endregion
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(KyuHENScreen))]
	[CanEditMultipleObjects]

    public class CustomKyuHENScreenInspector : Editor
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
        KyuHENScreen KyuHENScreen = (KyuHENScreen)target;
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