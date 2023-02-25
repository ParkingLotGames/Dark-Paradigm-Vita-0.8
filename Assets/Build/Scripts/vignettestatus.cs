

using UnityEngine;
using DP.DevTools;
using UnityEngine.UI;
using DP.StaticDataContainers;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Controls the vignette effect and status text.
    /// </summary>
    public class vignettestatus : MonoBehaviour
    {
        /// <summary>
        /// The image component that displays the vignette effect.
        /// </summary>
        [SerializeField]
        private Image vignette;

        /// <summary>
        /// The text component that displays the vignette status.
        /// </summary>
        [SerializeField]
        private Text text;

        /// <summary>
        /// Updates the vignette status text and enables/disables the vignette effect when the select button is pressed.
        /// </summary>
        private void Update()
        {
            text.text = "Vignette enabled: " + Vignette.Instance.img.enabled;

            if (Vignette.Instance.img.enabled)
            {
                if (Input.GetButtonDown(PSVitaInputValues.Select))
                    Vignette.Instance.img.enabled = false;
            }
            else
            {
                if (Input.GetButtonDown(PSVitaInputValues.Select))
                    Vignette.Instance.img.enabled = true;
            }
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(vignettestatus))]
	[CanEditMultipleObjects]

    public class CustomvignettestatusInspector : Editor
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
        vignettestatus vignettestatus = (vignettestatus)target;
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