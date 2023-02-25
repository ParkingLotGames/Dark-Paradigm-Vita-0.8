using UnityEngine;
using DP.DevTools;
using System.Collections;
using UnityEngine.Events;
using Essentials;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif
// TODO: Check why it's Player mask

namespace DP.AI
{
    public class EnemyRaycast : MonoBehaviour
    {
        [SerializeField] LayerMask playerLayerMask;
        bool playerNear;
        [SerializeField] UnityEvent OnPlayerInRange, OnPlayerOutOfRange;

        /// <summary>
        /// This function checks if the player is in range and invokes OnPlayerInRange or OnPlayerOutOfRange accordingly.
        /// </summary>
#if UNITY_EDITOR
        private void Update()
        {
            // Raycast to check if player is in range
            if (Physics.Raycast(transform.position, transform.localRotation.eulerAngles, playerLayerMask))
            {
                // Invoke OnPlayerInRange event
                OnPlayerInRange.Invoke();
            }
            else
            {
                // Invoke OnPlayerOutOfRange event
                OnPlayerOutOfRange.Invoke();
            }
            // Draw a ray to visualize the raycast in the editor
            Debug.DrawRay(transform.position, transform.localRotation.eulerAngles, Colors.Cyan);
        }
#endif
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(EnemyRaycast))]
	//[CanEditMultipleObjects]

    public class CustomEnemyRaycastInspector : Editor
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
        EnemyRaycast EnemyRaycast = (EnemyRaycast)target;
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