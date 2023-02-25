using UnityEngine;
using System.Collections;

namespace DP.Gameplay
{
    /// <summary>
    /// A component that despawns a decal after a certain amount of time has passed.
    /// </summary>
    public class DecalDespawner : MonoBehaviour
    {
        /// <summary>
        /// An enumeration of the different types of decals that can be despawned.
        /// </summary>
        public enum DecalType { Metal = 0, Wood = 1, Concrete = 2, Blood = 3 }

        /// <summary>
        /// The type of decal to be despawned.
        /// </summary>
        [SerializeField]
        public DecalType decalType;

        /// <summary>
        /// A reference to the decal pool that this component uses to despawn decals.
        /// </summary>
        [Tooltip("Reference to our pool, remember to set it or alternatively configure your pool as a static Instance to get rid of this ")]
        public DecalPool pool;

        /// <summary>
        /// The amount of time (in seconds) before the decal is despawned.
        /// </summary>
        [SerializeField]
        float timeToDespawn;

        /// <summary>
        /// Called when the object is enabled.
        /// </summary>
        void OnEnable()
        {
            StartCoroutine(DespawnObject());
        }

        /// <summary>
        /// Despawns the decal after the specified amount of time has passed.
        /// </summary>
        /// <returns>An IEnumerator used to implement the coroutine.</returns>
        IEnumerator DespawnObject()
        {
            yield return new WaitForSeconds(timeToDespawn);
            if (decalType == DecalType.Metal)
                pool.ReturnToMetalPool(gameObject);
            if (decalType == DecalType.Wood)
                pool.ReturnToWoodPool(gameObject);
            if (decalType == DecalType.Concrete)
                pool.ReturnToConcretePool(gameObject);
            if (decalType == DecalType.Concrete)
                pool.ReturnToBloodPool(gameObject);
            StopAllCoroutines();//that's it
        }
    }

}