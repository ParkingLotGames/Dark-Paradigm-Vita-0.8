using UnityEngine;
using DP.DevTools;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Class responsible for the tooltip that appears on menus
    /// </summary>
    public class MenuTooltip : MonoBehaviour
    {
        /// <summary>
        /// The instance of the MenuTooltip
        /// </summary>
        public static MenuTooltip Instance;

        /// <summary>
        /// The Text component of the tooltip
        /// </summary>
        public Text text;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            // Get the Text component of the tooltip
            text = GetComponent<Text>();

            // Ensures that there is only one instance of the MenuTooltip object
            Singleton();
        }

        /// <summary>
        /// Ensures that there is only one instance of the MenuTooltip object
        /// </summary>
        void Singleton()
        {
            // If there is no instance of the MenuTooltip, set this object as the instance and make it persistent
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            // If there is already an instance of the MenuTooltip, destroy this object
            else
            {
                Destroy(gameObject);
            }
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(MenuTooltip))]
	[CanEditMultipleObjects]

    public class CustomMenuTooltipInspector : Editor
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
        MenuTooltip MenuTooltip = (MenuTooltip)target;
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