#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using DP.Gameplay;

namespace DP.Controllers
{
    /// <summary>
    /// Class that handles the animation controller for the weapon
    /// </summary>
    public class WeaponAnimationController : MonoBehaviour
    {
        private Animator weaponAnimator;
        private bool getGun, hideGun, isAiming, isShooting, isReloading;
        private WeaponController weaponController;

        /// <summary>
        /// Gets the WeaponController component and the animator component in the children of this object.
        /// </summary>
        private void Awake()
        {
            weaponController = GetComponent<WeaponController>();
            GetAnimator();
        }

        /// <summary>
        /// Updates the animator boolean parameters based on the values of the WeaponController's boolean parameters.
        /// </summary>
        private void Update()
        {
            GetWeaponBools();
            weaponAnimator.SetBool("GetGun", getGun);
            weaponAnimator.SetBool("HideGun", hideGun);
            weaponAnimator.SetBool("isAiming", isAiming);
            weaponAnimator.SetBool("isShooting", isShooting);
            weaponAnimator.SetBool("isReloading", isReloading);
        }

        /// <summary>
        /// Gets the Animator component in the children of this object.
        /// </summary>
        private void GetAnimator()
        {
            weaponAnimator = GetComponentInChildren<Animator>();
        }

        /// <summary>
        /// Gets the boolean parameters from the WeaponController component.
        /// </summary>
        private void GetWeaponBools()
        {
            getGun = weaponController.getGun;
            hideGun = weaponController.hideGun;
            isAiming = weaponController.isAiming;
            isShooting = weaponController.isShooting;
            isReloading = weaponController.isReloading;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(WeaponAnimationController))]
	public class CustomWeaponAnimationControllerInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
}