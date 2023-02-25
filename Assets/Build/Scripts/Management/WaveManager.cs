using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace DP.Management
{
    /// <summary>
    /// Manages the waves of enemies in the game.
    /// </summary>
    public class WaveManager : MonoBehaviour
    {
        /// <summary>
        /// The current wave number.
        /// </summary>
        public int waveNum;

        /// <summary>
        /// The number of bosses to spawn this wave.
        /// </summary>
        public int bossesThisWave = 2;

        /// <summary>
        /// The number of bosses that have already been spawned this wave.
        /// </summary>
        public int bossesSpawnedThisWave;

        /// <summary>
        /// The number of zombies to spawn this wave.
        /// </summary>
        public int zombiesThisWave = 25;

        /// <summary>
        /// The number of zombies that have already been spawned this wave.
        /// </summary>
        public int zombiesSpawnedThisWave = 0;

        /// <summary>
        /// The time until the next wave.
        /// </summary>
        public float timeToNextWave;

        /// <summary>
        /// The number of zombies killed this wave.
        /// </summary>
        public int zombiesKilledThisWave;

        /// <summary>
        /// The number of bosses killed this wave.
        /// </summary>
        public int bossesKilledThisWave;

        /// <summary>
        /// The total number of enemies killed this wave.
        /// </summary>
        public int totalEnemiesKilledThisWave;

        /// <summary>
        /// Whether the current wave has finished.
        /// </summary>
        public bool waveFinished;

        int additionalZombiesThisWave;
        int additionalBossesThisWave;

        /// <summary>
        /// The total number of remaining enemies this wave.
        /// </summary>
        public int totalRemainingEnemiesThisWave;

        int extraZombiesFromWaveNumber;

        /// <summary>
        /// Unity event invoked when a wave is finished.
        /// </summary>
        [SerializeField] UnityEvent OnWaveFinished;

        /// <summary>
        /// Unity event invoked when a new wave is started.
        /// </summary>
        [SerializeField] UnityEvent OnWaveStarted;

        /// <summary>
        /// Singleton instance of the WaveManager.
        /// </summary>
        public static WaveManager Instance;

        /// <summary>
        /// Decrements the total number of remaining enemies and prepares for the next wave if no enemies remain.
        /// </summary>
        public void CountKill()
        {
            totalRemainingEnemiesThisWave -= 1;
            if (totalRemainingEnemiesThisWave == 0)
                PrepareNextWave();
        }

        /// <summary>
        /// Resets the game to its initial state.
        /// </summary>
        public void ResetGame()
        {
            ZeroWave();
            PrepareNextWave();
        }
        /// <summary>
        /// Initializes the singleton instance of the WaveManager.
        /// </summary>
        private void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);
        }

        /// <summary>
        /// Starts the game with a new wave.
        /// </summary>
        private void Start()
        {
            ResetGame();
        }

        /// <summary>
        /// Resets the current wave number to 0.
        /// </summary>
        private void ZeroWave()
        {
            waveNum = 0;
        }

        /// <summary>
        /// Starts a new wave of enemies.
        /// </summary>
        void StartNewWave()
        {
            waveFinished = false;
            OnWaveStarted.Invoke();
        }

        /// <summary>
        /// Sets the values for the next wave of enemies.
        /// </summary>
        private void SetNewWaveValues()
        {
            extraZombiesFromWaveNumber = waveNum;
            additionalBossesThisWave = 0;//Random.Range(0, 2);
            additionalZombiesThisWave = Random.Range(-3, 4);
            zombiesThisWave = Random.Range(18, 25) + (additionalZombiesThisWave + extraZombiesFromWaveNumber);
            bossesThisWave = 0;// Random.Range(0, 2) +  additionalBossesThisWave;
            totalRemainingEnemiesThisWave = zombiesThisWave + bossesThisWave;
        }

        /// <summary>
        /// Checks if all enemies in the current wave have been killed and prepares for the next wave if they have.
        /// </summary>
        private void Update()
        {
            if (totalRemainingEnemiesThisWave == 0)
                PrepareNextWave();
        }

        /// <summary>
        /// Prepares for the next wave of enemies.
        /// </summary>
        public void PrepareNextWave()
        {
            waveFinished = true;
            waveNum += 1;
            OnWaveFinished.Invoke();
            ResetWaveCounters();
            SetNewWaveValues();
            if (waveNum != 1)
                Invoke("StartNewWave", timeToNextWave);
        }

        /// <summary>
        /// Resets the wave counters to their initial state.
        /// </summary>
        public void ResetWaveCounters()
        {
            zombiesSpawnedThisWave = 0;
            bossesSpawnedThisWave = 0;
            totalEnemiesKilledThisWave = 0;
            zombiesKilledThisWave = 0;
            bossesKilledThisWave = 0;
        }
    }
}