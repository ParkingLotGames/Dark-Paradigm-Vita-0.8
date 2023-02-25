using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.DevTools
{
    /// <summary>
    /// Manager for animation hash codes.
    /// </summary>
    public class AnimationHashManager : MonoBehaviour
    {
        #region Variables

        /// <summary>
        /// The singleton instance of the AnimationHashManager.
        /// </summary>
        public static AnimationHashManager Instance;

        [HideInInspector]
        /// <summary>
        /// The hash code for the "Death" animation.
        /// </summary>
        public int deathHash;

        [HideInInspector]
        /// <summary>
        /// The hash code for the "Idle" animation.
        /// </summary>
        public int idleHash;

        [HideInInspector]
        /// <summary>
        /// The hash code for the "RunAngryFast" animation.
        /// </summary>
        public int runAngryHash;

        [HideInInspector]
        /// <summary>
        /// The hash code for the "Dissolve" animation.
        /// </summary>
        public int dissolveHash;

        [HideInInspector]
        /// <summary>
        /// The hash code for the "Hitmarker" animation.
        /// </summary>
        public int hitmarkerHash;

        #endregion

        #region Methods

        private void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);

            // Cache our hashes
            dissolveHash = Animator.StringToHash("Dissolve");
            deathHash = Animator.StringToHash("Death");
            runAngryHash = Animator.StringToHash("RunAngryFast");
            idleHash = Animator.StringToHash("Idle");
        }

        #endregion
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(AnimationHashManager))]
	//[CanEditMultipleObjects]

    public class CustomAnimationHashManagerInspector : Editor
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
        AnimationHashManager AnimationHashManager = (AnimationHashManager)target;
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