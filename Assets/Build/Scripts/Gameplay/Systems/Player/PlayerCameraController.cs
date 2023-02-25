using System;
using UnityEngine;
using DP.StaticDataContainers;

// TODO: Maybe this was being worked on? Seem sweird that almost nothing is called.


namespace DP.Controllers
{
    /// <summary>
    /// Controls the player's camera and handles its movement and rotation.
    /// </summary>
    public class PlayerCameraController : MonoBehaviour
    {
        public static PlayerCameraController Instance;
        [SerializeField] Transform TPSPlayerModel;
        [SerializeField][Range(1,10)] float androidLookSpeed = 5.0f;
        [SerializeField][Range(1,10)] float vitaLookSpeed = 5f;
        [SerializeField][Range(1,10)] float PS4LookSpeed = 5f;
        [SerializeField][Range(1,10)] float standaloneLookSpeed = 5f;
        float lookXLimit = 60.0f;
        public Camera playerCamera;
        public bool inverseY;

        float verticalRotation = 0;
        public bool isLookRotationEnabled = true;//;
        
        private void Awake()
        {
            Singleton();
            playerCamera = GetComponentInChildren<Camera>();
#if UNITY_STANDALONE
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
#endif
        }
        /// <summary>
        /// Enables camera movement.
        /// </summary>
        public void EnableCameraMovement() { isLookRotationEnabled = true; }
        /// <summary>
        /// Disables camera movement.
        /// </summary>
        public void DisableCameraMovement() { isLookRotationEnabled = false; }
        /// <summary>
        /// Ensures that only one instance of PlayerCameraController exists.
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
        /// Updates the rotation of the camera and player model.
        /// </summary>
        private void Update()
        {
            AllowRotation();
            //RotateCameraAndPlayer();
        }

        private void AllowRotation()
        {
            ////
            //            canLookAround = Management.GameManager.Instance.gameState == Management.GameState.Playing;
        }
        /// <summary>
        /// Rotates the camera and player based on user input and look speed.
        /// </summary>
        public void RotateCameraAndModel()
        {
            if (isLookRotationEnabled)
            {
                Vector3 playerDirection = Vector3.right * Input.GetAxisRaw(PSVitaInputValues.RSHorizontal) + Vector3.forward * Input.GetAxisRaw(PSVitaInputValues.RSVertical);
                if (playerDirection.sqrMagnitude > 0.0f)
                    TPSPlayerModel.transform.localRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
        }
        /// <summary>
        /// Rotates the camera and player model based on user input.
        /// </summary>
        public void RotateCameraAndPlayer()
        {
            if (isLookRotationEnabled)
            {
                if (inverseY)
                {
#if UNITY_ANDROID
                    verticalRotation = CF2Input.GetAxis(PSVitaInputValues.RSVertical) * (androidLookSpeed * 10) * Time.fixedDeltaTime;
#endif
#if UNITY_PSP2
                    verticalRotation = Input.GetAxis(PSVitaInputValues.RSVertical) * (vitaLookSpeed * 100) * Time.fixedDeltaTime;
#endif
#if UNITY_PS4
                    verticalRotation = Input.GetAxis(PS4InputValues.RSVertical1) * (PS4LookSpeed * 25) * Time.fixedDeltaTime;
#endif
#if UNITY_STANDALONE || UNITY_EDITOR
                    verticalRotation = Input.GetAxis("Mouse Y") * (standaloneLookSpeed * 10) * Time.fixedDeltaTime;
#endif
                }
                else
                {
#if UNITY_ANDROID
                    verticalRotation -= CF2Input.GetAxis(PSVitaInputValues.RSVertical) * (androidLookSpeed * 10) * Time.fixedDeltaTime;
#endif
#if UNITY_PSP2
                    verticalRotation -= Input.GetAxis(PSVitaInputValues.RSVertical) * (vitaLookSpeed * 100) * Time.fixedDeltaTime;
#endif
#if UNITY_PS4
                    verticalRotation -= Input.GetAxis(PS4InputValues.RSVertical1) * (PS4LookSpeed * 25) * Time.fixedDeltaTime;
#endif
#if UNITY_STANDALONE || UNITY_EDITOR
                    verticalRotation -= Input.GetAxis("Mouse Y") * (standaloneLookSpeed * 10) * Time.fixedDeltaTime;
#endif
                }
                verticalRotation = Mathf.Clamp(verticalRotation, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
#if UNITY_ANDROID
                    transform.rotation *= Quaternion.Euler(0, CF2Input.GetAxis(PSVitaInputValues.RSHorizontal) * (androidLookSpeed * 10) * Time.fixedDeltaTime, 0);
#endif
#if UNITY_PSP2
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis(PSVitaInputValues.RSHorizontal) * (vitaLookSpeed * 100) * Time.fixedDeltaTime, 0);
#endif
#if UNITY_PS4
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis(PS4InputValues.RSHorizontal1) * (PS4LookSpeed * 10) * Time.fixedDeltaTime, 0);
#endif
#if UNITY_STANDALONE || UNITY_EDITOR
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * (standaloneLookSpeed * 10) * Time.fixedDeltaTime, 0);
#endif
            }
        }

    }
}