using UnityEngine;
using DP.DevTools;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Handles setting the selected GameObject for the EventSystem used for input events.
    /// </summary>
    public class InputEventSystemBehavior : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// Reference to the EventSystem component.
        /// </summary>
        EventSystem eventSystem;

        /// <summary>
        /// The GameObject to select when the game over screen is shown.
        /// </summary>
        [SerializeField] GameObject firstSelectedOnGameOverScreen;

        /// <summary>
        /// The GameObject to select when the pause screen is shown.
        /// </summary>
        [SerializeField] GameObject firstSelectedOnPauseScreen;
        #endregion

        #region Methods
        private void Awake()
        {
            eventSystem = GetComponent<EventSystem>();
        }

        private void Start()
        {
            eventSystem.SetSelectedGameObject(firstSelectedOnGameOverScreen);
        }

        /// <summary>
        /// Sets the currently selected GameObject to null.
        /// </summary>
        public void SetNullSelected()
        {
            eventSystem.SetSelectedGameObject(null);
        }

        /// <summary>
        /// Sets the currently selected GameObject to the one for the game over screen.
        /// </summary>
        public void SetGameOverSelected()
        {
            eventSystem.SetSelectedGameObject(firstSelectedOnGameOverScreen);
        }

        /// <summary>
        /// Sets the currently selected GameObject to the one for the pause screen.
        /// </summary>
        public void SetPauseSelected()
        {
            eventSystem.SetSelectedGameObject(firstSelectedOnPauseScreen);
        }
        #endregion
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(InputEventSystemBehavior))]
	//[CanEditMultipleObjects]

    public class CustomInputEventSystemBehaviorInspector : Editor
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
        InputEventSystemBehavior InputEventSystemBehavior = (InputEventSystemBehavior)target;
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