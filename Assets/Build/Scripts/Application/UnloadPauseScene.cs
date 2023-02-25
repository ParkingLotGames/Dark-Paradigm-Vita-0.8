﻿using UnityEngine;
using DP.DevTools;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// A MonoBehaviour script responsible for unloading the Pause scene.
    /// </summary>
    public class UnloadPauseScene : MonoBehaviour
    {
        // The name of the scene to unload
        private string pauseScene = "Pause Menu";

        /// <summary>
        /// Unloads the pause scene asynchronously.
        /// </summary>
        public void UnloadScene()
        {
            SceneManager.UnloadSceneAsync(pauseScene);
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(UnloadPauseScene))]
	[CanEditMultipleObjects]

    public class CustomUnloadPauseSceneInspector : Editor
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
        UnloadPauseScene UnloadPauseScene = (UnloadPauseScene)target;
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