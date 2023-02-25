using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// A class that triggers transparency on an array of renderers when the player enters a collider trigger, and reverts back to the original shader when the player leaves the trigger.
    /// </summary>
    public class TransparencyTriggerForArray : MonoBehaviour
    {
        ///<summary>
        ///An array of renderers to trigger transparency on.
        ///</summary>
        [SerializeField]
        private Renderer[] renderers;

        ///<summary>
        ///The original diffuse shader used by the renderers.
        ///</summary>
        private Shader diffuseShader;

        ///<summary>
        ///The transparent shader used to make the renderers transparent.
        ///</summary>
        private Shader transparentShader;

        ///<summary>
        ///A boolean value that represents whether transparency is being applied to the renderers.
        ///</summary>
        private bool transparenting;

        ///<summary>
        ///The tag of the player object that triggers transparency.
        ///</summary>
        private string playerTag = "Player";

        ///<summary>
        ///The length of the array of renderers.
        ///</summary>
        private int renderersLength;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            renderersLength = renderers.Length;
            diffuseShader = Shader.Find("PS Vita/Lit/Diffuse/Simple Diffuse");
            transparentShader = Shader.Find("PS Vita/Lit/Diffuse/Transparent");
        }

        /// <summary>
        /// OnTriggerStay is called once per frame for every Collider other that is touching the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                for (int i = 0; i < renderersLength; i++)
                {
                    if (renderers[i].material.shader != transparentShader)
                        renderers[i].material.shader = transparentShader;
                    renderers[i].material.SetFloat("_Alpha", 0.4f);
                }
            }
        }

        /// <summary>
        /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(playerTag))
            {
                for (int i = 0; i < renderersLength; i++)
                {
                    renderers[i].material.shader = diffuseShader;
                }
            }
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(TransparencyTriggerForArray))]
	[CanEditMultipleObjects]

    public class CustomTransparencyTriggerForArrayInspector : Editor
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
        TransparencyTriggerForArray TransparencyTriggerArray = (TransparencyTriggerForArray)target;
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