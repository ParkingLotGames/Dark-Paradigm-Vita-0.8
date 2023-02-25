using UnityEngine;
using DP.DevTools;
using DP.StaticDataContainers;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Handles the Title Screen of the game and transitions to the next scene when the Start button is pressed.
    /// </summary>
    public class TitleScreen : MonoBehaviour
    {
        bool keyPressed;

        [SerializeField]
        UnityEvent OnPressStart;

        /// <summary>
        /// Checks for user input and triggers events when the Start button is pressed.
        /// </summary>
        private void Update()
        {
            if (!keyPressed)
            {
                if (Input.GetButtonDown(PSVitaInputValues.Cross) || Input.GetButtonDown(PSVitaInputValues.Start))
                {
                    FadeToBlack.StaticHideContents();
                    OnPressStart.Invoke();
                    Invoke("NextScene", .5f);
                }
            }
        }

        /// <summary>
        /// Loads the next scene asynchronously.
        /// </summary>
        private void NextScene()
        {
            SceneManager.LoadSceneAsync(2);
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(TitleScreen))]
	[CanEditMultipleObjects]

    public class CustomTitleScreenInspector : Editor
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
        TitleScreen TitleScreen = (TitleScreen)target;
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