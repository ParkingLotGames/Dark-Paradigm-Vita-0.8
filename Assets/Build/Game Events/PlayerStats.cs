using UnityEngine;
using DP.DevTools;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    [CreateAssetMenu]
    public class PlayerStats : ScriptableObject
    {
        #region Variables
        [System.Serializable]
        public class PersistentStats
        {
            [SerializeField] public int currentPlayerCash;
            [SerializeField] public int currentPlayerPoints;
            [SerializeField] public int playerXP;
            [SerializeField] public int playerLevel;
            /// <summary> 
            /// Amount of points required for next level up.
            /// </summary>
            [SerializeField] public List<int> XPRequiredToLevelUp;
            [SerializeField] public int totalCashWon;
            [SerializeField] public int totalPointsWon;
            [SerializeField] public int totalCashSpent;
            [SerializeField] public int totalPointsSpent;
            [SerializeField] public int WavesPlayed;

            [SerializeField] public int totalZombiesKilled;
            [SerializeField] public int totalMalesKilled;
            [SerializeField] public int totalFemalesKilled;
            [SerializeField] public int totalBossesKilled;
            [SerializeField] public int totalEnemiesKilled;

            [SerializeField] public int totalPistolKills;
            [SerializeField] public int totalSmgKills;
            [SerializeField] public int totalArKills;
            [SerializeField] public int totalRifleKills;
            [SerializeField] public int totalShotgunKills;
            [SerializeField] public int totalBonusKills;
            [SerializeField] public int totalThrowableKills;
        }

        [System.Serializable]
        public class InGameStats
        {
            [SerializeField] public int cashWonThisWave;
            [SerializeField] public int cashWonThisGame;
            [SerializeField] public int xpWonThisWave;
            [SerializeField] public int xpWonThisGame;
            [SerializeField] public int pointsWonThisWave;
            [SerializeField] public int pointsWonThisGame;
            [SerializeField] public int totalEnemiesKilledThisGame;
            [SerializeField] public int totalEnemiesKilledThisWave;
            [SerializeField] public int totalBossesKilledThisWave;
            [SerializeField] public int zombiesKilledThisWave;
            [SerializeField] public int femaleZombiesKilledThisWave;
            [SerializeField] public int secretEnemiesKilledThisWave;
            [SerializeField] public int skinnedKilledThisWave;
            [SerializeField] public int slugsKilledThisWave;
            [SerializeField] public int parasitesKilledThisWave;
            [SerializeField] public int spidersKilledThisWave;
            [SerializeField] public int bossesKilledThisWave;
            public int currentWave;
        }
        #region Private Variables
        [SerializeField] public PersistentStats persistentStats;
        [SerializeField] public InGameStats inGameStats;
        #endregion

        #endregion

        #region Methods

        private void OnEnable()
        {
            if (persistentStats.XPRequiredToLevelUp.Count != 256)
            {
                persistentStats.XPRequiredToLevelUp.Clear();
                for (int i = 0; i < 0256; i++)
                {
                    persistentStats.XPRequiredToLevelUp.Add(Mathf.RoundToInt(i * 1500 * i * 1.02344f));
                }
            }
        }

        private void OnDisable()
        {
            ResetInGameStats();
        }

        public void ResetInGameStats()
        {
            inGameStats.cashWonThisGame = 0;
            inGameStats.cashWonThisWave = 0;
            inGameStats.xpWonThisWave = 0;
            inGameStats.xpWonThisGame = 0;
            inGameStats.pointsWonThisGame = 0;
            inGameStats.pointsWonThisWave = 0;
            inGameStats.totalEnemiesKilledThisGame = 0;
            inGameStats.totalEnemiesKilledThisWave = 0;
            inGameStats.totalBossesKilledThisWave = 0;
            inGameStats.zombiesKilledThisWave = 0;
            inGameStats.femaleZombiesKilledThisWave = 0;
            inGameStats.secretEnemiesKilledThisWave = 0;
            inGameStats.skinnedKilledThisWave = 0;
            inGameStats.slugsKilledThisWave = 0;
            inGameStats.parasitesKilledThisWave = 0;
            inGameStats.spidersKilledThisWave = 0;
            inGameStats.currentWave = 1;
        }

        #endregion
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(PlayerStats))]
    //[CanEditMultipleObjects]

    public class CustomPlayerStatsInspector : Editor
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

            base.OnInspectorGUI();
            PlayerStats PlayerStats = (PlayerStats)target;
            //SerializedProperty example = serializedObject.FindProperty("Example");
            serializedObject.Update();

            EditorGUILayout.BeginHorizontal();

            EditorGUIUtility.labelWidth = 80;
            //EditorGUILayout.PropertyField(example);

            EditorGUILayout.EndHorizontal();


            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
    #endregion
}