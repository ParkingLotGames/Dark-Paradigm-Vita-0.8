using UnityEngine;
using DP.DevTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// This class handles the Vignette image effect.
    /// </summary>
    public class Vignette : MonoBehaviour
    {
        /// <summary>
        /// The singleton instance of the Vignette class.
        /// </summary>
        public static Vignette Instance;

        #region Variables
        /// <summary>
        /// The Image component for the Vignette effect.
        /// </summary>
        public Image img;

        #region Private Variables

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            Singleton();
            img = GetComponent<Image>();
        }

        /// <summary>
        /// Ensures that there is only one instance of the Vignette class.
        /// </summary>
        void Singleton()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(Vignette))]
	[CanEditMultipleObjects]

    public class CustomVignetteInspector : Editor
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
        Vignette Vignette = (Vignette)target;
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