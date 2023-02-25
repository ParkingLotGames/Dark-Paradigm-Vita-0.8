using UnityEngine;
using DP.DevTools;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Controls the sequence of intro still images and animations.
    /// </summary>
    public class IntroStillImages : MonoBehaviour
    {
        /// <summary>
        /// Event invoked when the KyuHEN splash fades.
        /// </summary>
        [SerializeField] UnityEvent fadeKyuHEN;

        /// <summary>
        /// Event invoked when the KyuHEN splash is hidden.
        /// </summary>
        [SerializeField] UnityEvent hideKyuHEN;

        /// <summary>
        /// Event invoked when the info screen is hidden.
        /// </summary>
        [SerializeField] UnityEvent hideInfo;

        /// <summary>
        /// Event invoked when the info screen fades.
        /// </summary>
        [SerializeField] UnityEvent fadeInfo;

        /// <summary>
        /// Invoked when the object is enabled.
        /// </summary>
        void OnEnable()
        {
            Invoke("FadeKyuHENSplash", 2.5f);
        }

        /// <summary>
        /// Fades the KyuHEN splash.
        /// </summary>
        void FadeKyuHENSplash()
        {
            fadeKyuHEN.Invoke();
            Invoke("HideKyuHENSplash", .5f);
        }

        /// <summary>
        /// Hides the KyuHEN splash.
        /// </summary>
        void HideKyuHENSplash()
        {
            hideKyuHEN.Invoke();
            Invoke("FadeInfoScreen", 2.5f);
        }

        /// <summary>
        /// Fades the info screen.
        /// </summary>
        void FadeInfoScreen()
        {
            fadeInfo.Invoke();
            Invoke("HideInfoScreen", .5f);
        }

        /// <summary>
        /// Hides the info screen.
        /// </summary>
        void HideInfoScreen()
        {
            hideInfo.Invoke();
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(IntroStillImages))]
	[CanEditMultipleObjects]

    public class CustomIntroStillImagesInspector : Editor
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
        IntroStillImages IntroStillImages = (IntroStillImages)target;
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