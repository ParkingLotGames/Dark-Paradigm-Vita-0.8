using DP.StaticDataContainers;
using UnityEngine;
using DP.ResourceManagement;
using DP.ScriptableEvents;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DP.Management
{
    /// <summary>
    /// This class manages the game state and transitions between different states.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        //public PlayerStatsEventSystem playerStatsEventSystem;
        /// <summary>
        /// The current game mode.
        /// </summary>
        public enum GameMode { Arcade }
        [SerializeField] public GameMode gameMode;
        /// <summary>
        /// The current game state.
        /// </summary>
        [SerializeField] public GameState gameState;
        /// <summary>
        /// The player's current points.
        /// </summary>
        [SerializeField]public int playerPoints;
        /// <summary>
        /// The player's current cash.
        /// </summary>
        [SerializeField]public int playerCash;
        /// <summary>
        /// The total number of enemies killed by the player.
        /// </summary>
        [SerializeField]public int totalEnemiesKilled;
        float photoModeHoldElapsed;
        /// <summary>
        /// Whether the player has used the select button to enter photo mode.
        /// </summary>
        public bool usedSelectForPhotoMode;

        float originalFixedDeltaTime;

        /// <summary>
        /// Whether the game has started.
        /// </summary>
        public bool gameStarted;

        /// <summary>
        /// Whether the player is currently playing the game.
        /// </summary>
        public bool playing;

        /// <summary>
        /// Whether the game has ended.
        /// </summary>
        public bool gameOver;

        /// <summary>
        /// Whether the game is currently in photo mode.
        /// </summary>
        public bool photoMode;

        /// <summary>
        /// Whether the HUD should be hidden.
        /// </summary>
        public bool hideHUD;

        /// <summary>
        /// Whether the item wheel should be shown.
        /// </summary>
        public bool showItemWheel;

        [SerializeField] UnityEvent OnGameStarted, OnGameOver, OnPauseGame, OnResumeGame, OnRestartGame, OnEnterPhotoMode, OnEnterPhotoModeShortcut, OnExitPhotoModeToPause;
        bool pauseGameRaised, resumeGameRaised;
        private bool thisScriptCanControlTime;

        public UnityEvent OnResetGame;
        public UnityEvent OnResetPlayerCounters;

        /// <summary>
        /// Initializes the GameManager instance and prepares the screen.
        /// </summary>
        private void Awake()
        {
            Singleton();
            PrepareScreen();
        }

        /// <summary>
        /// Invokes the <c>OnResetPlayerCounters</c> and <c>OnGameStarted</c> events when the game starts.
        /// </summary>
        private void Start()
        {
            OnResetPlayerCounters.Invoke();
            OnGameStarted.Invoke();
        }

        /// <summary>
        /// Resets the game state.
        /// </summary>
        public void ResetGame()
        {
            OnResetGame.Invoke();
        }

        /// <summary>
        /// Increments the number of enemies killed by the player.
        /// </summary>
        public void CountKill()
        {
            totalEnemiesKilled += 1;
        }

        /// <summary>
        /// Updates the current game state based on the player's inputs and game state.
        /// </summary>
        private void Update()
        {
            InputFSM();
            StateFSM();
        }
        /// <summary>
        /// Updates the current game state based on the player's actions.
        /// </summary>
        void StateFSM()
        {
            if (gameStarted)
                gameState = GameState.WaitingToStart;
            if (playing)
                gameState = GameState.Playing;
            if (gameStarted && !playing)
                gameState = GameState.Paused;
            if (gameOver)
                gameState = GameState.Over;
            if (photoMode)
                gameState = GameState.Photo;
            if (showItemWheel)
                gameState = GameState.ItemWheel;
        }


        #region FSMs
        /// <summary>
        /// Updates the game state based on input.
        /// </summary>
        void InputFSM()
        {
            if (gameState == GameState.WaitingToStart)
            {
                PrepareScreen();
                WaitToStartGame();
            }
            if (gameState == GameState.Playing)
            {
                ResumeTime();
                CheckForPause();
                CheckForPhotoMode();
            }
            if (gameState == GameState.Paused)
            {
                BlockTouchUIInput();
                StopTime();
                CheckForResume();
            }
            if (gameState == GameState.Over)
            {
                ShowGameOverUI();
                BlockTouchUIInput();
                StopTime();
            }
            if (gameState == GameState.Photo)
            {
                BlockTouchUIInput();
                StopTime();
                CheckForResume();
                ExitPhotoModeBehavior();
                //        UI.PhotoModeUIManager.Instance.PhotoModeUIManagement();
            }
        }
        /// <summary>
        /// Hide Pause UI.
        /// </summary>
        public void HidePauseUI()
        {
            //      Management.PauseManager.Instance.Hide();
        }
        /// <summary>
        /// Shows the Game Over UI over the Game's UI.
        /// </summary>
        private void ShowGameOverUI()
        {
            //        Management.GameOverManager.Instance.gameOverCanvas.SetActive(true);
        }
        /// <summary>
        /// Disables the Touchi UI Input capabilities.
        /// </summary>
        private void BlockTouchUIInput()
        {
#if UNITY_PSP2
            //UI.NoMouse.Instance.NoMouseBehavior();
#endif
        }
        /// <summary>
        /// Updates the game state when the player exits photo mode.
        /// </summary>
        private void ExitPhotoModeBehavior()
        {
            if (usedSelectForPhotoMode)
            {
                if (Input.GetButtonDown(PSVitaInputValues.Circle))
                {
                    ///photoMode = false;
                    ///playing = true;
                    ///usedSelectForPhotoMode = false;
                    ///hideHUD = false;
                    //                    Management.HUDManager.Instance.ManageCrosshairsVisibility();
                    //                    Management.HUDManager.Instance.ManageHUDVisibility();
                }
            }
            else
            {
                if (Input.GetButtonDown(PSVitaInputValues.Circle)) { }
                    photoMode = false;
            }
        }

        /*/// <summary>
/// Updates the game state based on the current game status.
/// </summary>
         * void StateFSM()
        {
            if (!gameStarted)
                gameState = GameState.WaitingToStart;
            if (playing)
                gameState = GameState.Playing;
            if (gameStarted && !playing)
                gameState = GameState.Paused;
            if (gameOver)
                gameState = GameState.Over;
            if (photoMode)
                gameState = GameState.Photo;
            if (ItemWheel)
                gameState = GameState.ItemWheel;
        }*/

        #endregion

        #region Methods

        #region Frequent
        /// <summary>
        /// Makes this GameManager instance a Singleton.
        /// </summary>
        void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
                Destroy(gameObject);
        }

        /// <summary>
        /// Resumes time if this script can control time.
        /// </summary>
        void ResumeTime()
        {
            if (thisScriptCanControlTime)
            Time.timeScale = 1;
        }
        /// <summary>
        /// Stops time if this script can control time.
        /// </summary>
        void StopTime()
        {
            if (thisScriptCanControlTime)
                Time.timeScale = 0;
        }
        /// <summary>
        /// Allows this script to control time.
        /// </summary>
        public void AllowTimeControl()
        {
            thisScriptCanControlTime = true;
        }
        /// <summary>
        /// Prevents this script from controlling time.
        /// </summary>
        public void DenyTimeControl()
        {
            thisScriptCanControlTime = false;
        }

        #endregion

        #region Waiting to Start

        /// <summary>
        /// Prepares the screen for the game.
        /// </summary>
        void PrepareScreen()
        {
            //pp enabled from itself since instance = null at awake
            //if (LoadScreen.Instance)
            //    LoadScreen.Instance.inUse = false;
            HideGameOverScreen();
            StopTime();
        }

        /// <summary>
        /// Hides the game over screen.
        /// </summary>
        private static void HideGameOverScreen()
        {
            //if (Management.GameOverManager.Instance)
            //    Management.GameOverManager.Instance.gameOverCanvas.SetActive(false);

        }
        /// <summary>
        /// Waits for the player to start the game.
        /// </summary>
        void WaitToStartGame()
        {
#if !UNITY_ANDROID
            //if (Input.anyKeyDown)
            {
                ResumeTime();
                gameStarted = true;
                playing = true;
            //    PostProcessingManager.Instance.DisablePostProcessing();
            }
#endif
#if UNITY_ANDROID
            if (!Application.isEditor)
            {
                if (Input.touchCount > 0)
                {
                    ResumeTime();
                    gameStarted = true;
                    playing = true;
                    //    PostProcessingManager.Instance.DisablePostProcessing();
                }
            }
            else
            {
                if (Input.anyKeyDown)
                {
                    ResumeTime();
                    gameStarted = true;
                    playing = true;
                    //    PostProcessingManager.Instance.DisablePostProcessing();
                }
            }
#endif
        }
        #endregion

        #region Playing
        /// <summary>
        /// Updates the game state when the game is paused.
        /// </summary>
        void CheckForPause()
        {
            if (Input.GetButtonDown(PSVitaInputValues.Start))
            {
                hideHUD = true;
                //    Management.PauseManager.Instance.pauseObject.SetActive(true);
                //    Management.HUDManager.Instance.ManageCrosshairsVisibility();
                //    Management.HUDManager.Instance.ManageHUDVisibility();
                //    PostProcessingManager.Instance.EnablePostProcessing();
                playing = false;
                if (!pauseGameRaised)
                {
                    resumeGameRaised = false;
                    OnPauseGame.Invoke();
                    pauseGameRaised = true;
                }
            }
        }
        /// <summary>
        /// Updates the game state when the player enters photo mode.
        /// </summary>
        void CheckForPhotoMode()
        {
            float timeToHold = 1;

            if (Input.GetButton(PSVitaInputValues.Select))
            {
                photoModeHoldElapsed += Time.deltaTime;
                if (photoModeHoldElapsed > timeToHold)
                {
                    photoMode = true;
                    hideHUD = true;
                    //      Management.HUDManager.Instance.ManageCrosshairsVisibility();
                    //      Management.HUDManager.Instance.ManageHUDVisibility();
                    playing = false;
                    photoModeHoldElapsed = 0;
                    usedSelectForPhotoMode = true;
                }
            }
            else
                photoModeHoldElapsed = 0;
        }
        #endregion

        #region Paused

        /// <summary>
        /// Checks if the player resumed the game.
        /// </summary>
        void CheckForResume()
        {
            if (Input.GetButtonDown(PSVitaInputValues.Start))
            {
                if (!resumeGameRaised)
                {
                    pauseGameRaised = false;
                    OnResumeGame.Invoke();
                    resumeGameRaised = true;
                }
                playing = true;
                photoMode = false;
                hideHUD = false;
                //Management.PauseManager.Instance.Hide();
                //Management.HUDManager.Instance.ManageCrosshairsVisibility();
                //Management.HUDManager.Instance.ManageHUDVisibility();
                //PostProcessingManager.Instance.DisablePostProcessing();
            }
        }

        void PauseManagement()
        {

        }

        #endregion

        #endregion
    }

    public enum GameState
    {
        WaitingToStart, Playing, Paused, Over, Photo, ItemWheel
    }
    
    /*#region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(GameManager))]
	//[CanEditMultipleObjects]

    public class CustomGameManagerInspector : Editor
	{
        public override void OnInspectorGUI()
        {
            #region Setup
            #region Available GUIStyles
            GUIStyle button = new GUIStyle();
            GUIStyle leftAlignedSubHeader = new GUIStyle();
            GUIStyle centeredHeader = new GUIStyle();
            GUIStyle smallLabel = new GUIStyle();
            #endregion
            #region Setup GUIStyles

            Font font;
            //Get Font
            font = (Font)Resources.Load(ResourcesPathContainer.oswaldFontPath);

            button.font = font;
            button.fontSize = 13;
            button.fontStyle = FontStyle.Bold;
            button.alignment = TextAnchor.MiddleCenter;
            button.normal.textColor = Color.white;
            button.normal.background = (Texture2D)Resources.Load(ResourcesPathContainer.editorButtonBlack);
            button.normal.background = (Texture2D)Resources.Load(ResourcesPathContainer.editorButtonBlackClick);

            leftAlignedSubHeader.font = font;
            leftAlignedSubHeader.fontSize = 15;
            leftAlignedSubHeader.fontStyle = FontStyle.Bold;
            leftAlignedSubHeader.alignment = TextAnchor.MiddleLeft;
            leftAlignedSubHeader.normal.textColor = Color.white;

            centeredHeader.font = font;
            centeredHeader.fontSize = 20;
            centeredHeader.fontStyle = FontStyle.Bold;
            centeredHeader.alignment = TextAnchor.MiddleCenter;
            centeredHeader.normal.textColor = Color.white;

            smallLabel.font = font;
            smallLabel.fontSize = 15;
            smallLabel.alignment = TextAnchor.MiddleCenter;
            smallLabel.normal.textColor = Color.white;

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
            #region Serialized Properties
            GameManager gameManager = (GameManager)target;

            SerializedProperty gameMode = serializedObject.FindProperty("gameMode");
            SerializedProperty gameState = serializedObject.FindProperty("gameState");
            SerializedProperty playerPoints = serializedObject.FindProperty("playerPoints");
            SerializedProperty playerCash = serializedObject.FindProperty("playerCash");
            SerializedProperty totalEnemiesKilled = serializedObject.FindProperty("totalEnemiesKilled");
            SerializedProperty gameStarted = serializedObject.FindProperty("gameStarted");
            SerializedProperty playing = serializedObject.FindProperty("playing");
            SerializedProperty gameOver = serializedObject.FindProperty("gameOver");
            SerializedProperty photoMode = serializedObject.FindProperty("photoMode");
            SerializedProperty hideHUD = serializedObject.FindProperty("hideHUD");
            SerializedProperty ItemWheel = serializedObject.FindProperty("showItemWheel");
            SerializedProperty currentGameStateSO = serializedObject.FindProperty("currentGameState");
            #endregion
            #endregion

            #region Custom Inspector Behavior
            serializedObject.Update();
            
            GUILayout.Label("Game Manager", centeredHeader);

            GUILayout.Label("Game Status", leftAlignedSubHeader);
            EditorGUILayout.BeginHorizontal();

            EditorGUIUtility.labelWidth = 96;
            EditorGUILayout.PropertyField(gameMode);

            EditorGUIUtility.labelWidth = 81;
            EditorGUILayout.PropertyField(gameState);

            EditorGUILayout.EndHorizontal();

            GUILayout.Label("Player Stats", leftAlignedSubHeader);
            EditorGUILayout.BeginHorizontal();

            EditorGUIUtility.labelWidth = 96;
            EditorGUILayout.PropertyField(playerPoints, width144);

            EditorGUIUtility.labelWidth = 80;
            EditorGUILayout.PropertyField(playerCash, width128);

            EditorGUILayout.EndHorizontal();


            GUILayout.Label("Game Status Monitor", leftAlignedSubHeader);
            EditorGUILayout.BeginHorizontal();

            EditorGUIUtility.labelWidth = 96;
            EditorGUILayout.PropertyField(gameStarted);

            EditorGUIUtility.labelWidth = 48;
            EditorGUILayout.PropertyField(playing);

            EditorGUIUtility.labelWidth = 80;
            EditorGUILayout.PropertyField(gameOver);

            EditorGUILayout.EndHorizontal();


            GUILayout.Label("UI Status Monitor", leftAlignedSubHeader);
            EditorGUILayout.BeginHorizontal();

            EditorGUIUtility.labelWidth = 96;
            EditorGUILayout.PropertyField(photoMode);

            EditorGUIUtility.labelWidth = 64;
            EditorGUILayout.PropertyField(hideHUD);

            EditorGUIUtility.labelWidth = 80;
            EditorGUILayout.PropertyField(ItemWheel);

            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties(); 
            #endregion
        }
    }
#endif
    #endregion*/
}