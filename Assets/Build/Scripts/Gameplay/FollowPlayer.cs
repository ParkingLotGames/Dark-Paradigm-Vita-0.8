using UnityEngine;
using DP.DevTools;
using DP.Controllers;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.Gameplay
{
    /// <summary>
    /// A component that causes its attached game object to follow a player anchor object.
    /// </summary>
    public class FollowPlayer : MonoBehaviour
    {
        /// <summary>
        /// The anchor object that the attached game object should follow.
        /// </summary>
        [SerializeField] GameObject playerAnchor;

        /// <summary>
        /// Called every fixed framerate frame.
        /// Causes the attached game object to move to the position of the player anchor object.
        /// </summary>
        private void FixedUpdate()
        {
            transform.position = playerAnchor.transform.position;
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(FollowPlayer))]
	//[CanEditMultipleObjects]

    public class CustomFollowPlayerInspector : Editor
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
        FollowPlayer FollowPlayer = (FollowPlayer)target;
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