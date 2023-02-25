using System.Collections.Generic;
using UnityEngine;

namespace DP.ScriptableObjects
{
    [CreateAssetMenu]
    public class PlayerInventory : ScriptableObject
    {
        [System.Serializable]public class PersistentInventory
        {
            [SerializeField] public int playerXP, playerLevel, playerCash;
            //stats

            [SerializeField] public List<Weapon> unlockedWeapons;
            [SerializeField] public List<Weapon> unlockedWeaponAttachments;
            [SerializeField] public List<Weapon> unlockedWeaponUpgrades;
            [SerializeField] public List<Weapon> unlockedWeaponSkins;
            [SerializeField] public List<Weapon> unlockedBonusWeapons;

            [SerializeField] public List<Weapon> unlockedHandsSkins;

            [SerializeField] public List<Weapon> unlockedPickups;
            [SerializeField] public List<Weapon> unlockedPowerups;
            [SerializeField] public List<Weapon> unlockedAbilities;

            [SerializeField] public List<Weapon> encounteredEnemies;
            [SerializeField] public List<Weapon> playedMaps;

            [SerializeField] public List<Weapon> unlockedEasterEggs;
            [SerializeField] public List<Weapon> unlockedGameExtras;
        }
        [System.Serializable]public class InGameInventory
        {
            [SerializeField] public Weapon equippedWeapon, holsteredWeapon;
            public Weapon transitionWeaponSlot;
            public int equippedWeaponCurrentBullets, equippedWeaponCurrentMagBullets, holsteredWeaponCurrentBullets, holsteredWeaponCurrentMagBullets;
            //[SerializeField] public WeaponUpgrade equippedWeapon, holsteredWeapon;
            [SerializeField] public Throwable throwable1, throwable2, throwable3, throwable4, throwable5;
            public int throwable1Qty, throwable2Qty, throwable3Qty, throwable4Qty, throwable5Qty;
            //[SerializeField] public HealingItem
            [SerializeField]public float bulletTimeLeft;
        }

        [SerializeField]public PersistentInventory persistentInventory;
        [SerializeField]public InGameInventory inGameInventory;
    }
}