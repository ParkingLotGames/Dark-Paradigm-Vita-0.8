using UnityEngine;
using DP.Management;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DP.Gameplay
{
    /// <summary>
    /// Controls the camera switching functionality for the player. Switches between the in-game view and the photo mode view by enabling/disabling the appropriate cameras.
    /// </summary>
    public class PlayerCameraSwitch : MonoBehaviour
    {
        [SerializeField] Camera[] gameCameras, photoModeCameras;
        int gameCamerasLength, photoModeCamerasLength;
        public static PlayerCameraSwitch Instance;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            // Initialize the camera arrays and set the lengths of each array
            gameCamerasLength = gameCameras.Length;
            photoModeCamerasLength = photoModeCameras.Length;

            // If there is no instance of this class, make this instance the singleton instance
            if (!Instance)
            {
                Instance = this;
            }
            // Otherwise, destroy this instance because there should only be one instance of this class
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Sets the in-game view by enabling all game cameras and disabling all photo mode cameras.
        /// </summary>
        public void SetInGameView()
        {
            for (int i = 0; i < gameCamerasLength; i++)
            {
                gameCameras[i].enabled = true;
            }
            for (int i = 0; i < photoModeCamerasLength; i++)
            {
                photoModeCameras[i].enabled = false;
            }
        }

        /// <summary>
        /// Sets the photo mode view by enabling all photo mode cameras and disabling all game cameras.
        /// </summary>
        public void SetPhotoModeView()
        {
            for (int i = 0; i < photoModeCamerasLength; i++)
            {
                photoModeCameras[i].enabled = true;
            }
            for (int i = 0; i < gameCamerasLength; i++)
            {
                gameCameras[i].enabled = false;
            }
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(PlayerCameraSwitch))]
	public class CustomPlayerCameraSwitchInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
}