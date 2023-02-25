using UnityEngine;
using DP.Gameplay;
using DP.Management;

namespace DP.AI
{
    /// <summary>
    /// Handles the spawning and despawning of enemies.
    /// </summary>
    public class EnemyRespawnComponent : MonoBehaviour
    {
        HealthComponent enemyHealth;
        EnemyStateComponent state;
        public SkinnedMeshRenderer mesh;
        EnemySpawnManager enemySpawnManager;
        bool spawned;

        /// <summary>
        /// Indicates whether the enemy is enqueued for respawning.
        /// </summary>
        public bool enqueued;

        private void Awake()
        {
            mesh = GetComponentInChildren<SkinnedMeshRenderer>();
            enemyHealth = GetComponentInChildren<HealthComponent>();
            state = GetComponent<EnemyStateComponent>();
            enemySpawnManager = GetComponentInParent<EnemySpawnManager>();
        }

        /// <summary>
        /// Spawns the enemy if it hasn't been spawned before, and increments the number of enemies spawned.
        /// </summary>
        public void Spawn()
        {
            if (!spawned)
            {
                Relocate();
                Activate();
            }
            CountSpawn();
        }

        private void CountSpawn()
        {
            WaveManager.Instance.zombiesSpawnedThisWave++;
            spawned = true;
        }

        /// <summary>
        /// Despawns the enemy by disabling its game object and scheduling it to be returned to the object pool after a delay.
        /// </summary>
        public void Despawn()
        {
            Invoke("Pool", 5f);
            spawned = false;
        }

        private void Pool()
        {
            if (!enqueued && !spawned)
            {
                enemySpawnManager.ReturnEnemyToPool(this.gameObject);
                enqueued = true;
                state.enemyState = EnemyState.WaitingToSpawn;
            }
        }

        private void Activate()
        {
            enemyHealth.gameObject.SetActive(false);
            enemyHealth.gameObject.SetActive(true);
        }

        private void Relocate()
        {
            //enemyHealth.transform.localPosition = new Vector3(0, 0, 0);
            transform.position = enemySpawnManager.idlingSpot.position;
        }
    }
}