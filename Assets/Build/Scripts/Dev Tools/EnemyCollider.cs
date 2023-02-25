using UnityEngine;

namespace DP.DevTools
{
    /// <summary>
    /// This class provides methods for retrieving BoxCollider components attached to the same GameObject as this script.
    /// </summary>
    public class EnemyCollider : MonoBehaviour
    {
        BoxCollider[] foundColliders;

        /// <summary>
        /// Retrieves an array of BoxCollider components attached to the same GameObject as this script.
        /// </summary>
        /// <returns>An array of BoxCollider components.</returns>
        public BoxCollider[] hitColliders()
        {
            foundColliders = GetComponents<BoxCollider>();
            return foundColliders;
        }
    }
}