using UnityEngine;
using DP.DevTools;
using DP.ScriptableObjects;
using DP.StaticDataContainers;
using DP.ScriptableEvents;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.Gameplay
{
    /// <summary>
    /// Controls the bullet time functionality in the game.
    /// </summary>
    public class BulletTimeController : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// A boolean flag indicating whether the bullet time functionality can be used by the player.
        /// </summary>
        [SerializeField] public bool canUseBulletTime;//bt

        /// <summary>
        /// A boolean flag indicating whether the bullet time behavior is currently enabled or not.
        /// </summary>
        public bool behaviorEnabled = true;

        #region Private Variables
        /// <summary>
        /// The Slow Motion Manager script that is used to control the bullet time functionality.
        /// </summary>
        [SerializeField] SlowMotionManager slowMotionManager;//bt

        /// <summary>
        /// A boolean flag indicating whether the bullet time functionality is currently enabled or not.
        /// </summary>
        [SerializeField] public bool bulletTimeEnabled;

        /// <summary>
        /// The maximum amount of bullet time that can be used by the player.
        /// </summary>
        [SerializeField] public float maxBulletTime;

        /// <summary>
        /// An event that is invoked to update the bullet time bar UI element.
        /// </summary>
        [SerializeField] UnityEvent updateBulletTimeBar;

        /// <summary>
        /// An event that is invoked when the bullet time functionality is over.
        /// </summary>
        [SerializeField] UnityEvent OnBulletTimeOver;

        /// <summary>
        /// The starting amount of bullet time that the player has.
        /// </summary>
        [SerializeField] float startingBulletTime;

        #endregion

        /// <summary>
        /// The player inventory script that is used to access the current bullet time left.
        /// </summary>
        [SerializeField] public PlayerInventory playerInventory;

        #endregion

        #region Methods

        /// <summary>
        /// Resets the bullet time to the starting amount.
        /// </summary>
        public void ResetBulletTime() { playerInventory.inGameInventory.bulletTimeLeft = startingBulletTime; }

        /// <summary>
        /// Updates the bullet time functionality in the game.
        /// </summary>
        void Update()
        {
            if (playerInventory.inGameInventory.bulletTimeLeft < maxBulletTime)
                updateBulletTimeBar.Invoke();
            canUseBulletTime = playerInventory.inGameInventory.bulletTimeLeft > 0;
            if (behaviorEnabled)
            {
                bulletTimeEnabled = slowMotionManager.bulletTimeInputDetected;
                float elapsed = 0f;
                if (bulletTimeEnabled)
                {
                    elapsed += Time.unscaledDeltaTime;
                    playerInventory.inGameInventory.bulletTimeLeft -= elapsed;//Scriptable Object ;)
                    if (playerInventory.inGameInventory.bulletTimeLeft <= 0)
                    {
                        canUseBulletTime = false;
                        elapsed = 0;
                        OnBulletTimeOver.Invoke();
                    }
                }
            }
        }

        #endregion
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(BulletTimeController))]
    //[CanEditMultipleObjects]

    public class CustomBulletTimeControllerInspector : Editor
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
            BulletTimeController BulletTimeController = (BulletTimeController)target;
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