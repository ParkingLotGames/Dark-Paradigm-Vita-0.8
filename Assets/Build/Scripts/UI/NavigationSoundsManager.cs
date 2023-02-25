using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Handles the management and playing of navigation sounds, including navigation, confirmation, and cancellation sounds.
    /// </summary>
    public class NavigationSoundsManager : MonoBehaviour
    {
        /// <summary>
        /// The single instance of the NavigationSoundsManager class.
        /// </summary>
        public static NavigationSoundsManager Instance;

        /// <summary>
        /// The audio clip used for the navigation sound effect.
        /// </summary>
        [SerializeField] AudioClip navigationSound;

        /// <summary>
        /// The audio clip used for the confirmation sound effect.
        /// </summary>
        [SerializeField] AudioClip confirmSound;

        /// <summary>
        /// The audio clip used for the cancellation sound effect.
        /// </summary>
        [SerializeField] AudioClip cancelSound;

        /// <summary>
        /// The audio source used to play the sound effects.
        /// </summary>
        public AudioSource audioSource;

        /// <summary>
        /// Mutes the sound effects temporarily.
        /// </summary>
        public void MuteSound()
        {
                audioSource.volume = 0;
                Invoke("RestoreVolume", .14f);
        }
        /// <summary>
        /// Called when the object is initialized.
        /// </summary>
        void Awake()
        {
            Singleton();
            audioSource = GetComponent<AudioSource>();
                audioSource.volume = 0;
                Invoke("RestoreVolume", .14f);
        }
        /// <summary>
        /// Ensures that there is only one instance of the NavigationSoundsManager class.
        /// </summary>
        void Singleton()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        /// <summary>
        /// Restores the volume of the sound effects to their default level.
        /// </summary>
        void RestoreVolume() { audioSource.volume = 0.2f; }
        /// <summary>
        /// Plays the navigation sound effect.
        /// </summary>
        public void PlayNavigation() { audioSource.PlayOneShot(navigationSound); }
        /// <summary>
        /// Plays the confirmation sound effect.
        /// </summary>
        public void PlayConfirm() { audioSource.PlayOneShot(confirmSound); }
        /// <summary>
        /// Plays the cancellation sound effect.
        /// </summary>
        public void PlayCancel() { audioSource.PlayOneShot(cancelSound); }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(NavigationSoundsManager))]
    [CanEditMultipleObjects]

    public class CustomNavigationSoundsManagerInspector : Editor
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
            NavigationSoundsManager NavigationSoundsManager = (NavigationSoundsManager)target;
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