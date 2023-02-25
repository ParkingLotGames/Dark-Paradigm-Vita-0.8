using UnityEngine;
using DP.Gameplay;
using DP.StaticDataContainers;
using DP.Controllers;
#if UNITY_ANDROID
using ControlFreak2;
#endif
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DP.UI
{
    /// <summary>
    /// Class that animates the camera's field of view (FOV) based on the player's actions, such as aiming or running.
    /// </summary>
    public class CameraFOVAnimation : MonoBehaviour
    {
        /// <summary>
        /// The reduction in FOV when aiming down sights (ADS). Range is from -60 to -10 degrees.
        /// </summary>
        [SerializeField] [Range(-60, -10)] public float FOVAimingReduction;

        /// <summary>
        /// The speed at which the FOV changes when aiming.
        /// </summary>
        [SerializeField] public float FOVChangeAimingSpeed;

        /// <summary>
        /// The amount that the FOV expands when the player is running.
        /// </summary>
        [SerializeField] public float FOVRunningExpansion;

        /// <summary>
        /// The speed at which the FOV changes when running.
        /// </summary>
        [SerializeField] public float FOVChangeRunningSpeed;

        /// <summary>
        /// An array of all the cameras that are being used for this animation.
        /// </summary>
        Camera[] cameras;

        /// <summary>
        /// The field of view (FOV) when the player is not aiming or running.
        /// </summary>
        float hipFOV;

        /// <summary>
        /// The field of view (FOV) when the player is aiming down sights (ADS).
        /// </summary>
        float ADS_FOV;

        /// <summary>
        /// The field of view (FOV) when the player is running.
        /// </summary>
        float runningFOV;

        /// <summary>
        /// The current field of view (FOV) of the camera.
        /// </summary>
        float currentFOV;

        /// <summary>
        /// A flag indicating whether the camera has moved to the aim position.
        /// </summary>
        bool movedToAim;

        /// <summary>
        /// A flag indicating whether the camera has moved to the hip position.
        /// </summary>
        bool movedToHip;

        /// <summary>
        /// A flag indicating whether the camera has moved to the run position.
        /// </summary>
        bool movedToRun;

        /// <summary>
        /// A flag indicating whether the player was running in the previous frame.
        /// </summary>
        bool wasRunning;

        /// <summary>
        /// A flag indicating whether the player is aiming.
        /// </summary>
        bool aiming;

        /// <summary>
        /// A flag indicating whether the player is hip firing.
        /// </summary>
        bool hipFire;

        /// <summary>
        /// A flag indicating whether the player is running.
        /// </summary>
        bool running;

        /// <summary>
        /// Gets all the cameras that are being used for this animation and sets their initial FOV values.
        /// </summary>
        private void Awake()
        {
            cameras = GetComponentsInChildren<Camera>();
            int camerasCount = cameras.Length;
            for (int i = 0; i < camerasCount; i++)
            {
                currentFOV = cameras[i].fieldOfView;
                hipFOV = currentFOV;
                ADS_FOV = hipFOV + FOVAimingReduction;
                runningFOV = hipFOV + FOVRunningExpansion;
            }
        }

        /// <summary>
        /// Sets the aiming flag to true.
        /// </summary>
        public void SetAimingTrue() { aiming = true; }

        /// <summary>
        /// Sets
#if UNITY_EDITOR
        [CustomEditor(typeof(CameraFOVAnimation))]
        public class CustomCameraADSAnimationInspector : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
            }
        }
#endif
    }
}