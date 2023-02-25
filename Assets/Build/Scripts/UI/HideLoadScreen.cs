using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// This class provides functionality to hide the loading screen by deactivating the load screen sprite and load progress container.
    /// </summary>
    public class HideLoadScreen : MonoBehaviour
    {
        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            // If the LoadScreen instance exists
            if (LoadScreen.Instance)
            {
                // Deactivate the load screen sprite and the load progress container
                LoadScreen.Instance.loadScreenSprite.gameObject.SetActive(false);
                LoadScreen.Instance.loadProggressContainer.SetActive(false);
            }
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(HideLoadScreen))]
	[CanEditMultipleObjects]

    public class CustomHideLoadScreenInspector : Editor
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
        HideLoadScreen HideLoadScreen = (HideLoadScreen)target;
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