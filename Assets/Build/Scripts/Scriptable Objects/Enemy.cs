using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DP.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Archetypes/Enemy", order = 0)]
    /// <summary>
    /// Represents an enemy in the game.
    /// </summary>
    public class Enemy : ScriptableObject
    {
        /// <summary>
        /// Specifies the different types of enemies that can be created.
        /// </summary>
        public enum EnemyType { Zombie = 0, FemZombie = 1, Skinned = 2, Parasite = 3, Rotten = 4, Spider = 5 }

        /// <summary>
        /// Indicates whether this enemy has been initialized.
        /// </summary>
        public bool initialized;

        /// <summary>
        /// The type of enemy.
        /// </summary>
        [SerializeField] public EnemyType enemyType;

        /// <summary>
        /// The health points of the enemy.
        /// </summary>
        [SerializeField] public int health;

        /// <summary>
        /// Indicates whether custom damage multipliers should be used for this enemy.
        /// </summary>
        [SerializeField] public bool useCustomDamageMultipliers;

        /// <summary>
        /// The damage multiplier for the leg area.
        /// </summary>
        [SerializeField] public float legMultiplier;

        /// <summary>
        /// The damage multiplier for the upper leg area.
        /// </summary>
        [SerializeField] public float upLegMultiplier;

        /// <summary>
        /// The damage multiplier for the hips area.
        /// </summary>
        [SerializeField] public float hipsMultiplier;

        /// <summary>
        /// The damage multiplier for the torso area.
        /// </summary>
        [SerializeField] public float torsoMultiplier;

        /// <summary>
        /// The damage multiplier for the chest area.
        /// </summary>
        [SerializeField] public float chestMultiplier;

        /// <summary>
        /// The damage multiplier for the neck area.
        /// </summary>
        [SerializeField] public float neckMultiplier;

        /// <summary>
        /// The damage multiplier for the arm area.
        /// </summary>
        [SerializeField] public float armMultiplier;

        /// <summary>
        /// The damage multiplier for the forearm area.
        /// </summary>
        [SerializeField] public float foreArmMultiplier;

        /// <summary>
        /// The damage multiplier for the head area.
        /// </summary>
        [SerializeField] public float headMultiplier;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Enemy))]
	public class CustomEnemyInspector : Editor
	{
        public override void OnInspectorGUI()
        {
            Enemy enemy = (Enemy)target;
            if (!enemy.initialized)
            {
                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Init Zombie"))
                {
                    enemy.enemyType = 0;
                    enemy.health = 100;
                    enemy.initialized = true;
                }
                if (GUILayout.Button("Init Fem Zombie"))
                {
                    enemy.enemyType = (Enemy.EnemyType)1;
                    enemy.health = 100;
                    enemy.initialized = true;
                }
                if (GUILayout.Button("Init Skinned"))
                {
                    enemy.enemyType = (Enemy.EnemyType)2;

                    enemy.health = 135;
                    enemy.initialized = true;
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Init Parasite"))
                {
                    enemy.enemyType = (Enemy.EnemyType)3;

                    enemy.health = 220;
                    enemy.initialized = true;
                }
                if (GUILayout.Button("Init Rotten"))
                {
                    enemy.enemyType = (Enemy.EnemyType)4;
                    enemy.health = 150;
                    enemy.initialized = true;
                }
                if (GUILayout.Button("Init Spider"))
                {
                    enemy.enemyType = (Enemy.EnemyType)5;
                    enemy.health = 180;
                    enemy.initialized = true;
                }
                GUILayout.EndHorizontal();
            }
            base.OnInspectorGUI();
        }
    }
#endif
}