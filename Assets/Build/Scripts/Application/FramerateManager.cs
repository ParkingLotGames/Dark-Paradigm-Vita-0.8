using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Allows for setting the framerate to 30, 60, or unlimited.
    /// </summary>
    public class FramerateManager : MonoBehaviour
    {
        /// <summary>
        /// Sets the framerate to unlimited (no vSync).
        /// </summary>
        public void SetUnlimitedFramerate()
        {
            QualitySettings.vSyncCount = 0;
        }

        /// <summary>
        /// Sets the framerate to 30 frames per second (vSync every 2 frames).
        /// </summary>
        public void Set30Framerate()
        {
            QualitySettings.vSyncCount = 2;
        }

        /// <summary>
        /// Sets the framerate to 60 frames per second (vSync every frame).
        /// </summary>
        public void Set60Framerate()
        {
            QualitySettings.vSyncCount = 1;
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(FramerateManager))]
	[CanEditMultipleObjects]

    public class CustomFramerateManagerInspector : Editor
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
        FramerateManager FramerateManager = (FramerateManager)target;
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