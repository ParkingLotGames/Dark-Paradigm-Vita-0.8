using UnityEngine;
using DP.DevTools;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.ScriptableEvents
{
    [CreateAssetMenu(menuName = "Scriptable Event System/Game Event For List")]
    /// <summary>
    /// A ScriptableObject representing a Game Event that listeners can subscribe to and be notified when the event is raised.
    /// </summary>
    public class GameEventForList : ScriptableObject
    {
        /// <summary>
        /// A list of GameEventListenerForList that have subscribed to this event.
        /// </summary>
        List<GameEventListenerForList> listeners = new List<GameEventListenerForList>();

        /// <summary>
        /// Registers a new GameEventListenerForList to be notified when this event is raised.
        /// </summary>
        /// <param name="newListener">The GameEventListenerForList to register.</param>
        public void RegisterListener(GameEventListenerForList newListener)
        {
            listeners.Add(newListener);
        }

        /// <summary>
        /// Unregisters a GameEventListenerForList so that it will no longer be notified when this event is raised.
        /// </summary>
        /// <param name="listenerToRemove">The GameEventListenerForList to unregister.</param>
        public void UnregisterListener(GameEventListenerForList listenerToRemove)
        {
            listeners.Remove(listenerToRemove);
        }

        /// <summary>
        /// Raises this event, notifying all registered listeners.
        /// </summary>
        public void RaiseEvent()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(GameEventForList))]
    //[CanEditMultipleObjects]

    public class CustomGameEventForListInspector : Editor
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
            GameEventForList GameEvent = (GameEventForList)target;
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