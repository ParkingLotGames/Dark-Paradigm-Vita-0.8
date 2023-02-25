using UnityEngine;
using DP.DevTools;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.ScriptableEvents
{
    /// <summary>
    /// This class represents an event listener that listens to a specific GameEvent. When the event is raised,
    /// the listener will respond by invoking a UnityEvent. This class is intended to be attached to a GameObject
    /// in the Unity editor.
    /// </summary>
    public class GameEventListener : MonoBehaviour
    {
        /// <summary>
        /// The GameEvent that this listener is listening to. When the event is raised, this listener will respond.
        /// </summary>
        [SerializeField] public GameEvent gameEvent;

        /// <summary>
        /// The UnityEvent that will be invoked when the GameEvent is raised.
        /// </summary>
        [SerializeField] UnityEvent response;

        /// <summary>
        /// Called when this listener is enabled. Registers this listener with its associated GameEvent.
        /// </summary>
        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        /// <summary>
        /// Called when this listener is disabled. Unregisters this listener from its associated GameEvent.
        /// </summary>
        private void OnDisable()
        {
            gameEvent.UnregisterListener();
        }

        /// <summary>
        /// Called when the associated GameEvent is raised. Invokes the UnityEvent to respond to the event.
        /// </summary>
        public void OnEventRaised()
        {
            response.Invoke();
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(GameEventListener))]
	//[CanEditMultipleObjects]

    public class CustomGameEventListenerInspector : Editor
	{
    public override void OnInspectorGUI()
    {
            #region GUIStyles
            //Define GUIStyles
            GUIStyle label = new GUIStyle();
            label.fontSize = 18;
            label.alignment = TextAnchor.UpperCenter;
            label.normal.textColor = Color.white;
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
            GameEventListener GameEventListener = (GameEventListener)target;
            if (GameEventListener.gameEvent)
                GUILayout.Label($"{GameEventListener.gameEvent.name} Event Listener", label);
            else
                EditorGUILayout.HelpBox("Please assign an event to register and listen to", MessageType.Error);
        base.OnInspectorGUI();
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