using UnityEngine;
using DP.DevTools;
using DP.ScriptableObjects;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.Controllers
{
    [RequireComponent(typeof(AudioSource))]
    /// <summary>
    /// Handles playing footstep sounds for the player based on their movement.
    /// </summary>
    public class PlayerFootstepSoundsSystem : MonoBehaviour
    {
        /// <summary>
        /// The footstep sounds container used to play footstep sounds.
        /// </summary>
        [SerializeField] FootstepSoundsContainer footstepSsoundsContainer;

        /// <summary>
        /// True if the player is currently walking, false otherwise.
        /// </summary>
        bool isWalking;

        /// <summary>
        /// True if the player is currently running, false otherwise.
        /// </summary>
        bool isRunning;

        /// <summary>
        /// The elapsed time since the last footstep sound was played.
        /// </summary>
        float elapsed;

        /// <summary>
        /// The cadence for playing footstep sounds while walking.
        /// </summary>
        [SerializeField] float walkingCadence;

        /// <summary>
        /// The cadence for playing footstep sounds while running.
        /// </summary>
        [SerializeField] float runningCadence;

        /// <summary>
        /// The length of the array of footstep sounds in the footstep sounds container.
        /// </summary>
        int footstepSoundsLength;

        /// <summary>
        /// The audio source used to play footstep sounds.
        /// </summary>
        AudioSource footstepsAudioSource;

        /// <summary>
        /// Initializes the footstep sounds system.
        /// </summary>
        void Awake()
        {
            footstepSoundsLength = footstepSsoundsContainer.footstepSounds.Length - 1;
            footstepsAudioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Sets the player's running state to true.
        /// </summary>
        public void IsRunningTrue() { isRunning = true; }

        /// <summary>
        /// Sets the player's running state to false.
        /// </summary>
        public void IsRunningFalse() { isRunning = false; }

        /// <summary>
        /// Sets the player's walking state to true.
        /// </summary>
        public void IsWalkingTrue() { isWalking = true; }

        /// <summary>
        /// Sets the player's walking state to false.
        /// </summary>
        public void IsWalkingFalse() { isWalking = false; }

        /// <summary>
        /// Called every frame to check the player's movement and play footstep sounds as appropriate.
        /// </summary>
        private void Update()
        {
            if (isWalking && !isRunning)
            {
                isRunning = false;
                elapsed += Time.deltaTime;
                if (elapsed >= walkingCadence)
                {
                    footstepsAudioSource.PlayOneShot(footstepSsoundsContainer.footstepSounds[Random.Range(0, footstepSoundsLength)]);
                    elapsed = 0;
                }
            }
            if (!isWalking && !isRunning)
                elapsed = 0;

            if (isRunning)
            {
                isWalking = false;
                elapsed += Time.deltaTime;
                if (elapsed >= runningCadence)
                {
                    footstepsAudioSource.PlayOneShot(footstepSsoundsContainer.footstepSounds[Random.Range(0, footstepSoundsLength)]);
                    elapsed = 0;
                }
            }
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(PlayerFootstepSoundsSystem))]
    //[CanEditMultipleObjects]

    public class CustomPlayerFootstepSoundsSystemInspector : Editor
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
            PlayerFootstepSoundsSystem PlayerFootstepSoundsSystem = (PlayerFootstepSoundsSystem)target;
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