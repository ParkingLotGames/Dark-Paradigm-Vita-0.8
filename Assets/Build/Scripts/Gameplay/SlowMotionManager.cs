using UnityEngine;
using DP.StaticDataContainers;
using DP.ScriptableEvents;
using UnityEngine.Events;

namespace DP.Gameplay
{
    /// <summary>
    /// This class manages slow motion effects in the game, and handles user inputs related to slow motion effects.
    /// </summary>
    public class SlowMotionManager : MonoBehaviour
    {
        // Public variables
        /// <summary>
        /// Determines whether slow motion effects are currently overridden.
        /// </summary>
        public bool slowMoOverriden;

        // Serialized Internal variables
        /// <summary>
        /// Determines whether toggle input is currently enabled.
        /// </summary>
        [SerializeField]
        public bool toggleInput;

        // Internal variables
        /// <summary>
        /// Indicates whether item wheel time input has been detected.
        /// </summary>
        public bool itemWheelTimeInputDetected;

        /// <summary>
        /// Indicates whether bullet time input has been detected.
        /// </summary>
        public bool bulletTimeInputDetected;

        /// <summary>
        /// Indicates whether the "item wheel time input detected" event has been raised.
        /// </summary>
        public bool enableItemWheelEventOnHoldRaised;

        /// <summary>
        /// Indicates whether the "item wheel time input released" event has been raised.
        /// </summary>
        public bool disableItemWheelEventOnHoldRaised;

        // Serialize Private variables
        /// <summary>
        /// The BulletTimeController component used to control bullet time.
        /// </summary>
        [SerializeField]
        BulletTimeController bulletTimeController;

        /// <summary>
        /// The UnityEvent invoked when item wheel time input is detected.
        /// </summary>
        [SerializeField]
        UnityEvent OnItemWheelTimeInputDetected;

        /// <summary>
        /// Indicates whether the "item wheel time input detected" event has been raised.
        /// </summary>
        bool itemWheelTimeInputDetectedRaised;

        /// <summary>
        /// The UnityEvent invoked when item wheel time input is released.
        /// </summary>
        [SerializeField]
        UnityEvent OnItemWheelTimeInputReleased;

        /// <summary>
        /// Indicates whether the "item wheel time input released" event has been raised.
        /// </summary>
        bool itemWheelTimeInputReleasedRaised;

        /// <summary>
        /// The UnityEvent invoked when bullet time input is detected.
        /// </summary>
        [SerializeField]
        UnityEvent OnBulletTimeInputDetected;

        /// <summary>
        /// Indicates whether the "bullet time input detected" event has been raised.
        /// </summary>
        bool bulletTimeInputDetectedRaised;

        /// <summary>
        /// The UnityEvent invoked when bullet time input is released.
        /// </summary>
        [SerializeField]
        UnityEvent OnBulletTimeInputReleased;

        /// <summary>
        /// Indicates whether the "bullet time input released" event has been raised.
        /// </summary>
        bool bulletTimeInputReleasedRaised;

        /// <summary>
        /// Indicates whether the "enable bullet time event on hold" has been raised.
        /// </summary>
        private bool enableBulletTimeEventOnHoldRaised;

        /// <summary>
        /// Indicates whether the "disable bullet time event on hold" has been raised.
        /// </summary>
        private bool disableBulletTimeEventOnHoldRaised;

        /// <summary>
        /// Indicates whether bullet time is currently available to the player.
        /// </summary>
        private bool bulletTimeAvailable;


        /// <summary>
        /// This method is called every frame to update slow motion effects and handle user inputs related to slow motion effects.
        /// </summary>
        private void Update()
        {
            if (!slowMoOverriden)
            {
                if (!toggleInput)
                {
                    bulletTimeInputDetected = Input.GetButton(PSVitaInputValues.Right) && !itemWheelTimeInputDetected;
                    itemWheelTimeInputDetected = Input.GetButton(PSVitaInputValues.Left) && !bulletTimeInputDetected;

                    if (itemWheelTimeInputDetected)
                    {
                        if (!enableItemWheelEventOnHoldRaised)

                            OnItemWheelTimeInputDetected.Invoke();
                        disableItemWheelEventOnHoldRaised = false;
                        enableItemWheelEventOnHoldRaised = true;

                    }
                    else
                    {
                        if (!disableItemWheelEventOnHoldRaised)
                            OnItemWheelTimeInputReleased.Invoke();
                        enableItemWheelEventOnHoldRaised = false;
                        disableItemWheelEventOnHoldRaised = true;
                    }
                    if (bulletTimeInputDetected && bulletTimeAvailable)
                    {
                        if (!enableBulletTimeEventOnHoldRaised)
                            OnBulletTimeInputDetected.Invoke();
                        disableBulletTimeEventOnHoldRaised = false;
                        enableBulletTimeEventOnHoldRaised = true;
                    }
                    else
                    {
                        if (!disableBulletTimeEventOnHoldRaised)
                            OnBulletTimeInputReleased.Invoke();
                        enableBulletTimeEventOnHoldRaised = false;
                        disableBulletTimeEventOnHoldRaised = true;
                    }
                }
                else
                {
                    bulletTimeInputDetected = bulletTimeInputDetected ? (Input.GetButtonDown(PSVitaInputValues.Right) ? false : true) : (Input.GetButtonDown(PSVitaInputValues.Right) && !itemWheelTimeInputDetected ? true : false);
                    itemWheelTimeInputDetected = itemWheelTimeInputDetected ? (Input.GetButtonDown(PSVitaInputValues.Left) ? false : true) : (Input.GetButtonDown(PSVitaInputValues.Left) && !bulletTimeInputDetected ? true : false);

                    if (itemWheelTimeInputDetected)
                    {
                        if (!itemWheelTimeInputDetectedRaised)
                            OnItemWheelTimeInputDetected.Invoke();
                        itemWheelTimeInputReleasedRaised = false;
                        itemWheelTimeInputDetectedRaised = true;
                    }
                    else
                    {
                        if (!itemWheelTimeInputReleasedRaised)
                            OnItemWheelTimeInputReleased.Invoke();
                        itemWheelTimeInputDetectedRaised = false;
                        itemWheelTimeInputReleasedRaised = true;
                    }
                    if (bulletTimeInputDetected && bulletTimeController.playerInventory.inGameInventory.bulletTimeLeft > 0)
                    {
                        if (!bulletTimeInputDetectedRaised)

                            OnBulletTimeInputDetected.Invoke();
                        bulletTimeInputReleasedRaised = false;
                        bulletTimeInputDetectedRaised = true;
                    }
                    else
                    {
                        if (!bulletTimeInputReleasedRaised)
                            OnBulletTimeInputReleased.Invoke();
                        bulletTimeInputDetectedRaised = false;
                        bulletTimeInputReleasedRaised = true;
                    }
                }
            }
        }

        /// <summary>
        /// Overrides the current slow motion effects and disables all related inputs.
        /// </summary>
        public void OverrideSlowMotion()
        {
            slowMoOverriden = true;
            itemWheelTimeInputDetected = false;
        }

        /// <summary>
        /// Removes the slow motion override and re-enables all related inputs.
        /// </summary>
        public void RemoveSlowMotionOveride()
        {
            slowMoOverriden = false;
        }

        /// <summary>
        /// Sets bullet time as available for the player.
        /// </summary>
        public void SetBulletTimeAsAvailable()
        {
            bulletTimeAvailable = true;
        }

        /// <summary>
        /// Sets bullet time as not available for the player.
        /// </summary>
        public void SetBulletTimeAsNotAvailable()
        {
            bulletTimeAvailable = false;
        }
    }
}