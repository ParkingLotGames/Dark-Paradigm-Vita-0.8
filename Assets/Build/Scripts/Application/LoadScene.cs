using UnityEngine;
using DP.DevTools;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Loads the specified scene synchronously or asynchronously with a delay.
    /// </summary>
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad;
        [SerializeField] private int sceneIndexToLoad;
        [SerializeField] private bool useIndex;
        private bool triggered;

        /// <summary>
        /// Loads the specified scene synchronously or asynchronously.
        /// </summary>
        public void LoadSceneSync()
        {
            if (!triggered)
            {
                if (!useIndex)
                {
                    SceneManager.LoadSceneAsync(sceneToLoad);
                    triggered = true;
                }
                else
                {
                    SceneManager.LoadSceneAsync(sceneIndexToLoad);
                    triggered = true;
                }
            }
        }

        /// <summary>
        /// Loads the specified scene with a delay of 0.15 seconds.
        /// </summary>
        public void LoadSceneDelayed() { Invoke("LoadSceneSync", .15f); }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(LoadScene))]
    [CanEditMultipleObjects]

    public class CustomLoadSceneInspector : Editor
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
            LoadScene LoadScene = (LoadScene)target;
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