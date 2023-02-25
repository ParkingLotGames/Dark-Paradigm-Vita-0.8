using System.Collections;
using UnityEngine;

namespace DP.DevTools
{
    /// <summary>
    /// Component used to automatically disable game object after a certain time has passed. Used for particles and flashes.
    /// </summary>
    public class SelfDeactivatingDespawner : MonoBehaviour
    {
        [Tooltip("Time in seconds before the game object is disabled")]
        public float timeToDespawn;

        /// <summary>
        /// Coroutine that disables the game object after <see cref="timeToDespawn"/> seconds have passed.
        /// </summary>
        IEnumerator DespawnObject()
        {
            yield return new WaitForSeconds(timeToDespawn);
            this.gameObject.SetActive(false);
            StopAllCoroutines();
        }

        /// <summary>
        /// Enables the component and starts the coroutine to disable the game object.
        /// </summary>
        void OnEnable()
        {
            StartCoroutine(DespawnObject());
        }

        /// <summary>
        /// Disables the component.
        /// </summary>
        private void OnDisable()
        {
            this.enabled = false;
        }
    }
}