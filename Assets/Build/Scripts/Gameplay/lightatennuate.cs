using UnityEngine;
using DP.DevTools;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// This class represents an object that attenuates a light source over time and displays the current time and intensity value.
    /// </summary>
    public class lightatennuate : MonoBehaviour
    {
        /// <summary>
        /// Gets the Light component attached to the same GameObject as this script.
        /// </summary>
        /// <value>The Light component attached to the same GameObject as this script.</value>
        private Light light => GetComponent<Light>();

        /// <summary>
        /// Gets the Text component of a UI object with the "Text" tag.
        /// </summary>
        /// <value>The Text component of a UI object with the "Text" tag.</value>
        private Text text => FindObjectOfType<Text>();

        /// <summary>
        /// Update is called once per frame. It attenuates the light source and updates the UI text.
        /// </summary>
        private void Update()
        {
            if (light.intensity != 0)
                light.intensity = Mathf.LerpUnclamped(1, 0, Time.deltaTime);
            text.text = Time.time.ToString() + $"light int = {light.intensity}";
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(lightatennuate))]
	[CanEditMultipleObjects]

    public class CustomlightatennuateInspector : Editor
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
        lightatennuate lightatennuate = (lightatennuate)target;
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