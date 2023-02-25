using UnityEngine;
using DP.DevTools;
using UnityEngine.UI;
using DP.Gameplay;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.UI
{
    /// <summary>
    /// Represents a health bar UI element that displays the current health of a player.
    /// </summary>
    public class Healthbar : MonoBehaviour
    {
        /// <summary>
        /// The gradient used to color the health bar.
        /// </summary>
        [SerializeField] public Gradient healthbarColor;

        /// <summary>
        /// The slider used to display the health bar's value.
        /// </summary>
        [SerializeField] public Slider healthbarSlider;

        /// <summary>
        /// The image used to display the health bar's color.
        /// </summary>
        [SerializeField] public Image healthbarImage;

        /// <summary>
        /// The HealthComponent associated with the health bar.
        /// </summary>
        [SerializeField] HealthComponent playerHealth;

        /// <summary>
        /// A boolean flag indicating whether the player's health is low.
        /// </summary>
        [SerializeField] public bool lowHealth;

        /// <summary>
        /// A boolean flag indicating whether the player's health is critical.
        /// </summary>
        [SerializeField] public bool criticalHealth;

        /// <summary>
        /// The current color of the health bar.
        /// </summary>
        Color currentColor;

        /// <summary>
        /// Initializes the health bar with the correct maximum value.
        /// </summary>
        private void Awake()
        {
            healthbarSlider.maxValue = playerHealth.maxHealth;
        }

        /// <summary>
        /// Updates the health bar's value and color.
        /// </summary>
        public void UpdateHealthBar()
        {
            healthbarSlider.value = playerHealth.currentHealth;
            currentColor = healthbarColor.Evaluate(healthbarSlider.value / playerHealth.maxHealth);
            lowHealth = playerHealth.currentHealth <= 35 && playerHealth.currentHealth >= 15;
            criticalHealth = playerHealth.currentHealth <= 15 && playerHealth.currentHealth >= 0.00001f;
            healthbarImage.color = healthbarColor.Evaluate(healthbarSlider.value / playerHealth.maxHealth);
        }

        /// <summary>
        /// Updates the health bar's color based on the player's current health.
        /// </summary>
        private void Update()
        {
            currentColor = healthbarColor.Evaluate(healthbarSlider.value / playerHealth.maxHealth);
            if (lowHealth)
                healthbarImage.color = Color.Lerp(currentColor, healthbarColor.Evaluate(1), Mathf.PingPong(Time.time * 2, 1));
            if (criticalHealth)
                healthbarImage.color = Color.Lerp(currentColor, healthbarColor.Evaluate(1), Mathf.PingPong(Time.time * 5, 1));
        }
    }
}