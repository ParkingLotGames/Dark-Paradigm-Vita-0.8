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
    /// Manages uploading the player's high score to the server.
    /// </summary>
    public class UploadScore : MonoBehaviour
    {
        string playerName = "PSVita";
        int playerScore = 1236;

        private void Awake()
        {
            // This method is called when the object is created, but we don't need to do anything here.
        }

        /// <summary>
        /// Uploads the current high score to the server.
        /// </summary>
        public void Upload()
        {
            HighScores.UploadScore(playerName, playerScore);
        }

        /// <summary>
        /// Sets the player's score to the given value.
        /// </summary>
        /// <param name="score">The new high score for the player.</param>
        public void SetNewHighScore(int score)
        {
            playerScore = score;
        }

        /// <summary>
        /// Sets the player's name to the given value.
        /// </summary>
        /// <param name="username">The new username for the player.</param>
        public void SetPlayerName(string username)
        {
            playerName = username;
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(UploadScore))]
    [CanEditMultipleObjects]

    public class CustomUploadScoreInspector : Editor
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
            UploadScore UploadScore = (UploadScore)target;
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