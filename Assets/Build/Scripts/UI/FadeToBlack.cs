using UnityEngine;
using DP.DevTools;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// A component for fading to and from black using UnityEvents.
    /// </summary>
    public class FadeToBlack : MonoBehaviour
    {
        public static FadeToBlack Instance;
        [SerializeField] UnityEvent action, fadeToBlack;

        /// <summary>
        /// Unity callback that is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);
        }

        /// <summary>
        /// Shows the contents by invoking the "action" UnityEvent.
        /// </summary>
        public void ShowContents()
        {
            action.Invoke();
        }

        /// <summary>
        /// Hides the contents by invoking the "fadeToBlack" UnityEvent.
        /// </summary>
        public void HideContents()
        {
            fadeToBlack.Invoke();
        }

        /// <summary>
        /// Static method that shows the contents by invoking the "action" UnityEvent.
        /// </summary>
        public static void StaticShowContents()
        {
            Instance.action.Invoke();
        }

        /// <summary>
        /// Static method that hides the contents by invoking the "fadeToBlack" UnityEvent.
        /// </summary>
        public static void StaticHideContents()
        {
            Instance.fadeToBlack.Invoke();
        }
    }
}