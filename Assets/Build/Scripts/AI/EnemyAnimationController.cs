using DP.DevTools;
using UnityEngine;

namespace DP.AI
{
    /// <summary>
    /// Class that manages the enemy animations.
    /// </summary>
    public class EnemyAnimationController : MonoBehaviour
    {
        AnimationHashManager eam;
        Animator animator;

        /// <summary>
        /// Initializes the animator and animation hash manager.
        /// </summary>
        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            eam = AnimationHashManager.Instance;
        }

        /// <summary>
        /// Plays the death animation.
        /// </summary>
        public void PlayDeathAnimation()
        {
            animator.SetBool("Death", true);
            animator.SetBool("Chasing", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Hit", false);
        }

        /// <summary>
        /// Plays the chase animation.
        /// </summary>
        public void PlayChaseAnimation()
        {
            animator.SetBool("Death", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Hit", false);
            animator.SetBool("Chasing", true);
        }

        /// <summary>
        /// Plays the attack animation.
        /// </summary>
        public void PlayAttackAnimation()
        {
            animator.SetBool("Death", false);
            animator.SetBool("Attack", true);
            animator.SetBool("Hit", false);
            animator.SetBool("Chasing", false);
        }

        /// <summary>
        /// Plays the hit animation.
        /// </summary>
        public void PlayHitAnimation()
        {
            animator.SetBool("Death", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Hit", true);
            animator.SetBool("Chasing", false);
        }
    }

}