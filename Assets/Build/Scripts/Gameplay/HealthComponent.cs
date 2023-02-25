using UnityEngine;
using DP.Interfaces;
using UnityEngine.Events;

namespace DP.Gameplay
{
    /// <summary>
    /// Represents a component that manages the health of an entity and implements the IDamageable, IHealable, and IKillable interfaces.
    /// </summary>
    public class HealthComponent : MonoBehaviour, IDamageable, IHealable, IKillable
    {
        /// <summary>
        /// The current health of the entity.
        /// </summary>
        [SerializeField] public int currentHealth;

        /// <summary>
        /// The starting health of the entity.
        /// </summary>
        [SerializeField] public int startingHealth;

        /// <summary>
        /// The maximum health of the entity.
        /// </summary>
        [SerializeField] public int maxHealth;

        /// <summary>
        /// An event that is triggered when the entity's health changes.
        /// </summary>
        [SerializeField] UnityEvent OnHealthChanged;

        /// <summary>
        /// An event that is triggered when the entity dies.
        /// </summary>
        [SerializeField] UnityEvent OnDie;

        /// <summary>
        /// The current health of the entity as a percentage of its maximum health.
        /// </summary>
        public int currentHealthPercent;

        /// <summary>
        /// Resets the player's health to its starting value and triggers the OnHealthChanged event.
        /// </summary>
        public void ResetPlayerHealth()
        {
            if(currentHealth < startingHealth)
            currentHealth = startingHealth;
            OnHealthChanged.Invoke();
        }

        /// <summary>
        /// Updates the current health of the entity as a percentage of its maximum health.
        /// </summary>
        public void UpdateHealthPercentage()
        {
            currentHealthPercent = (currentHealth / maxHealth) * 100;
        }
        /// <summary>
        /// Applies damage to the entity and triggers the OnHealthChanged event. If the entity's health drops to 0 or below, triggers the OnDie event.
        /// </summary>
        /// <param name="damage">The amount of damage to apply.</param>
        public void TakeDamage(int damage)
        {
            if (currentHealth > 0)
            {
                currentHealth -= damage;
                OnHealthChanged.Invoke();
                Debug.Log($"Dealt {damage} of damage, enemy remaining HP: {currentHealth}");
            }
            if(currentHealth <= 0)
            {
                OnHealthChanged.Invoke();
                Die();
                Debug.Log($"Dealt {damage} of damage, enemy remaining HP: {currentHealth}, enemy died");
            }
        }
        /// <summary>
        /// Heals the entity and triggers the OnHealthChanged event. The entity's health cannot exceed its maximum health.
        /// </summary>
        /// <param name="healing">The amount of healing to apply.</param>
        public void Heal(int healing)
        {
            currentHealth = Mathf.Clamp(currentHealth += healing, 1, maxHealth);
            OnHealthChanged.Invoke();
        }
        /// <summary>
        /// Resets the entity's health to its starting value and triggers the OnHealthChanged event.
        /// </summary>
        public void ResetHealth()
        {
            currentHealth = startingHealth;
            OnHealthChanged.Invoke();
        }
        /// <summary>
        /// Triggers the OnDie event.
        /// </summary>
        public void Die()
        {
            OnDie.Invoke();
        }
    }
}