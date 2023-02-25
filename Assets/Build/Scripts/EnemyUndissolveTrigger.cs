using UnityEngine;
using DP.DevTools;
using DP.AI;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Controls the undissolve trigger for enemies
    /// </summary>
    public class EnemyUndissolveTrigger : MonoBehaviour
    {
        /// <summary>
        /// The time elapsed since the script was enabled.
        /// </summary>
        private float t;

        /// <summary>
        /// The enemy dissolve behavior component attached to this game object.
        /// </summary>
        private EnemyDissolveBehavior enemyDissolveBehavior;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            enemyDissolveBehavior = GetComponent<EnemyDissolveBehavior>();
        }

        /// <summary>
        /// Called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
            t = 0;
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
            t += Time.deltaTime;
            if (t < 0.7f)
                enemyDissolveBehavior.UndissolveAnimation();
            else
                enabled = false;
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(EnemyUndissolveTrigger))]
	[CanEditMultipleObjects]

    public class CustomEnemyUndissolveTriggerInspector : Editor
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
        EnemyUndissolveTrigger EnemyUndissolveTrigger = (EnemyUndissolveTrigger)target;
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