using UnityEngine;
using DP.DevTools;
using UnityEngine.Events;
using DP.StaticDataContainers;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// A behavior for detecting when the back button is pressed and invoking an event in response.
    /// </summary>
    public class BackButtonBehavior : MonoBehaviour
    {
        /// <summary>
        /// The event to invoke when the back button is pressed.
        /// </summary>
        [SerializeField] UnityEvent OnBackButtonPress;

        /// <summary>
        /// Whether the back button has already been triggered to prevent multiple invocations.
        /// </summary>
        bool triggered;

        /// <summary>
        /// Update is called once per frame and checks for back button input to invoke the OnBackButtonPress event.
        /// </summary>
        private void Update()
        {
            if (!triggered)
            {
                if (Input.GetButtonDown(PSVitaInputValues.Circle))
                    OnBackButtonPress.Invoke();

                triggered = true;
            }
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(BackButtonBehavior))]
	[CanEditMultipleObjects]

    public class CustomBackButtonBehaviorInspector : Editor
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
        BackButtonBehavior BackButtonBehavior = (BackButtonBehavior)target;
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