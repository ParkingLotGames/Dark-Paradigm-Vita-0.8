using UnityEngine;
using UnityEngine.AI;
//using NavJob.Components;
namespace DP.AI
{
    /// <summary>
    /// Represents an instance of an enemy in the game.
    /// </summary>
    public class EnemyInstanceComponent : MonoBehaviour
    {
        /// <summary>
        /// Determines whether the navigation for this enemy is enabled or not.
        /// </summary>
        [HideInInspector] public bool navigationEnabled;

        /// <summary>
        /// Determines whether the enemy has repathed.
        /// </summary>
        [HideInInspector] public bool repathed;

        /// <summary>
        /// The NavMeshAgent attached to this enemy.
        /// </summary>
        [HideInInspector] public NavMeshAgent agent;

        /// <summary>
        /// The Animator component attached to a child object of this enemy.
        /// </summary>
        [HideInInspector] public Animator animator;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
        }
    }
}