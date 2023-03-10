using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// This class is responsible for loading scenes by name using the LoadScreen class.
    /// </summary>
    public class LoadScreenCaller : MonoBehaviour
    {
        /// <summary>
        /// Loads a scene based on its build index.
        /// </summary>
        /// <param name="index">The build index of the scene to be loaded.</param>
        public void LoadSceneIndex(int index)
        {
            LoadScreen.Instance.LoadSceneByIndex(index);
        }

        /// <summary>
        /// Loads a scene based on its name.
        /// </summary>
        /// <param name="name">The name of the scene to be loaded.</param>
        public void LoadSceneName(string name)
        {
            LoadScreen.Instance.LoadSceneByName(name);
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(LoadScreenCaller))]
	[CanEditMultipleObjects]

    public class CustomLoadScreenCallerInspector : Editor
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
        LoadScreenCaller LoadScreenCaller = (LoadScreenCaller)target;
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