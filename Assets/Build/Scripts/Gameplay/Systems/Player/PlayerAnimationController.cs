using UnityEngine;
using DP.Controllers;
using DP.DevTools;
using DP.Gameplay;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DP.Controllers
{
    /// <summary>
    /// Controls the animations of the player character in the game.
    /// </summary>
    public class PlayerAnimationController : MonoBehaviour
    {
        Animator playerAnimation;
        CharacterController characterController;
        bool isWalking, isRunning, isGrounded;

        /// <summary>
        /// Sets the isWalking boolean value to true.
        /// </summary>
        public void SetIsWalkingTrue() { isWalking = true; }

        /// <summary>
        /// Sets the isWalking boolean value to false.
        /// </summary>
        public void SetIsWalkingFalse() { isWalking = false; }

        /// <summary>
        /// Sets the isRunning boolean value to true.
        /// </summary>
        public void SetIsRunningTrue() { isRunning = true; }

        /// <summary>
        /// Sets the isRunning boolean value to false.
        /// </summary>
        public void SetIsRUningFalse() { isRunning = false; }

        #region Unity Callbacks

        /// <summary>
        /// Gets the Animator and CharacterController components when the script is initialized.
        /// </summary>
        private void Awake()
        {
            GetAnimator();
        }

        /// <summary>
        /// Sends the appropriate boolean values to the Animator every frame.
        /// </summary>
        private void Update()
        {
            BoolsToAnimator();
        }

        #endregion

        /// <summary>
        /// Gets the Animator and CharacterController components from the game object.
        /// </summary>
        private void GetAnimator()
        {
            playerAnimation = GetComponentInChildren<Animator>();
            characterController = GetComponent<CharacterController>();
        }

        /// <summary>
        /// Sends the isWalking, isRunning, and isGrounded boolean values to the Animator.
        /// </summary>
        private void BoolsToAnimator()
        {
            isGrounded = characterController.isGrounded;
            playerAnimation.SetBool("isWalking", isWalking);
            playerAnimation.SetBool("isRunning", isRunning);
            playerAnimation.SetBool("isGrounded", isGrounded);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(PlayerAnimationController))]
	public class CustomPlayerAnimationControllerInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
}