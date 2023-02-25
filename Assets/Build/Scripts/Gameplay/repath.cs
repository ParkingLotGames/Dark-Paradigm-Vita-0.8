using UnityEngine;
using DP.DevTools;
using DP.Controllers;
using UnityEngine.AI;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// A helper class for updating the navigation path of the attached NavMeshAgent
    /// to follow the player character.
    /// </summary>
    public class repath : MonoBehaviour
    {
        /// <summary>
        /// The player character that the NavMeshAgent will follow.
        /// </summary>
        GameObject Player;

        /// <summary>
        /// The NavMeshAgent component attached to this GameObject.
        /// </summary>
        public NavMeshAgent navAgent;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            Player = FindObjectOfType<PlayerMovementController>().gameObject;
            navAgent = GetComponent<NavMeshAgent>();
        }

        /// <summary>
        /// Update is called once per frame and updates the destination of the NavMeshAgent
        /// to follow the player character.
        /// </summary>
        private void Update()
        {
            navAgent.SetDestination(Player.transform.position);
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(repath))]
	//[CanEditMultipleObjects]

    public class CustomrepathInspector : Editor
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

        //base.OnInspectorGUI();
        repath repath = (repath)target;
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