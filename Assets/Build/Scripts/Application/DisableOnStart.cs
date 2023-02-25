using UnityEngine;
using DP.DevTools;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// A script to disable the GameObject it is attached to if the current scene is the first scene, and enable a vignette image in other scenes.
    /// </summary>
    public class DisableOnStart : MonoBehaviour
    {
        /// <summary>
        /// This function is called before the first frame update.
        /// </summary>
        void Start()
        {
            // Check if the current scene is the first scene.
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                // Disable the Vignette image and the GameObject this script is attached to.
                Vignette.Instance.img.enabled = false;
                gameObject.SetActive(false);
            }
            else
            {
                // Enable the Vignette image and destroy this script.
                Vignette.Instance.img.enabled = true;
                Destroy(this);
            }
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(DisableOnStart))]
	[CanEditMultipleObjects]

    public class CustomDisableOnStartInspector : Editor
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
        DisableOnStart DisableOnStart = (DisableOnStart)target;
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