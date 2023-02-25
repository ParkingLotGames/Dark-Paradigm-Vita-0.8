using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Controls the transparency of an object's material when a certain collider is triggered.
    /// </summary>
    public class TransparencyTrigger : MonoBehaviour
    {
        /// <summary>
        /// The renderer attached to the parent object.
        /// </summary>
        Renderer renderer;

        /// <summary>
        /// The shader to use when the object is not transparent.
        /// </summary>
        Shader diffuseShader;

        /// <summary>
        /// The shader to use when the object is transparent.
        /// </summary>
        Shader transparentShader;

        /// <summary>
        /// A flag indicating whether the object is currently transparent or not.
        /// </summary>
        bool transparenting;

        /// <summary>
        /// The tag used to identify the player object.
        /// </summary>
        string playerTag = "Player";

        /// <summary>
        /// Initializes the required variables and shaders for this script.
        /// </summary>
        private void Awake()
        {
            renderer = GetComponentInParent<Renderer>();
            diffuseShader = Shader.Find("PS Vita/Lit/Diffuse/Simple Diffuse");
            transparentShader = Shader.Find("PS Vita/Lit/Diffuse/Transparent");
        }

        /// <summary>
        /// Controls the transparency of the material when the player enters the collider.
        /// </summary>
        /// <param name="other">The collider of the object that entered the trigger.</param>
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (renderer.material.shader != transparentShader)
                    renderer.material.shader = transparentShader;
                renderer.material.SetFloat("_Alpha", 0.4f);
            }
        }

        /// <summary>
        /// Sets the object's material back to the default shader when the player exits the collider.
        /// </summary>
        /// <param name="other">The collider of the object that exited the trigger.</param>
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(playerTag))
            {
                renderer.material.shader = diffuseShader;
            }
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(TransparencyTrigger))]
	[CanEditMultipleObjects]

    public class CustomTransparencyTriggerInspector : Editor
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
        TransparencyTrigger TransparencyTrigger = (TransparencyTrigger)target;
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