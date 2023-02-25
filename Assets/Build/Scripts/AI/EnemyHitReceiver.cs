using UnityEngine;
using DP.AI;
using DP.Gameplay;
#if UNITY_EDITOR
using UnityEditor;
#endif



    namespace DP.Gameplay
    {
    /// <summary>
    /// Class that receives and handles hit events for enemy game objects.
    /// </summary>
    public class EnemyHitReceiver : MonoBehaviour
    {
        /// <summary>
        /// Reference to the health component of the enemy.
        /// </summary>
        public HealthComponent enemyHealth;

        /// <summary>
        /// Reference to the state component of the enemy.
        /// </summary>
        public EnemyStateComponent enemyState;

        private void Start()
        {
            // Initialize enemy health and state components
            enemyHealth = GetComponentInParent<HealthComponent>();
            enemyState = GetComponentInParent<EnemyStateComponent>();
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(EnemyHitReceiver))]
        public class CustomEnemyHitReceiverInspector : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
            }
        }
#endif
    }