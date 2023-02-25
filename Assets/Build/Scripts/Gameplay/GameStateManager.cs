using UnityEngine;
using DP.DevTools;
using UnityEngine.Events;
using DP.Gameplay;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.Management
{
    /// <summary>
    /// Manages the current state of the game.
    /// </summary>
    public class GameStateManager : MonoBehaviour
    {
        /// <summary>
        /// Enumeration of all possible game states.
        /// </summary>
        public enum GameState
        {
            WaitingToStart, Playing, Paused, Over, Photo, ItemWheel
        }

        /// <summary>
        /// The listener that waits for the game to start.
        /// </summary>
        ListenForGameStart listenForGameStart;

        /// <summary>
        /// The current state of the game.
        /// </summary>
        [SerializeField] public GameState gameState;

        /// <summary>
        /// The UnityEvent that is invoked when the game is waiting to start.
        /// </summary>
        [SerializeField] public UnityEvent OnWaitingToStartGame;

        /// <summary>
        /// The UnityEvent that is invoked when the game starts.
        /// </summary>
        [SerializeField] public UnityEvent OnStartGame;

        /// <summary>
        /// The UnityEvent that is invoked when the game is paused.
        /// </summary>
        [SerializeField] public UnityEvent OnPause;

        /// <summary>
        /// The UnityEvent that is invoked when the game is resumed.
        /// </summary>
        [SerializeField] public UnityEvent OnResume;

        /// <summary>
        /// The UnityEvent that is invoked when the game is over.
        /// </summary>
        [SerializeField] public UnityEvent OnGameOver;

        /// <summary>
        /// The UnityEvent that is invoked when the game is in photo mode.
        /// </summary>
        [SerializeField] public UnityEvent OnPhotoMode;

        /// <summary>
        /// The UnityEvent that is invoked when the game is in item wheel mode.
        /// </summary>
        [SerializeField] public UnityEvent OnItemWheelMode;

        /// <summary>
        /// Determines if the OnStartGame event has been invoked.
        /// </summary>
        public bool OnStartInvoked;

        void Start()
        {
            listenForGameStart = GetComponent<ListenForGameStart>();
            ResetGame();
        }

        /// <summary>
        /// Resets the game to its default state.
        /// </summary>
        public void ResetGame()
        {
            gameState = GameState.WaitingToStart;
            OnWaitingToStartGame.Invoke();
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public void StartGame()
        {
            gameState = GameState.Playing;
            if (!OnStartInvoked)
            {
                OnStartGame.Invoke();
                OnStartInvoked = true;
            }
        }

        /// <summary>
        /// Ends the game.
        /// </summary>
        public void GameOver()
        {
            gameState = GameState.Over;
            OnGameOver.Invoke();
            OnStartInvoked = false;
        }

        /// <summary>
        /// Enables the listener for the game to start.
        /// </summary>
        public void EnableListenForGameStart()
        {
            listenForGameStart.enabled = true;
        }

        /// <summary>
        /// Disables the listener for the game to start.
        /// </summary>
        public void DisableListenForGameStart()
        {
            listenForGameStart.enabled = false;
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(GameStateManager))]
	//[CanEditMultipleObjects]

    public class CustomGameStateManagerInspector : Editor
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
        GameStateManager GameStateManager = (GameStateManager)target;
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