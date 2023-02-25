using UnityEngine;                                                                                
using DP.Management;
using DP.AI;
using System.Collections.Generic;
using DP.ResourceManagement;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace DP.Gameplay
{
    /// <summary>
    /// Class that manages enemy spawning.
    /// </summary>
    public class EnemySpawnManager : MonoBehaviour
    {
        [SerializeField]public int prewarmEnemiesPerTypePS4, prewarmEnemiesPerTypeVita, prewarmEnemiesPerTypeAndroidLow, prewarmEnemiesPerTypeAndroidMid, prewarmEnemiesPerTypeAndroidHigh, prewarmEnemiesPerTypePCLow, prewarmEnemiesPerTypePCMid, prewarmEnemiesPerTypePCHigh;
        [SerializeField]public int prewarmBossesPerTypePS4, prewarmBossesPerTypeVita;//, prewarmEnemiesPerTypeAndroidLow, prewarmEnemiesPerTypeAndroidMid, prewarmEnemiesPerTypeAndroidHigh, prewarmEnemiesPerTypePCLow, prewarmEnemiesPerTypePCMid, prewarmEnemiesPerTypePCHigh;
        [SerializeField]public Queue<GameObject> pooledEnemies = new Queue<GameObject>();
        [SerializeField]public Queue<GameObject> pooledBosses = new Queue<GameObject>();
        [SerializeField]public bool canCurrentlySpawn;
        [SerializeField]public bool isSpawnAllowed;
        [SerializeField]public int spawnsAllowed;
        [SerializeField]public UnityEvent OnEnemySpawned;
        [SerializeField]public Transform[] spawnPoints;
        [SerializeField]public Transform idlingSpot;
        public Transform enemiesContainer;
        int remainingSpawns;
        float spawnTimer;
        List<GameObject> enemiesToLoad = new List<GameObject>();
        List<GameObject> bossesToLoad = new List<GameObject>();
        int enemiesToLoadCount, bossesToLoadCount;

        /// <summary>
        /// Gets an enemy from the enemy pool.
        /// NOTE: Allows the pool to grow on demand on desktop platforms.
        /// </summary>
        public GameObject GetEnemy()
        {
#if UNITY_PS4 || UNITY_XBOXONE || UNITY_STANDALONE
            //if (enemies.Count == 0)
            //  AddEnemies(1);
#endif
            return pooledEnemies.Dequeue();
        }
        /// <summary>
        /// Gets a boss from the boss pool.
        /// </summary>
        public GameObject GetBoss()
        {
            return pooledBosses.Dequeue();
        }

        private void Awake()
        {
            enemiesContainer = this.transform;
            GameObject skinnedPrefab =  ((GameObject)Resources.Load(ResourcesPathContainer.vitaSkinned));
            GameObject parasitePrefab = ((GameObject)Resources.Load(ResourcesPathContainer.vitaParasite));
            GameObject slugPrefab =     ((GameObject)Resources.Load(ResourcesPathContainer.vitaSlug));
            
            enemiesToLoad.Add(skinnedPrefab);
            bossesToLoad.Add(parasitePrefab);
            bossesToLoad.Add(slugPrefab);
                         
            enemiesToLoadCount = enemiesToLoad.Count;
            bossesToLoadCount = bossesToLoad.Count;

            AddEnemiesToPool(prewarmEnemiesPerTypeVita);
            AddBossesToPool(prewarmBossesPerTypeVita);

            spawnsAllowed = 1;
            remainingSpawns = spawnsAllowed;

        }
        private void Update()
        {
            CheckSpawnAllowance();
        }

        /// <summary>
        /// Adds enemies to the pool.
        /// </summary>
        void AddEnemiesToPool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < enemiesToLoadCount; j++)
                {
                    var enemy = Instantiate(enemiesToLoad[j], enemiesContainer.position, Quaternion.identity, enemiesContainer);
                    pooledEnemies.Enqueue(enemy);
                    var enemyState = enemy.GetComponentInChildren<EnemyStateComponent>();
                    enemyState.repath.navAgent.enabled = false;
#if UNITY_EDITOR
                    Debug.Log("1 " + enemy.GetComponent<EnemyStateComponent>().enemyType.ToString() + " Created & Pooled, " + pooledEnemies.Count + " enemies in pool");
#endif
                }

            }
        }
        /// <summary>
        /// Adds bosses to the pool.
        /// </summary>
        void AddBossesToPool(int count)
        {
             //for each requested prewarm
            for (int i = 0; i < count; i++)
            {
                //we create one of each enemy type
                for (int j = 0; j < bossesToLoadCount; j++)
                {
                    var boss = Instantiate(bossesToLoad[j], enemiesContainer.position, Quaternion.identity, enemiesContainer);
                    pooledBosses.Enqueue(boss);
                    var bossState = boss.GetComponentInChildren<EnemyStateComponent>();
#if UNITY_EDITOR
                    Debug.Log("1 " + boss.GetComponent<EnemyStateComponent>().enemyType.ToString() + " Created & Pooled, " + pooledEnemies.Count + " enemies in pool");
#endif
                }

            }
        }
        /// <summary>
        /// Returns an enemy to the pool.
        /// </summary>
        public void ReturnEnemyToPool(GameObject enemy)
        {
            pooledEnemies.Enqueue(enemy);
#if UNITY_EDITOR
            Debug.Log("1" + enemy.GetComponent<EnemyStateComponent>().enemyType.ToString() + " Returned to Pool, " + pooledEnemies.Count + " enemies remaining in pool");
#endif
        }
        /// <summary>
        /// Returns a boss to the pool.
        /// </summary>
        public void ReturnBossToPool(GameObject enemy)
        {
            pooledBosses.Enqueue(enemy);
#if UNITY_EDITOR
            Debug.Log("1" + enemy.GetComponent<EnemyStateComponent>().enemyType.ToString() + " Returned to Pool, " + pooledEnemies.Count + " enemies remaining in pool");
#endif
        }

        [SerializeField]public float SpawnTime;
        private int totalRemainingEnemiesThisWave;
        /// <summary>
        /// Checks if spawning is allowed.
        /// </summary>
        private void CheckSpawnAllowance()
        {
            if (WaveManager.Instance.totalRemainingEnemiesThisWave != 0 && pooledEnemies.Count != 0 && WaveManager.Instance.zombiesSpawnedThisWave < WaveManager.Instance.zombiesThisWave)
            {
                isSpawnAllowed = true;
                remainingSpawns = spawnsAllowed;
            }
            else
            {
                isSpawnAllowed = false;
                remainingSpawns = 0;
            }

            if (WaveManager.Instance.totalRemainingEnemiesThisWave == 0)
                WaveManager.Instance.PrepareNextWave();

            if (isSpawnAllowed)
            {
                if (canCurrentlySpawn)
                {
                    SpawnEnemy(1);
                    if (isSpawnAllowed)
                        remainingSpawns = spawnsAllowed;
                    else
                        remainingSpawns = 0;
                }
                else
                {
                    spawnTimer += Time.deltaTime;
                    if (spawnTimer >= SpawnTime)
                    {
                        canCurrentlySpawn = true;
                        spawnTimer = 0;
                        remainingSpawns = spawnsAllowed;
                    }
                }
            }
        }
        /// <summary>
        /// Spawns an enemy.
        /// </summary>
        private void SpawnEnemy(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject activeEnemy = GetEnemy();
                activeEnemy.GetComponent<EnemyStateComponent>().enemyState = EnemyState.Spawning;
                
                remainingSpawns -= 1;
                canCurrentlySpawn = false;
#if UNITY_EDITOR
                Debug.Log("1" + activeEnemy.GetComponent<EnemyStateComponent>().enemyType.ToString() + " Spawned, " + pooledEnemies.Count + " enemies remaining in pool");
#endif
                OnEnemySpawned.Invoke();
            }
        }
        /// <summary>
        /// Resets the spawn.
        /// </summary>
        private void ResetSpawnAllowance()
        {
            canCurrentlySpawn = true;
        }

    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(EnemySpawnManager))]
    [CanEditMultipleObjects]

    public class CustomEnemySpawnManagerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            #region GUIStyles
            //Define GUIStyles

            #endregion

            #region Layout Widths
            GUILayoutOption width32 = GUILayout.Width(32);
            GUILayoutOption width40 = GUILayout.Width(40);
            GUILayoutOption width48 = GUILayout.Width(48);
            GUILayoutOption width64 = GUILayout.Width(64);
            GUILayoutOption width80 = GUILayout.Width(80);
            GUILayoutOption width96 = GUILayout.Width(96);
            GUILayoutOption width112 = GUILayout.Width(112);
            GUILayoutOption width128 = GUILayout.Width(128);
            GUILayoutOption width144 = GUILayout.Width(144);
            GUILayoutOption width160 = GUILayout.Width(160);
            #endregion

            //base.OnInspectorGUI();
            EnemySpawnManager EnemySpawnManager = (EnemySpawnManager)target;
            SerializedProperty psVitaEnemies    = serializedObject.FindProperty("prewarmEnemiesPerTypeVita");
            SerializedProperty psVitaBosses     = serializedObject.FindProperty("prewarmBossesPerTypeVita");
            SerializedProperty canCurrentlySpawn = serializedObject.FindProperty("canCurrentlySpawn");
            SerializedProperty isSpawnAllowed   = serializedObject.FindProperty("isSpawnAllowed");
            SerializedProperty spawnsAllowed    = serializedObject.FindProperty("spawnsAllowed");
            SerializedProperty spawnPoints = serializedObject.FindProperty("spawnPoints");
            SerializedProperty idlingSpot = serializedObject.FindProperty("idlingSpot");
            SerializedProperty onEnemySpawned    = serializedObject.FindProperty("OnEnemySpawned");
            serializedObject.Update();

            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 200;
            EditorGUILayout.PropertyField(psVitaEnemies);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 192;
            EditorGUILayout.PropertyField(psVitaBosses);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 160;
            EditorGUILayout.PropertyField(canCurrentlySpawn);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 144;
            EditorGUILayout.PropertyField(isSpawnAllowed   );
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 128;
            EditorGUILayout.PropertyField(spawnsAllowed    );
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 160;
            EditorGUILayout.PropertyField(idlingSpot   );
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 160;
            EditorGUILayout.PropertyField(spawnPoints, true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 160;
            EditorGUILayout.PropertyField(onEnemySpawned   );
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
    #endregion
}