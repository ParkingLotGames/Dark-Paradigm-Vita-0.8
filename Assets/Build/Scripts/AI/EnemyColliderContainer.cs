using UnityEngine;

namespace DP.AI
{
    /// <summary>
    /// Represents a container for enemy colliders.
    /// </summary>
    public class EnemyColliderContainer : MonoBehaviour
    {
        private SphereCollider[] colliders;
        private int collidersQty;

        /// <summary>
        /// Initializes the container by obtaining all the child sphere colliders.
        /// </summary>
        private void Awake()
        {
            colliders = GetComponentsInChildren<SphereCollider>();
            collidersQty = colliders.Length;
        }

        /// <summary>
        /// Disables all the colliders in the container.
        /// </summary>
        public void DisableColliders()
        {
            for (int i = 0; i < collidersQty; i++)
            {
                colliders[i].enabled = false;
            }
        }

        /// <summary>
        /// Enables all the colliders in the container.
        /// </summary>
        public void EnableColliders()
        {
            for (int i = 0; i < collidersQty; i++)
            {
                colliders[i].enabled = true;
            }
        }
    }
}