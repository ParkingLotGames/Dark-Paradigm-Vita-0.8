using UnityEngine;
using DP.DevTools;
using DP.StaticDataContainers;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Listens for a specific input and invokes an event when the input is triggered.
    /// </summary>
    public class ListenForCircle : MonoBehaviour
    {
        float t;
        bool triggered;

        /// <summary>
        /// The event that is triggered when the input is detected.
        /// </summary>
        [SerializeField] public UnityEvent OnCirclePressed;

        /// <summary>
        /// Resets the timer when the component is enabled.
        /// </summary>
        private void OnEnable()
        {
            t = 0;
        }

        /// <summary>
        /// Checks for the input and triggers the event if the input is detected and the trigger has not already been set.
        /// </summary>
        void Update()
        {
            t += Time.deltaTime;
            if (Input.GetButtonDown(PSVitaInputValues.Circle) && !triggered && t >= .15f)
            {
                triggered = true;
                Invoke("ResetTrigger", .135f);
                OnCirclePressed.Invoke();
            }
        }

        /// <summary>
        /// Resets the trigger state after a delay.
        /// </summary>
        private void ResetTrigger()
        {
            triggered = false;
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(ListenForCircle))]
	[CanEditMultipleObjects]

    public class CustomListenForCircleInspector : Editor
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
        ListenForCircle ListenForCircle = (ListenForCircle)target;
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