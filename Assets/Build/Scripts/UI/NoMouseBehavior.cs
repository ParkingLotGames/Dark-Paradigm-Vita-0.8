
using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif
using System.Collections;
using UnityEngine.EventSystems;

namespace DP.UI
{
    /// <summary>
    /// This script is responsible for handling input when there is no mouse available. It listens to the event system
    /// to determine if a game object is selected or not, and selects a default object if none is currently selected.
    /// </summary>
    public class NoMouseBehavior : MonoBehaviour
    {
        EventSystem eventSystem;
        private GameObject selectedObj;

        /// <summary>
        /// Initializes the NoMouseBehavior by getting the EventSystem component from the same GameObject that the script is
        /// attached to.
        /// </summary>
        private void Awake()
        {
            eventSystem = GetComponent<EventSystem>();
        }

        /// <summary>
        /// Handles input for the NoMouseBehavior script by checking if there is a selected object in the EventSystem, and
        /// selecting a default object if there isn't one currently selected.
        /// </summary>
        public void Update()
        {
            if (eventSystem)
            {
            //    if (ManagementRefactor.GameManager.Instance.gameState == ManagementRefactor.GameState.Paused)
                if (!eventSystem.currentSelectedGameObject)
                    eventSystem.SetSelectedGameObject(selectedObj);
                selectedObj = eventSystem.currentSelectedGameObject;
            }
        }
    }



    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(NoMouseBehavior))]
    //[CanEditMultipleObjects]

    public class CustomNoMouseBehaviorInspector : Editor
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
            NoMouseBehavior NoMouseBehavior = (NoMouseBehavior)target;
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