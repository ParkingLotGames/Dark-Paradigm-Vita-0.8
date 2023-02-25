using UnityEngine;

namespace DP.DevTools
{
    /// <summary>
    /// This class represents the boundary collider for an enemy object. 
    /// </summary>
    public class EnemyBounds : MonoBehaviour
    {
        /// <summary>
        /// The SphereCollider component used to represent the boundary of the enemy object.
        /// </summary>
        private SphereCollider foundCollider;

        /// <summary>
        /// Returns the SphereCollider component used to represent the boundary of the enemy object.
        /// </summary>
        /// <returns>The SphereCollider component used to represent the boundary of the enemy object.</returns>
        public SphereCollider boundsCollider()
        {
            foundCollider = GetComponent<SphereCollider>();
            return foundCollider;
        }
    }
}