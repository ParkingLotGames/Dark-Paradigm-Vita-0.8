using UnityEngine;
using UnityEngine.Events;

namespace DP.Gameplay
{
    /// <summary>
    /// Listens for a game start request triggered by any key input and invokes an event when detected.
    /// </summary>
    public class ListenForGameStart : MonoBehaviour
    {
        /// <summary>
        /// Unity event to be triggered when a game start request is detected.
        /// </summary>
        [SerializeField] UnityEvent OnStartGameRequested;

        /// <summary>
        /// Checks for key input and invokes the game start event if any key is detected.
        /// </summary>
        private void Update()
        {
            if (Input.anyKeyDown)
            {
                OnStartGameRequested.Invoke();
            }
        }
    }

}