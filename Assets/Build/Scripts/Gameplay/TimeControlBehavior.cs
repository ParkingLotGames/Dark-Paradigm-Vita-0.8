using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.Gameplay
{
    /// <summary>
    /// Manages the behavior of time in the game.
    /// </summary>
    public class TimeControlBehavior : MonoBehaviour
    {
        [SerializeField]
        public float slowTimeSpeed = 0.15f;

        float originalFixedTime;

        private void Awake()
        {
            /// <summary>
            /// Retrieves the original fixed time before any modification.
            /// </summary>
            originalFixedTime = Time.fixedDeltaTime;
        }

        /// <summary>
        /// Slows down the time by setting the time scale and fixed delta time to a fraction of the original.
        /// </summary>
        public void SlowDownTime()
        {
            /// <summary>
            /// The new time scale for the game.
            /// </summary>
            float newTimeScale = slowTimeSpeed;
            Time.timeScale = newTimeScale;
            Time.fixedDeltaTime = newTimeScale * Time.deltaTime;
        }

        /// <summary>
        /// Restores the time to its original settings.
        /// </summary>
        public void RestoreTime()
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = originalFixedTime;
            Debug.Log("Time 1");
        }

        /// <summary>
        /// Stops the time by setting the time scale to 0.
        /// </summary>
        public void StopTime()
        {
            Time.timeScale = 0;
            Debug.Log("Time 0");
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(TimeControlBehavior))]
	//[CanEditMultipleObjects]

    public class CustomTimeControlInspector : Editor
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
        TimeControlBehavior SlowMotionBehavior = (TimeControlBehavior)target;
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