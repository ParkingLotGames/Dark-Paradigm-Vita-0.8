using UnityEngine;
using DP.DevTools;
using DP.ResourceManagement;
using DP.Management;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DP
{
    ///<summary>
    /// This class manages the audio in the menu scene and ensures that only one instance of the audio exists.
    ///</summary>
    public class MenuAudioManager : MonoBehaviour
    {
        GameObject menuAudioPrefab;
        public static MenuAudioManager Instance;

        ///<summary>
        /// Called when the object is initialized.
        ///</summary>
        void Awake()
        {
            Singleton();
        }

        ///<summary>
        /// Called when a new level is loaded.
        ///</summary>
        ///<param name="level">The index of the level that was loaded.</param>
        private void OnLevelWasLoaded(int level)
        {
            ManageAudio();
        }

        ///<summary>
        /// Manages the audio in the menu scene by ensuring that only one instance of the audio exists.
        ///</summary>
        private void ManageAudio()
        {
            if (!GameManager.Instance)
            {
                if (menuAudioPrefab == null)
                {
                    menuAudioPrefab = Instantiate((GameObject)Resources.Load(ResourcesPathContainer.menuAudio));
                    DontDestroyOnLoad(menuAudioPrefab);
                }
                else
                {
                    Destroy(menuAudioPrefab);
                }
            }
            else
            {
                Destroy(menuAudioPrefab);
            }
        }

        ///<summary>
        /// Ensures that only one instance of the MenuAudioManager exists.
        ///</summary>
        void Singleton()
        {
            if (!Instance)
            {
                Instance = this;
                ManageAudio();
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}