using UnityEngine;
using DP.DevTools;
using UnityEngine.Events;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.ScriptableEvents
{
    /// <summary>
    /// An public class that listens for events of type <see cref="GameEventForList"/> and responds with a UnityEvent.
    /// </summary>
    public class GameEventListenerForList : MonoBehaviour
    {
        /// <summary>
        /// The game event this listener is registered to.
        /// </summary>
        [SerializeField] public GameEventForList gameEvent;

        /// <summary>
        /// The UnityEvent to be invoked in response to the game event.
        /// </summary>
        [SerializeField] UnityEvent response;

        /// <summary>
        /// Called when this component becomes enabled.
        /// </summary>
        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        /// <summary>
        /// Called when this component becomes disabled.
        /// </summary>
        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        /// <summary>
        /// Invokes the response UnityEvent in response to the game event being raised.
        /// </summary>
        public void OnEventRaised()
        {
            response.Invoke();
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(GameEventListenerForList))]
    [CanEditMultipleObjects]

    public class CustomGameEventListenerForListInspector : Editor
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
            GameEventListenerForList GameEventListener = (GameEventListenerForList)target;
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