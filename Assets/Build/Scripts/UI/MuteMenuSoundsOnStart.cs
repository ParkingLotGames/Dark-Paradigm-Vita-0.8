using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Mutes menu sounds when the game starts.
    /// </summary>
    public class MuteMenuSoundsOnStart : MonoBehaviour
    {
        /// <summary>
        /// The singleton instance of this class.
        /// </summary>
        public static MuteMenuSoundsOnStart Instance;

        /// <summary>
        /// Called when the object is initialized.
        /// </summary>
        void Start()
        {
            // Mutes menu sounds using the NavigationSoundsManager.
            NavigationSoundsManager.Instance.MuteSound();
        }
    }
}