using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DP.Gameplay
{
    [System.Serializable]
    /// <summary>
    /// Class for managing the sounds associated with a weapon.
    /// </summary>
    public class WeaponSounds
    {
        /// <summary>
        /// The sounds to play when the weapon is being reloaded.
        /// </summary>
        public AudioClip[] reloadSounds;

        /// <summary>
        /// The sounds to play when the weapon is fired.
        /// </summary>
        public AudioClip[] shootSounds;

        /// <summary>
        /// The sounds to play when the weapon runs out of ammo.
        /// </summary>
        public AudioClip[] emptyMagSounds;
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(WeaponSounds))]
	public class CustomWeapon_VFXInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
}