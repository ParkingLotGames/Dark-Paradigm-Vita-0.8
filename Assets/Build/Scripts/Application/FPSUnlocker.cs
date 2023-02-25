using UnityEngine;
using DP.DevTools;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.DevTools
{
    /// <summary>
    /// Controls the vSync count based on the active scene's build index.
    /// </summary>
    public class FPSUnlocker : MonoBehaviour
    {
        void Start()
        {
#if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;
#endif

#if !UNITY_EDITOR
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            QualitySettings.vSyncCount = 0;
        }
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            QualitySettings.vSyncCount = 2;
        }
#endif
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(FPSUnlocker))]
    //[CanEditMultipleObjects]

    public class CustomFPSUnlockerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Framerate will be unlocked automatically only inside the Editor", MessageType.Info);
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

            //base.OnInspectorGUI();
            FPSUnlocker FPSUnlocker = (FPSUnlocker)target;
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