using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.UI
{
    /// <summary>
    /// Class that defines the behavior of a hitmarker in a game.
    /// </summary>
    public class HitmarkerBehavior : MonoBehaviour
    {
        RectTransform rectTransform;
        RectTransform[] partsRectTransform;
        int partsCount;

        /// <summary>
        /// A boolean that determines whether the hitmarker behavior is enabled.
        /// </summary>
        [SerializeField] bool behaviorEnabled;

        /// <summary>
        /// A boolean that determines whether the hitmarker behavior should use an animator.
        /// </summary>
        [SerializeField] bool useAnimator;

        /// <summary>
        /// The speed at which the hitmarker should rotate.
        /// </summary>
        [SerializeField] float hitmarkerRotationSpeed;

        /// <summary>
        /// The size the hitmarker should expand to when activated.
        /// </summary>
        [SerializeField] float hitmarkerExpandedSize;

        /// <summary>
        /// The size the hitmarker should compress to when deactivated.
        /// </summary>
        [SerializeField] float hitmarkerCompressedSize;

        /// <summary>
        /// The speed at which the hitmarker should breathe when expanding and compressing.
        /// </summary>
        [SerializeField] float hitmarkerBreatheSpeed;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            partsRectTransform = GetComponentsInChildren<RectTransform>();
            partsCount = partsRectTransform.Length;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            if (!useAnimator)
            {
                if (behaviorEnabled)
                {
                    for (int i = 0; i < partsCount; i++)
                    {
                        if (partsRectTransform[i].localRotation.z != 60)
                            partsRectTransform[i].Rotate(0, 0, Mathf.Lerp(0, 60, hitmarkerRotationSpeed * Time.fixedDeltaTime));
                    }
                }
                Vector2 expanded = new Vector2(hitmarkerExpandedSize, hitmarkerExpandedSize);
                Vector2 compressed = new Vector2(hitmarkerCompressedSize, hitmarkerCompressedSize);
                if (rectTransform.sizeDelta != expanded)
                    rectTransform.sizeDelta = Vector2.LerpUnclamped(compressed, expanded, hitmarkerBreatheSpeed * Time.fixedDeltaTime);
            }
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(HitmarkerBehavior))]
	//[CanEditMultipleObjects]

    public class CustomHitmarkerBehaviorInspector : Editor
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
        HitmarkerBehavior HitmarkerBehavior = (HitmarkerBehavior)target;
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