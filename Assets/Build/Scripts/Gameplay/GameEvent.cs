using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif

namespace DP.ScriptableEvents
{
    [CreateAssetMenu(menuName = "Scriptable Event System/Game Event")]
    /// <summary>
    /// Represents a game event that can be registered and raised by listeners.
    /// </summary>
    public class GameEvent : ScriptableObject
    {
        private GameEventListener listener;

        /// <summary>
        /// Registers a listener to this event.
        /// </summary>
        /// <param name="newListener">The listener to register.</param>
        public void RegisterListener(GameEventListener newListener)
        {
            listener = newListener;
        }

        /// <summary>
        /// Unregisters the current listener from this event.
        /// </summary>
        public void UnregisterListener()
        {
            listener = null;
        }

        /// <summary>
        /// Raises this event, invoking the listener's OnEventRaised() method.
        /// </summary>
        public void RaiseEvent()
        {
            listener.OnEventRaised();
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(GameEvent))]
	//[CanEditMultipleObjects]

    public class CustomGameEventInspector : Editor
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
        GameEvent GameEvent = (GameEvent)target;
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