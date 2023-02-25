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
    /// Listens for the start button to be pressed and invokes the OnStartPressed event.
    /// </summary>
    public class ListenForStartKey : MonoBehaviour
    {
        [SerializeField] UnityEvent OnStartPressed;

        bool pressed;

        /// <summary>
        /// Checks if the start button has been pressed and invokes the OnStartPressed event if it has.
        /// </summary>
        void Update()
        {
            if (!pressed)
            {
                if (Input.GetButtonDown(PSVitaInputValues.Start))
                {
                    pressed = true;
                    OnStartPressed.Invoke();
                    Invoke("ResetPressed", .135f);
                }
            }
        }

        /// <summary>
        /// Resets the pressed flag to false.
        /// </summary>
        void ResetPressed() { pressed = false; }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(ListenForStartKey))]
	[CanEditMultipleObjects]

    public class CustomListenForPauseInspector : Editor
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
        ListenForStartKey ListenForPause = (ListenForStartKey)target;
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