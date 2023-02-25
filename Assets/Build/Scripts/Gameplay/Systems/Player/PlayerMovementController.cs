//using DP.Gameplay;
using DP.StaticDataContainers;
using UnityEngine;
using UnityEngine.Events;
//using NavJob.Components;

namespace DP.Controllers
{
    [RequireComponent(typeof(CharacterController))]
    /// <summary>
    /// Controls the movement of the player character, including walking, running, and jumping.
    /// </summary>
    public class PlayerMovementController : MonoBehaviour
    {
        /// <summary>
        /// The walking speed of the player.
        /// </summary>
        [SerializeField] float walkingSpeed = 5.5f;

        /// <summary>
        /// The original walking speed of the player, used to reset the walking speed after running.
        /// </summary>
        float originalWalkingSpeed;

        /// <summary>
        /// The running speed of the player.
        /// </summary>
        [SerializeField] float runningSpeed = 8f;

        /// <summary>
        /// The original running speed of the player, used to reset the running speed after running.
        /// </summary>
        float originalRunningSpeed;

        /// <summary>
        /// The speed at which the player jumps.
        /// </summary>
        [SerializeField] float jumpSpeed = 8.0f;

        /// <summary>
        /// The strength of gravity affecting the player.
        /// </summary>
        [SerializeField] float gravity = 20.0f;

        /// <summary>
        /// The player's current stamina.
        /// </summary>
        [SerializeField] public float currentStamina;

        /// <summary>
        /// The maximum amount of stamina the player can have.
        /// </summary>
        [SerializeField] public float maxStamina = 15;

        /// <summary>
        /// Determines whether the player can move.
        /// </summary>
        [SerializeField] public bool canMove;

        /// <summary>
        /// Determines whether the player is running.
        /// </summary>
        public bool isRunning;

        /// <summary>
        /// Determines whether the player is moving.
        /// </summary>
        public bool isMoving;

        /// <summary>
        /// Determines whether the player can run.
        /// </summary>
        public bool canRun;

        /// <summary>
        /// The current forward speed of the player.
        /// </summary>
        float currentForwSpeed;

        /// <summary>
        /// The current side speed of the player.
        /// </summary>
        float currentSideSpeed;

        /// <summary>
        /// Event that gets triggered when the stamina bar is updated.
        /// </summary>
        [SerializeField] UnityEvent UpdateStaminaBar;

        /// <summary>
        /// Event that gets triggered when the player starts moving.
        /// </summary>
        [SerializeField] UnityEvent OnMoving;

        /// <summary>
        /// Event that gets triggered when the player stops moving.
        /// </summary>
        [SerializeField] UnityEvent OnStopMoving;

        /// <summary>
        /// Reference to the character controller component.
        /// </summary>
        [HideInInspector] public CharacterController characterController;

        /// <summary>
        /// The movement direction of the player.
        /// </summary>
        Vector3 moveDirection = Vector3.zero;

        /// <summary>
        /// The forward vector of the player.
        /// </summary>
        Vector3 forward;

        /// <summary>
        /// The right vector of the player.
        /// </summary>
        Vector3 right;

        /// <summary>
        /// The movement direction along the y axis.
        /// </summary>
        float movementDirectionY;

        /// <summary>
        /// The transform component of the player.
        /// </summary>
        [HideInInspector] public Transform playerTransform;

        /// <summary>
        /// The horizontal input axis of the player.
        /// </summary>
        float lsh;

        /// <summary>
        /// The vertical input axis of the player.
        /// </summary>
        float lsv;
        /// <summary>
        /// Determines whether or not the player's stamina is boosted
        /// </summary>
        public bool staminaBoost;
        /// <summary>
        /// Determines whether or not the player is currently reloading their stamina
        /// </summary>
        public bool reloadingStamina;
        /// <summary>
        /// Determines whether or not the player is currently shooting.
        /// </summary>
        private bool isShooting;
        /// <summary>
        /// Determines whether or not the player is currently aiming.
        /// </summary>
        private bool isAiming;

        public UnityEvent OnRunning, OnStoppedRunning;
        /// <summary>
        /// Resets the player's current stamina to 0.
        /// </summary>
        public void ResetStamina() { currentStamina = 0; }


        /// <summary>
        /// Gets necessary references for the script and initializes some values.
        /// </summary>
        private void Awake()
        {
            GetReferences();
            SetOriginalSpeeds();
        }

        private void Update()
        {
            StaminaManagement();
            AllowMovement();
            InputManagement();
        }



        /// <summary>
        /// Handles updating the player's stamina value based on certain conditions, as well as updating the stamina UI.
        /// </summary>
        private void StaminaManagement()
        {
            if (canMove)
            {
                if (!isRunning)
                {
                    if (!staminaBoost)
                    {
                        if (currentStamina < maxStamina)
                        {
                            currentStamina += Mathf.Clamp(2.35f * Time.fixedDeltaTime, 0, maxStamina);
                            reloadingStamina = true;
                            UpdateStaminaBar.Invoke();
                        }
                        else
                        {
                            reloadingStamina = false;
                        }
                    }
                    else
                    {
                        if (currentStamina < maxStamina)
                        {
                            currentStamina += Mathf.Clamp(8f * Time.fixedDeltaTime, 0, maxStamina);
                            reloadingStamina = true;
                            UpdateStaminaBar.Invoke();
                        }
                        else
                        {
                            reloadingStamina = false;
                        }
                    }
                }
                else
                {
                    UpdateStaminaBar.Invoke();
                    reloadingStamina = false;
                }
            }
        }

        /// <summary>
        /// Enables the player's movement.
        /// </summary>
        public void EnableMovement()
        {
            canMove = true;
        }

        /// <summary>
        /// Disables the player's movement.
        /// </summary>
        public void DisableMovement()
        {
            canMove = false;
        }

        /// <summary>
        /// Gets references to the player's character controller and transform.
        /// </summary>
        private void GetReferences()
        {
            characterController = GetComponent<CharacterController>();
            playerTransform = transform;
        }

        /// <summary>
        /// Stores the original walking and running speeds for the player.
        /// </summary>
        private void SetOriginalSpeeds()
        {
            originalWalkingSpeed = walkingSpeed;
            originalRunningSpeed = runningSpeed;
        }

        /// <summary>
        /// Handles input management for the player.
        /// </summary>
        private void InputManagement()
        {
            Movement();
            Run();
        }

        /// <summary>
        /// Handles allowing the player to run and toggling the player's running state.
        /// </summary>
        private void Run()
        {
            AllowRun();
            ToggleRun();
        }

        /// <summary>
        /// Determines whether or not the player can run based on certain conditions.
        /// </summary>
        private void AllowRun()
        {
            //canRun: if can move true only if going forward, else can't run
#if UNITY_ANDROID
            canRun = canMove ?currentStamina > 4.2 && CF2Input.GetAxis(PSVitaInputValues.LSVertical) > 0 : false;
#endif
#if UNITY_PSP2
            canRun = (canMove ? (reloadingStamina ? currentStamina > 4.2 && Input.GetAxis(PSVitaInputValues.LSVertical) > 0 : currentStamina > 0 && Input.GetAxis(PSVitaInputValues.LSVertical) > 0) : false);
#endif
#if UNITY_PS4
            canRun = canMove ?currentStamina > 4.2 && Input.GetAxis(PS4InputValues.LSVertical1) > 0 : false;
#endif
#if UNITY_STANDALONE || UNITY_EDITOR
            canRun = (canMove ? (reloadingStamina ? currentStamina > 4.2 && Input.GetAxis("Vertical") > 0 : currentStamina > 0 && Input.GetAxis("Vertical") > 0) : false);
#endif
        }


        /// <summary>
        /// Toggles the player's running state based on input.
        /// </summary>
        private void ToggleRun()
        {
            if (canRun)
            {
                if (isRunning)
                { OnRunning.Invoke(); }
                else
                {
                    OnStoppedRunning.Invoke();
                }
                // Is Running: if running and you press the key it's false, else is true, if not running and you press the key it's true, else is false.
#if UNITY_ANDROID
                isRunning = !isShooting && !isAiming ? (isRunning ? (CF2Input.GetButtonDown(PSVitaInputValues.Down) ? false : true) : (CF2Input.GetButtonDown(PSVitaInputValues.Down) ? true : false)) : false;
#endif
#if UNITY_PSP2
                //OnShoot, OnAim
                isRunning = !isShooting && !isAiming ? (isRunning && currentForwSpeed >= walkingSpeed ? (Input.GetButtonDown(PSVitaInputValues.Down) ? false : true) : (Input.GetButtonDown(PSVitaInputValues.Down) ? true : false)) : false;
#endif
#if UNITY_PS4
                isRunning = !isShooting && !isAiming ? (isRunning ? (Input.GetButtonDown(PS4InputValues.L3p1) ? false : true) : (Input.GetButtonDown(PS4InputValues.L3p1) ? true : false)) : false;
#endif
#if UNITY_STANDALONE || UNITY_EDITOR
                isRunning = !isShooting && !isAiming ? (isRunning && currentForwSpeed >= walkingSpeed ? (Input.GetKeyDown(KeyCode.LeftShift) ? false : true) : (Input.GetKeyDown(KeyCode.LeftShift) ? true : false)) : false;
#endif
                float lastValue = currentStamina;
                float elapsed = 0;
                elapsed += Time.fixedDeltaTime;
                if (isRunning)
                    currentStamina -= elapsed;
                else
                    elapsed = 0;
            }
            else
            {
                isRunning = false;
                OnStoppedRunning.Invoke();

            }
        }


        /// <summary>
        /// Handles the player's movement based on input and other factors.
        /// </summary>
        private void Movement()
        {
            forward = transform.TransformDirection(Vector3.forward);
            right = transform.TransformDirection(Vector3.right);
#if UNITY_ANDROID
            currentForwSpeed = canMove ? (isRunning ? runningSpeed : walkingSpeed) * CF2Input.GetAxis(PSVitaInputValues.LSVertical) : 0;
            currentSideSpeed = canMove ? (isRunning ? runningSpeed : walkingSpeed) * CF2Input.GetAxis(PSVitaInputValues.LSHorizontal) : 0;
            isMoving = canMove ? (CF2Input.GetAxis(PSVitaInputValues.LSHorizontal) != 0 || CF2Input.GetAxis(PSVitaInputValues.LSVertical) != 0 ? true : false) : false;
            lsh = CF2Input.GetAxis(PSVitaInputValues.LSHorizontal);
            lsv = CF2Input.GetAxis(PSVitaInputValues.LSVertical);
            
#endif
#if UNITY_PSP2
            currentForwSpeed = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis(PSVitaInputValues.LSVertical) : 0;
            currentSideSpeed = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis(PSVitaInputValues.LSHorizontal) : 0;
            isMoving = canMove ? (Input.GetAxis(PSVitaInputValues.LSHorizontal) != 0 || Input.GetAxis(PSVitaInputValues.LSVertical) != 0 ? true : false) : false;
#endif
#if UNITY_PS4
            currentForwSpeed = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis(PS4InputValues.LSVertical1) : 0;
            currentSideSpeed = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis(PS4InputValues.LSHorizontal1) : 0;
            isMoving = canMove ? (Input.GetAxis(PS4InputValues.LSHorizontal1) != 0 || Input.GetAxis(PS4InputValues.LSVertical1) != 0 ? true : false) : false;
#endif
#if UNITY_STANDALONE || UNITY_EDITOR

            currentForwSpeed = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
            currentSideSpeed = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
            isMoving = canMove ? (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 ? true : false) : false;
#endif
            if (isMoving)
                OnMoving.Invoke();
            else
                OnStopMoving.Invoke();
            movementDirectionY = moveDirection.y;
            moveDirection = (forward * currentForwSpeed) + (right * currentSideSpeed);

            //Jump
#if UNITY_ANDROID
            moveDirection.y = CF2Input.GetButtonDown(PSVitaInputValues.Cross) && canMove && characterController.isGrounded ? jumpSpeed : movementDirectionY;
#endif
#if UNITY_PSP2
            moveDirection.y = Input.GetButtonDown(PSVitaInputValues.Cross) && canMove && characterController.isGrounded ? jumpSpeed : movementDirectionY;
#endif
#if UNITY_PS4
            moveDirection.y = Input.GetButtonDown(PS4InputValues.Cross1) && canMove && characterController.isGrounded ? jumpSpeed : movementDirectionY;
#endif

#if UNITY_STANDALONE || UNITY_EDITOR
            moveDirection.y = Input.GetKeyDown(KeyCode.Space) && canMove && characterController.isGrounded ? jumpSpeed : movementDirectionY;
#endif


            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            characterController.Move(moveDirection * Time.deltaTime);
        }

        /// <summary>
        /// Handles allowing the player to move based on certain conditions.
        /// </summary>
        private void AllowMovement()
        {
            // we can move if we are grounded & the game isn't over/paused/waiting to start
        }


        /// <summary>
        /// Sets isShooting to true.
        /// </summary>
        public void SetIsShootingTrue() { isShooting = true; }
        /// <summary>
        /// Sets isShooting to false.
        /// </summary>
        public void SetIsShootingFalse() { isShooting = false; }

        /// <summary>
        /// Sets isAiming to true.
        /// </summary>
        public void SetIsAimingTrue() { isAiming = true; }

        /// <summary>
        /// Sets isAiming to false.
        /// </summary>
        public void SetIsAimingFalse() { isAiming = false; }

    }
}