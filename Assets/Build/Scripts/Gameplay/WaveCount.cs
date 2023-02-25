using UnityEngine;
using UnityEngine.UI;

namespace DP.ScriptableEvents
{
    /// <summary>
    /// Represents a component responsible for updating and resetting the wave count displayed in the game.
    /// </summary>
    public class WaveCount : MonoBehaviour
    {
        /// <summary>
        /// The PlayerStats component to use for getting the current wave number.
        /// </summary>
        [SerializeField] PlayerStats playerStats;

        /// <summary>
        /// The Text component to use for displaying the wave count.
        /// </summary>
        Text text;

        /// <summary>
        /// The current wave count value.
        /// </summary>
        public int Value;

        /// <summary>
        /// Sets up the component by getting the Text component.
        /// </summary>
        private void Awake()
        {
            text = GetComponent<Text>();
        }

        /// <summary>
        /// Updates the wave count displayed in the game based on the current wave number.
        /// </summary>
        public void UpdateWaveCount()
        {
            // Sets the text of the Text component to the current wave number.
            text.text = $"WAVE {playerStats.inGameStats.currentWave}";
        }

        /// <summary>
        /// Resets the wave count to 1 and updates the display.
        /// </summary>
        public void ResetWaveCount()
        {
            // Resets the current wave number to 1.
            playerStats.inGameStats.currentWave = 1;

            // Updates the display with the new wave count.
            text.text = $"WAVE {playerStats.inGameStats.currentWave}";
        }
    }

}