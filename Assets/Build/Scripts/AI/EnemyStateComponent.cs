using UnityEngine;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using DP.Management;
using DP.Gameplay;
using UnityEngine.Events;

namespace DP.AI
{
    /// <summary>
    /// Component that manages the enemy's state and behavior.
    /// </summary>
    public class EnemyStateComponent : MonoBehaviour
    {
        /// <summary>
        /// The type of enemy.
        /// </summary>
        [SerializeField]
        public EnemyType enemyType;

        /// <summary>
        /// The current state of the enemy.
        /// </summary>
        [SerializeField]
        public EnemyState enemyState;

        /// <summary>
        /// The colliders attached to the enemy.
        /// </summary>
        [SerializeField]
        public EnemyColliderContainer colliders;

        /// <summary>
        /// The health component attached to the enemy.
        /// </summary>
        [SerializeField]
        public HealthComponent health;

        /// <summary>
        /// The animation controller for the enemy.
        /// </summary>
        [SerializeField]
        public EnemyAnimationController enemyAnimator;

        /// <summary>
        /// The component that manages the enemy's respawn behavior.
        /// </summary>
        [SerializeField]
        public EnemyRespawnComponent respawnComponent;

        /// <summary>
        /// The spawner that creates pick-ups when the enemy is killed.
        /// </summary>
        [SerializeField]
        public EnemyLootSpawner pickupSpawner;

        /// <summary>
        /// The component that manages pathfinding for the enemy.
        /// </summary>
        [SerializeField]
        public repath repath;

        /// <summary>
        /// Indicates whether the enemy is currently hurt.
        /// </summary>
        [SerializeField]
        public bool isHurt;

        /// <summary>
        /// Indicates whether the enemy is currently dead.
        /// </summary>
        [SerializeField]
        public bool isDead;

        /// <summary>
        /// Indicates whether the enemy's kill count has been incremented.
        /// </summary>
        [SerializeField]
        public bool killCounted;

        /// <summary>
        /// Indicates whether the enemy is prepared to spawn.
        /// </summary>
        [SerializeField]
        public bool preparedToSpawn;

        /// <summary>
        /// Indicates whether the enemy is ready to behave according to its current state.
        /// </summary>
        [SerializeField]
        public bool ready;

        /// <summary>
        /// The player's stats.
        /// </summary>
        [SerializeField]
        public PlayerStats playerStats;

        private bool droppedObject;

        private Rigidbody rb;

        /// <summary>
        /// Event that is triggered when the enemy is created.
        /// </summary>
        [SerializeField]
        public UnityEvent OnEnemyCreation;

        /// <summary>
        /// Event that is triggered when the enemy spawns.
        /// </summary>
        [SerializeField]
        public UnityEvent OnEnemySpawn;

        /// <summary>
        /// Event that is triggered when the enemy is killed.
        /// </summary>
        [SerializeField]
        public UnityEvent OnEnemyKilled;

        private EnemySpawnManager enemySpawnManager;

        /// <summary>
        /// Called during the enemy's creation.
        /// </summary>
        public void DuringEnemyCreation()
        {
            repath.enabled = false;
            enemyAnimator.enabled = false;
        }

        /// <summary>
        /// Called during the enemy's spawn.
        /// </summary>
        public void DuringEnemySpawn()
        {
        }

        /// <summary>
        /// Called before the first frame.
        /// Initializes component references and disables the enemy's pathfinding.
        /// </summary>
        private void Awake()
        {
            enemyAnimator = GetComponentInChildren<EnemyAnimationController>();
            health = GetComponent<HealthComponent>();
            respawnComponent = GetComponent<EnemyRespawnComponent>();
            colliders = GetComponent<EnemyColliderContainer>();
            repath = GetComponentInChildren<repath>();
            enemySpawnManager = GetComponentInParent<EnemySpawnManager>();
            repath.enabled = false;
            PickupSpawner();

        }
        /// <summary>
        /// Finds the enemy's loot spawner component and assigns it to the pickupSpawner field.
        /// </summary>
        private void PickupSpawner()
        {
            pickupSpawner = GetComponentInChildren<EnemyLootSpawner>();
        }

        private void Update()
        {
            RunStateBehavior();
        }

        /// <summary>
        /// Executes the appropriate behavior for the enemy's current state.
        /// Determines the enemy's state based on its current health and whether or not it is hurt.
        /// Sets the appropriate speed, animation, and actions for each state.
        /// </summary>
        private void RunStateBehavior()
        {

            if (enemyState == EnemyState.WaitingToSpawn)
            {
//                movement.DespawnedBehavior();
                killCounted = false;
                health.ResetHealth();
                isDead = false;
                ready = false;
            }
            if (enemyState == EnemyState.Spawning)
            {
//                movement.StopNavigation();
                respawnComponent.Spawn();
                respawnComponent.enqueued = false;
                ready = true;
                transform.position = enemySpawnManager.spawnPoints[Random.Range(0, enemySpawnManager.spawnPoints.Length)].position;
                repath.navAgent.enabled = true;
                colliders.EnableColliders();
                droppedObject = false;
                OnEnemySpawn.Invoke();
    
            repath.enabled = true;
                enemyState = EnemyState.Chasing;
            }
            if (ready)
            {
                if (health.currentHealth > 45)//Percent not working wtf
                    enemyState = EnemyState.Chasing;
                if (health.currentHealth < 45 && health.currentHealth > 25)//Percent not working wtf
                    enemyState = EnemyState.Walking;
                if (isHurt && health.currentHealth < 35 && health.currentHealth > 0)//Percent not working wtf
                    enemyState = EnemyState.Crawling;
                if (health.currentHealth <= 0)//Percent not working wtf
                    enemyState = EnemyState.RegularDeath;
                if (isHurt && health.currentHealth <= 0)//Percent not working wtf
                    enemyState = EnemyState.CrawlDeath;
            }
            if (enemyState == EnemyState.Walking)
            {
//                movement.SetWalkSpeed();
//                movement.InvokeRetarget();
            }
            if (enemyState == EnemyState.Chasing)
            {
                enemyAnimator.PlayChaseAnimation();
                //               movement.InvokeRetarget();
            }
            if (enemyState == EnemyState.Attacking)
            {

            }
            if (enemyState == EnemyState.Crawling)
            {
//                movement.SetCrawlSpeed();
//                movement.InvokeRetarget();
            }
            if (enemyState == EnemyState.RegularDeath)
            {
                Die();
            }
            if (enemyState == EnemyState.CrawlDeath)
            {
                Die();
            }
        }

        /// <summary>
        /// Called when the enemy dies.
        /// </summary>
        public void Die()
        {
            isDead = true; //for weapon
            //movement.DestroyNavAgent();
            enemyAnimator.PlayDeathAnimation();
            if (!droppedObject)
            {
                var pickup = Gameplay.PickupPool.Instance.Get();
                pickup.transform.parent = pickupSpawner.transform;
                pickup.transform.position = pickupSpawner.transform.position;
                pickup.SetActive(true);
                pickup.transform.parent = Gameplay.PickupPool.Instance.container;
                droppedObject = true;
            }
            //if (!rb)
            //    rb = gameObject.AddComponent<Rigidbody>();
            //rb.detectCollisions = false;
            Invoke("RemoveRigidbody", .45f);
            colliders.DisableColliders();

            if (!killCounted)
            {
                if (enemyType == EnemyType.zombie)
                {
                    playerStats.inGameStats.zombiesKilledThisWave += 1;
                }
                if (enemyType == EnemyType.boss)
                {
                    playerStats.inGameStats.bossesKilledThisWave += 1;
                }
                playerStats.inGameStats.totalEnemiesKilledThisGame += 1;
                WaveManager.Instance.totalEnemiesKilledThisWave = playerStats.inGameStats.totalEnemiesKilledThisGame;
                WaveManager.Instance.totalRemainingEnemiesThisWave--;
                OnEnemyKilled.Invoke();
                respawnComponent.Despawn();
                killCounted = true;
            }
        }
            private void RemoveRigidbody()
        {
            Invoke("RemoveAgent",1);
            //if (rb)
            //    Destroy(rb);

        }

        private void RemoveAgent()
        {
            repath.navAgent.enabled = false;
            repath.enabled = false;
            respawnComponent.Despawn();
            transform.position = enemySpawnManager.idlingSpot.position;
        }
    }
    /// <summary>
    /// Represents the different states that an enemy can be in
    /// </summary>
    public enum EnemyState
    {
        WaitingToSpawn, Spawning, Walking, Chasing, Attacking, RegularDeath, Hurt, Crawling, CrawlDeath, Jumping, Faliing
    }
    /// <summary>
    /// Represents the different types of enemies.
    /// </summary>
    public enum EnemyType
    {
        boss, zombie
    }
}