using UnityEngine;
using UnityEngine.UI;
using DP.AI;
using DP.Gameplay;
using DP.StaticDataContainers;
using DP.ScriptableObjects;
using DP.ScriptableEvents;
using DP.ResourceManagement;
using Essentials;
using static DP.ScriptableObjects.Weapon;
using UnityEngine.Events;
using System.Collections.Generic;
using DP.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DP.Controllers
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] public UpdateBulletCount updateBulletCount;
        [SerializeField] public Transform parent;
        [System.Serializable] public class Events
        {
        [SerializeField] public UnityEvent OnTriangleSwap, OnADS, OnStoppedADS, OnShoot, OnStoppedShooting, OnItemWheelSwap, OnProjectlieApplyForce, OnReload, OnHeadshot, OnHit;
        }
        public Events events = new Events();
        public WeaponClass weaponType;
        public SelectedWeapon selectedWeapon;
        #region Variables
        /// <summary>
        /// Scriptable Object that contains the current weapon's settings
        /// </summary>
        [Tooltip("Scriptable Object that contains the current weapon's settings")]
        public Weapon equippedWeapon;

        /// <summary>
        /// Scriptable Object that contains the current holstered weapon's settings
        /// </summary>
        [Tooltip("Scriptable Object that contains the current holstered weapon's settings")]
        public Weapon holsterWeapon;

        /// <summary>
        /// Scriptable Event System that issues commands for the UI to update when needed, relative to score and cash
        /// </summary>
        [Tooltip("Scriptable Event System that issues commands for the UI to update when needed, relative to score and cash")]
        [SerializeField]PlayerStats playerStats;
        ///<summary>
        /// Scriptable Event System that issues commands for the UI to update when needed, relative to weapon stats such as ammo count
        /// </summary>
        [Tooltip("Scriptable Event System that issues commands for the UI to update when needed, relative to weapon stats such as ammo count")]
        
        [SerializeField] PlayerInventory playerInventory;

        public HeadshotSoundsContainer headshotSounds;
        
        
        /// <summary>
        /// This will define a random spread pattern depending on how long has the gun been shooting or if the character is moving
        /// </summary>
        public float spread;

        /// <summary>
        /// Returns the unmodified (crouch, run, etc) original spread value
        /// </summary>
        public float originalSpread;

        /// <summary>
        /// Check to define if the Spread will be replaced by the Reduced Spread value when the player is not moving
        /// </summary>
        public bool reducedSpreadWhenNotMoving;

        /// <summary>
        /// This will define a reduced random spread pattern if the character is not moving
        /// </summary>
        public float reducedSpread;

        /// <summary>
        /// Maximum Crosshairs size when the player is shooting or moving
        /// </summary>
        public float crosshairsMaxSize;

        /// <summary>
        /// Crosshairs expansion/compression speed
        /// </summary>
        public float crosshairsReactionSpeed;

        [Tooltip("Check to enable auto-reloading if the gun runs out of bullets")]
        [SerializeField] public bool autoReload;
        [Tooltip("If you place the Impact Effect at the same position as the hit object, you will have flickering issues, so to avoid that, set this to 0.01 or something similar")]
        [SerializeField] float impactEffectDistanceToWall = .5f;//readonly

        //Gun stats
        float timeBetweenShotsFromDifferentPresses, runningSpread, crouchSpread;
        float timeBetweenShotsOfSamePress;
        int bulletsPerBurst;

        public int bulletsPerSpreadShot;

        bool singleShotWeapon, automaticWeapon, burstWeapon;

        public bool spreadShotWeapon;

        public int bulletsToShootFromButtonPress;

        public DecalPool decalPool;

        //bools 
        public bool isShooting, readyToShoot, isReloading, isAiming, emptyGun;

        //Reference
        public RaycastHit hit;

        //Graphics
        Text bulletCounter;
        //animationcontroller
        public bool getGun, hideGun;
        public bool holdToAim = true;
        public bool shootAllowed = true;


        #region Private Variables


        /// <summary>
        /// Private Scriptable Object used to switch between equipped and holstered
        /// </summary>
        ScriptableObject transitionSO;

        /// <summary>
        ///Points deducted from enemy health on each hit
        /// </summary>
        public float damage;

        ///<summary>
        ///Weapon's effective range
        /// </summary>
        float effectiveRange;

        ///<summary>
        ///Force applied to hit object if it has a rigidbody
        /// </summary>
        float impactForce; //force when hitting rigidbodies

        /// <summary>
        /// Time it will take for this gun to reload
        /// </summary>
        float reloadTime;

        /// <summary>
        /// Number of bullets in a magazine
        /// </summary>
        int bulletsPerMagazine;
        private int totalInitBullets;

        /// <summary>
        /// Total number of bullets currently available
        /// </summary>
        public int bulletsLeftInInventory;

        /// <summary>
        /// Number of bullets left in the current magazine
        /// </summary>
        public int bulletsLeftInMag;

        //Enemies
        HealthComponent enemyHealth;
        EnemyHitReceiver enemyHitReceiver;
        EnemyStateComponent enemyState;
        Sprite weaponSprite;
        private AudioSource audioSource;
        public ImpactableLayers impactableLayers;
        [SerializeField]public float crosshairsRestingSize;
        public bool playerMoving;

        //[SerializeField] Animator hitmarker, headshotMarker;
        #endregion

        #endregion

        #region Event Methods
        public void EnableShooting()
        {
            shootAllowed = true;
        }
        public void DisableShooting()
        {
            shootAllowed = false;
        }


        public void CountHit(float dealtDamage)
        {
            playerStats.inGameStats.pointsWonThisGame += (Mathf.RoundToInt(dealtDamage));
            playerStats.inGameStats.cashWonThisGame += (Mathf.RoundToInt((dealtDamage) / 10));
            Invoke("AddPointsUIAnimation", 1.5f);
            Invoke("AddCashUIAnimation", 1.5f);
            events.OnHit.Invoke();
        }
        public void CountHeadshot(float dealtDamage)
        {
            playerStats.inGameStats.pointsWonThisGame += (Mathf.RoundToInt(dealtDamage));
            Invoke("AddPointsUIAnimation", 1.5f);
            playerStats.inGameStats.cashWonThisGame += (Mathf.RoundToInt((dealtDamage) / 10) + 10);
            Invoke("AddCashUIAnimation", 1.5f);
            events.OnHeadshot.Invoke();
        }
        public void AddPointsUIAnimation()
        {
        }
        public void AddCashUIAnimation()
        {
        }
        public void PrepareWeapon()
        {
            ResetWeaponValues();
            readyToShoot = true;
            originalSpread = spread;
        }
        void ResetWeaponValues()
        {
            weaponType = equippedWeapon.weaponClass;
            selectedWeapon = equippedWeapon.selectedWeapon;
            damage = equippedWeapon.damage;
            effectiveRange = equippedWeapon.effectiveRange;
            impactForce = equippedWeapon.impactForce;
            reloadTime = equippedWeapon.reloadTime;
            bulletsPerMagazine = equippedWeapon.bulletsPerMagazine;
            if (!equippedWeapon.hasBeenPickedUp)
            {
                bulletsLeftInMag = bulletsPerMagazine;
                totalInitBullets = equippedWeapon.totalInitBullets;
                bulletsLeftInInventory = totalInitBullets - bulletsPerMagazine;
            }
            else
            {
                bulletsLeftInMag = equippedWeapon.bulletsLeftInMag;
                bulletsLeftInInventory = equippedWeapon.bulletsLeftInInventory;
            }
            spread = equippedWeapon.spread;
            originalSpread = equippedWeapon.originalSpread;
            reducedSpreadWhenNotMoving = equippedWeapon.reducedSpreadWhenNotMoving;
            reducedSpread = equippedWeapon.reducedSpread;
            runningSpread = equippedWeapon.runningSpread;
            crouchSpread = equippedWeapon.crouchSpread;
            crosshairsMaxSize = equippedWeapon.crosshairsMaxSize;
            crosshairsReactionSpeed = equippedWeapon.crosshairsReactionSpeed;
            timeBetweenShotsFromDifferentPresses = equippedWeapon.timeBetweenShotsFromDifferentPresses;
            timeBetweenShotsOfSamePress = equippedWeapon.timeBetweenShotsOfSamePress;
            bulletsPerBurst = equippedWeapon.bulletsPerBurst;
            bulletsPerSpreadShot = equippedWeapon.bulletsPerSpreadShot;
            singleShotWeapon = equippedWeapon.singleShotWeapon;
            automaticWeapon = equippedWeapon.automaticWeapon;
            burstWeapon = equippedWeapon.burstWeapon;
            spreadShotWeapon = equippedWeapon.spreadShotWeapon;
            weaponSprite = equippedWeapon.weaponSprite;
            crosshairsRestingSize = equippedWeapon.crosshairsRestingSize;
        }
        void GetReferences()
        {
            audioSource = GetComponent<AudioSource>();
            //                bulletCounter = BulletCounter.Instance.GetComponent<Text>();
        }


        #endregion

        #region Methods
      
        void InputManagement()
        {
            if (shootAllowed)
            {

                if (autoReload && emptyGun && !isReloading && bulletsLeftInInventory > 0)
                {
                    Reload();
                }

                    Aim();


#if UNITY_ANDROID
                  if (burstWeapon || automaticWeapon) isShooting = readyToShoot && !isReloading && bulletsLeftInMag > 0 ? CF2Input.GetButton(PSVitaInputValues.R) : false;
                else isShooting = readyToShoot && !isReloading && bulletsLeftInMag > 0 ? CF2Input.GetButtonDown(PSVitaInputValues.R) : false;
                emptyGun = bulletsLeftInMag == 0 && !isReloading ? CF2Input.GetButtonDown(PSVitaInputValues.R) : false;

                if (CF2Input.GetButtonDown(PSVitaInputValues.Square) && bulletsLeftInMag < bulletsPerMagazine && !isReloading && bulletsLeftInInventory > 0) Reload();
#endif

#if UNITY_PS4
                if (burstWeapon || automaticWeaponx) isShooting = readyToShoot && !isReloading && bulletsLeftInMag > 0 ? Input.GetButton(PS4InputValues.R2p1) : false;
                else isShooting = readyToShoot && !isReloading && bulletsLeftInMag > 0 ? Input.GetButtonDown(PS4InputValues.R2p1) : false;
                emptyGun = bulletsLeftInMag == 0 && !isReloading ? Input.GetButtonDown(PS4InputValues.R2p1) : false;

                if (Input.GetButtonDown(PS4InputValues.Square1) && bulletsLeftInMag < bulletsPerMagazine && !isReloading && totalCurrentBullets > 0) Reload();
#endif

#if UNITY_PSP2
                if (burstWeapon || automaticWeapon) isShooting = readyToShoot && !isReloading && bulletsLeftInMag > 0 ? Input.GetButton(PSVitaInputValues.R) : false;
                else isShooting = readyToShoot && !isReloading && bulletsLeftInMag > 0 ? Input.GetButtonDown(PSVitaInputValues.R) : false;
                emptyGun = bulletsLeftInMag == 0 && !isReloading ? Input.GetButtonDown(PSVitaInputValues.R) : false;

                if (Input.GetButtonDown(PSVitaInputValues.Square) && bulletsLeftInMag < bulletsPerMagazine && !isReloading && bulletsLeftInInventory > 0) Reload();
#if UNITY_EDITOR
                if (Input.GetKeyDown(KeyCode.R) && bulletsLeftInMag < bulletsPerMagazine && !isReloading && bulletsLeftInInventory > 0) Reload();
#endif
#endif

#if UNITY_STANDALONE || UNITY_EDITOR
                if (burstWeapon || automaticWeapon) isShooting = readyToShoot && !isReloading && bulletsLeftInMag > 0 ? Input.GetMouseButton(0) : false;
                else isShooting = readyToShoot && !isReloading && bulletsLeftInMag > 0 ? Input.GetMouseButtonDown(0) : false;
                emptyGun = bulletsLeftInMag == 0 && !isReloading ? Input.GetMouseButtonDown(0) : false;

                if (Input.GetKeyDown(KeyCode.R) && bulletsLeftInMag < bulletsPerMagazine && !isReloading && bulletsLeftInInventory > 0) Reload();
#endif

#if UNITY_WINRT

#endif

#if UNITY_XBOXONE

#endif



                //Shoot
                if (readyToShoot && isShooting && !isReloading && bulletsLeftInMag > 0)
                {
                    bulletsToShootFromButtonPress = bulletsPerBurst;
                    Shoot();
                }
                if (emptyGun)
                    audioSource.PlayOneShot(equippedWeapon.weaponSounds.emptyMagSounds[UnityEngine.Random.Range(0, equippedWeapon.weaponSounds.emptyMagSounds.Length)]);
            }
        }

        void Shoot()
        {
            
            readyToShoot = false;

            //To OnShot, via AVFX
            //audioSource.PlayOneShot(equippedWeapon.weaponSounds.shootSounds[UnityEngine.Random.Range(0, equippedWeapon.weaponSounds.shootSounds.Length)]);

#if !UNITY_PSP2
            events.OnProjectlieApplyForce.Invoke(); 
#endif

            float xSpread = UnityEngine.Random.Range(-spread, spread);
            float ySpread = UnityEngine.Random.Range(-spread, spread);
            //RaycastHit debugHit;
            Vector3 spreadV3 = parent.forward + new Vector3(xSpread, ySpread, 0);
            //Physics.Raycast(parent.position, spreadV3, out debugHit, Mathf.Infinity);
            //Debug.DrawRay(parent.position, parent.forward * 100, Color.red);

            #region Enemy Impacts
            if (Physics.Raycast(parent.position, parent.forward, out hit, effectiveRange, impactableLayers.enemyBitMasks.headLayer))
            {
                HandleImpact(spreadV3, DamageMultiplier.headDamageMultiplier);
            }
            if (Physics.Raycast(parent.position, spreadV3, out hit, effectiveRange, impactableLayers.enemyBitMasks.neckLayer))
            {
                HandleImpact(spreadV3, DamageMultiplier.neckDamageMultiplier);                
            }
            if (Physics.Raycast(parent.position, spreadV3, out hit, effectiveRange, impactableLayers.enemyBitMasks.chestLayer))
            {
                HandleImpact(spreadV3, DamageMultiplier.chestDamageMultiplier);                
            }
            if (Physics.Raycast(parent.position, spreadV3, out hit, effectiveRange, impactableLayers.enemyBitMasks.armLayer))
            {
                HandleImpact(spreadV3, DamageMultiplier.legDamageMultiplier);                
            }
            if (Physics.Raycast(parent.position, spreadV3, out hit, effectiveRange, impactableLayers.enemyBitMasks.forearmLayer))
            {
                HandleImpact(spreadV3, DamageMultiplier.upLegDamageMultiplier);                
            }
            if (Physics.Raycast(parent.position, spreadV3, out hit, effectiveRange, impactableLayers.enemyBitMasks.torsoLayer))
            { 
                HandleImpact(spreadV3, DamageMultiplier.torsoDamageMultiplier);                
            }
            if (Physics.Raycast(parent.position, spreadV3, out hit, effectiveRange, impactableLayers.enemyBitMasks.hipsLayer))
            {
                HandleImpact(spreadV3, DamageMultiplier.hipsDamageMultiplier);                
            }
            if (Physics.Raycast(parent.position, spreadV3, out hit, effectiveRange, impactableLayers.enemyBitMasks.legLayer))
            {
                HandleImpact(spreadV3, DamageMultiplier.hipsDamageMultiplier);
                ImpactBehaviour(spreadV3);
                LegHitBehaviour();
            }
            if (Physics.Raycast(parent.position, spreadV3, out hit, effectiveRange, impactableLayers.enemyBitMasks.upLegLayer))
            {
                ImpactBehaviour(spreadV3);
                LegHitBehaviour();                
            }
            #endregion
            #region Surface Bullet Impact Decals
               if (Physics.Raycast(parent.position, spreadV3, out hit, effectiveRange, impactableLayers.environmentBitMasks.woodLayer))
               {
                var woodDecal = decalPool.GetWoodDecal();
                    woodDecal.transform.parent = hit.transform;
                    woodDecal.transform.SetPositionAndRotation(hit.point + hit.normal * impactEffectDistanceToWall, Quaternion.LookRotation(hit.normal));
                    woodDecal.SetActive(true);
                //Debug.DrawRay(hit.point, spreadV3);
            }
               if (Physics.Raycast(parent.position, spreadV3, out hit, effectiveRange, impactableLayers.environmentBitMasks.waterLayer))
               {
                   //equippedWeapon.weaponSounds.waterImpactFx.transform.SetPositionAndRotation(hit.point + hit.normal * impactEffectDistanceToWall, Quaternion.LookRotation(hit.normal));
                   //Weapon_VFX.Instance.waterImpactFx.SetActive(true);
               }
               if (Physics.Raycast(parent.position, spreadV3, out hit, effectiveRange, impactableLayers.environmentBitMasks.concreteLayer))
               {
                   var concreteDecal = decalPool.GetConcreteDecal();
                   concreteDecal.transform.SetPositionAndRotation(hit.point + hit.normal * impactEffectDistanceToWall, Quaternion.LookRotation(-hit.normal));
                concreteDecal.transform.parent = hit.transform; 
                   concreteDecal.SetActive(true);
                   //Weapon_VFX.Instance.concreteImpactFx.transform.SetPositionAndRotation(hit.point + hit.normal * impactEffectDistanceToWall, Quaternion.LookRotation(hit.normal));
                   //Weapon_VFX.Instance.concreteImpactFx.SetActive(true);
               }
               if (Physics.Raycast(parent.position, spreadV3, out hit, effectiveRange, impactableLayers.environmentBitMasks.metalLayer))
               {
                   var metalDecal = decalPool.GetMetalDecal();
                metalDecal.transform.parent = hit.transform;
                   metalDecal.transform.SetPositionAndRotation(hit.point + hit.normal * impactEffectDistanceToWall, Quaternion.LookRotation(hit.normal));
                   metalDecal.SetActive(true);
                   //Weapon_VFX.Instance.metalImpactFx.transform.SetPositionAndRotation(hit.point + hit.normal * impactEffectDistanceToWall, Quaternion.LookRotation(hit.normal));
                   //Weapon_VFX.Instance.metalImpactFx.SetActive(true);
               }
            #endregion
            
               //To Onshot
               //OnPlayMuzzleFlash.Invoke();
            bulletsLeftInMag--;
            bulletsToShootFromButtonPress--;

            equippedWeapon.bulletsLeftInMag = bulletsLeftInMag;
            playerInventory.inGameInventory.equippedWeaponCurrentBullets = bulletsLeftInInventory;
            playerInventory.inGameInventory.equippedWeaponCurrentMagBullets = bulletsLeftInMag;
            events.OnShoot.Invoke();
            PlayMuzzleFlash();
            audioSource.PlayOneShot( equippedWeapon.weaponSounds.shootSounds[Random.Range(0, equippedWeapon.weaponSounds.shootSounds.Length)]);
            Invoke("ResetShot", timeBetweenShotsFromDifferentPresses);

            if (bulletsToShootFromButtonPress > 0 && bulletsLeftInMag > 0)
                Invoke("Shoot", timeBetweenShotsOfSamePress);
        }

        private void HandleImpact(Vector3 spreadV3, float damageMultiplier)
        {
            ImpactBehaviour(spreadV3);
            float dealtDamage = damage * damageMultiplier;
            HeadshotDamage(dealtDamage);
        }

        private void LegHitBehaviour()
        {
            if (enemyHealth.currentHealth < 35)
            {
                float dealtDamage = damage * DamageMultiplier.legDamageMultiplier;
                enemyHealth.TakeDamage((int)dealtDamage);
                enemyState.isHurt = true;
                CountHit(dealtDamage);

                var bloodFX = decalPool.GetBloodFX();
                bloodFX.transform.parent = hit.transform;
                bloodFX.transform.SetPositionAndRotation(hit.point + hit.normal * impactEffectDistanceToWall, Quaternion.LookRotation(hit.normal));
                bloodFX.SetActive(true);
                //PopupAnimation(dealtDamage);

                //audioSource.PlayOneShot(fleshImpactSounds[UnityEngine.Random.Range(0, fleshImpactSounds.Length)]);
                //if (Debugging.DebugSymbols.logDebugData)
                //{
                //    Debug.Log("enemy took " + damage + " points of damage, multiplied by " + DamageMultiplier.upLegDamageMultiplier);
                //    Debug.Log("player got " + (Mathf.RoundToInt(dealtDamage)) + " Points");
                //    Debug.Log("player got " + (Mathf.RoundToInt((dealtDamage) / 10)) + " Cash");
                //}
            }
            else
            {
                float dealtDamage = damage * DamageMultiplier.upLegDamageMultiplier;
                enemyHealth.TakeDamage((int)dealtDamage);
                CountHit(dealtDamage);

                var bloodFX = decalPool.GetBloodFX();
                bloodFX.transform.parent = hit.transform;
                bloodFX.transform.SetPositionAndRotation(hit.point + hit.normal * impactEffectDistanceToWall, Quaternion.LookRotation(hit.normal));
                bloodFX.SetActive(true);
                //PopupAnimation(dealtDamage);

                //
                //audioSource.PlayOneShot(fleshImpactSounds[UnityEngine.Random.Range(0, equippedWeapon.weaponSounds.headshotSounds.Length)]);
                //if (Debugging.DebugSymbols.logDebugData)
                //{
                //    Debug.Log("enemy took " + damage + " points of damage, multiplied by " + DamageMultiplier.upLegDamageMultiplier);
                //    Debug.Log("player got " + (Mathf.RoundToInt(dealtDamage)) + " Points");
                //    Debug.Log("player got " + (Mathf.RoundToInt((dealtDamage) / 10)) + " Cash");
                //}

            }
        }

        private void HitBehaviour(float dealtDamage)
        {
            enemyHealth.TakeDamage((int)dealtDamage);
            CountHit(dealtDamage);
            //PopupAnimation(dealtDamage);

            //audioSource.PlayOneShot(headshotSounds.headshotSounds[Random.Range(0, headshotSounds.headshotSounds.Length)]);
#if UNITY_EDITOR
            {
                Debug.Log("enemy took " + damage + " points of damage, multiplied by " + DamageMultiplier.neckDamageMultiplier);
                Debug.Log("player got " + (Mathf.RoundToInt(dealtDamage)) + " Points");
                Debug.Log("player got " + (Mathf.RoundToInt((dealtDamage) / 10)) + " Cash");
            }
#endif
        }

        private void HeadshotDamage(float dealtDamage)
        {
            enemyHealth.TakeDamage((int)dealtDamage);
            CountHeadshot(dealtDamage);
            //if(!audioSource.isPlaying)
            //audioSource.PlayOneShot(headshotSounds.headshotSounds[Random.Range(0, headshotSounds.headshotSounds.Length)]);
            //if (Debugging.DebugSymbols.logDebugData)
            //{
            //    Debug.Log("enemy took " + damage + " points of damage, multiplied by " + DamageMultiplier.headDamageMultiplier);
            //    Debug.Log("player got " + (Mathf.RoundToInt(dealtDamage)) + " Points");
            //    Debug.Log("player got " + (Mathf.RoundToInt((dealtDamage) / 10) + 10) + " Cash");
            //}
        }

        private void ImpactBehaviour(Vector3 spreadV3)
        {
#if UNITY_EDITOR
            Debug.DrawRay(spreadV3, hit.point);
#endif
            enemyHitReceiver = hit.transform.GetComponent<EnemyHitReceiver>();
            enemyState = enemyHitReceiver.enemyState;
            enemyHealth = enemyHitReceiver.enemyHealth;

            var bloodFX = decalPool.GetBloodFX();
            bloodFX.transform.parent = hit.transform;
            bloodFX.transform.SetPositionAndRotation(hit.point + hit.normal * impactEffectDistanceToWall, Quaternion.LookRotation(hit.normal));
            bloodFX.SetActive(true);
        }

        public Transform bulletSpawn;
        public List<GameObject> spawnedMuzzleFlashes = new List<GameObject>();

        int spawnedMuzzleFlashesLength;

        private void SpawnMuzzleFlashes()
        {
            equippedWeapon.muzzleFlashesCount = equippedWeapon.muzzleFlashes.Length;
            for (int i = 0; i < equippedWeapon.muzzleFlashesCount; i++)
            {
                var mf = Instantiate((GameObject)equippedWeapon.muzzleFlashes[i],bulletSpawn);
                mf.SetActive(false);
                spawnedMuzzleFlashes.Add(mf);
                spawnedMuzzleFlashesLength += 1;
            }
        }

        public void PlayMuzzleFlash() { spawnedMuzzleFlashes[Random.Range(0, spawnedMuzzleFlashesLength)].SetActive(true);  }

        private void Aim()
        {
            if (holdToAim)
            {
#if UNITY_ANDROID
                isAiming = CF2Input.GetButton(PSVitaInputValues.L);
#endif

#if UNITY_PS4
                isAiming = Input.GetButton(PS4InputValues.L2p1);
#endif

#if UNITY_PSP2
                isAiming = Input.GetButton(PSVitaInputValues.L);
#endif

#if UNITY_STANDALONE || UNITY_EDITOR
                isAiming = Input.GetMouseButton(1);
#endif

#if UNITY_WINRT

#endif

#if UNITY_XBOXONE

#endif
                if(isAiming)
                {
                    events.OnADS.Invoke();
                }
                else
                {
                    events.OnStoppedADS.Invoke();
                }
            }
            else
            {
                if (!isAiming)
                {

#if UNITY_ANDROID
                    if (CF2Input.GetButtonDown(PSVitaInputValues.L))
                    {
                        isAiming = true;
                    events.OnADS.Invoke();
                    }
#endif

#if UNITY_PS4
                    if (Input.GetButtonDown(PS4InputValues.L2p1))
                    {
                        isAiming = true;
                    events.OnADS.Invoke();
                    }
#endif

#if UNITY_PSP2
                    if (Input.GetButtonDown(PSVitaInputValues.L))
                    {
                        isAiming = true;
                    events.OnADS.Invoke();
                    }
#endif

#if UNITY_STANDALONE || UNITY_EDITOR
                    if (Input.GetMouseButtonDown(1))
                    {
                        isAiming = true;
                    events.OnADS.Invoke();
                    }

#endif

#if UNITY_WINRT

#endif

#if UNITY_XBOXONE

#endif
                }
                else
                {
#if UNITY_ANDROID
                       if (CF2Input.GetButtonDown(PSVitaInputValues.L))
                    {
                        isAiming = isAiming ? Input.GetButtonDown(PSVitaInputValues.L) ? false : true : false;
                        isAiming = false;
                        events.OnStoppedADS.Invoke();
                    }
#endif

#if UNITY_PS4
                       if (Input.GetButtonDown(PS4InputValues.L2p1))
                    {
                        isAiming = isAiming ? Input.GetButtonDown(PSVitaInputValues.L) ? false : true : false;
                        isAiming = false;
                        events.OnStoppedADS.Invoke();
                    }
#endif

#if UNITY_PSP2
                    if (Input.GetButtonDown(PSVitaInputValues.L))
                    {
                        isAiming = isAiming ? Input.GetButtonDown(PSVitaInputValues.L) ? false : true : false;
                        isAiming = false;
                        events.OnStoppedADS.Invoke();
                    }
#endif

#if UNITY_STANDALONE || UNITY_EDITOR
                    if (Input.GetMouseButtonDown(1))
                    {
                        isAiming = isAiming ? Input.GetMouseButtonDown(1) ? false : true : false;
                        isAiming = false;
                        events.OnStoppedADS.Invoke();
                    }
#endif

#if UNITY_WINRT

#endif

#if UNITY_XBOXONE

#endif


                }
            }
        }

        private void ResetShot()
        {
            readyToShoot = true;
        }
        private void Reload()
        {
            isReloading = true;
            audioSource.PlayOneShot(equippedWeapon.weaponSounds.reloadSounds[Random.Range(0, equippedWeapon.weaponSounds.reloadSounds.Length)]);
            Invoke("ReloadFinished", reloadTime);
        }

        public void ReloadFinished()
        {
            if (bulletsLeftInInventory > bulletsPerMagazine)
            {
                if (bulletsLeftInMag != 0)
                {
                    int bulletsReloaded = bulletsPerMagazine - bulletsLeftInMag;
                    bulletsLeftInMag = bulletsLeftInMag + bulletsReloaded;
                    bulletsLeftInInventory -= bulletsReloaded;
                }
                if (bulletsLeftInMag == 0)
                {
                    bulletsLeftInMag = bulletsPerMagazine;
                    bulletsLeftInInventory -= bulletsPerMagazine;
                }
            }
            if (bulletsLeftInInventory <= bulletsPerMagazine)
            {
                if (bulletsLeftInMag != 0)
                {
                    int requiredBulletsToFillCharger = bulletsPerMagazine - bulletsLeftInMag;
                    if (bulletsLeftInInventory >= requiredBulletsToFillCharger)
                    {
                        bulletsLeftInMag += requiredBulletsToFillCharger;
                        bulletsLeftInInventory -= requiredBulletsToFillCharger;
                    }
                    else
                    {

                        bulletsLeftInMag += bulletsLeftInInventory;
                        bulletsLeftInInventory = 0;
                    }

                }
                if (bulletsLeftInMag == 0)
                {
                    bulletsLeftInMag = bulletsLeftInInventory;
                    bulletsLeftInInventory = 0;

                }
            }
            if (bulletsLeftInInventory == 0 && bulletsLeftInMag == 0)
            {
                //PointsPopup.Instance.text = "NO AMMO!";
                //                    //PointsPopupPool.Instance.gameObject.SetActive(true);
            }
            equippedWeapon.bulletsLeftInMag = bulletsLeftInMag;
            playerInventory.inGameInventory.equippedWeaponCurrentBullets = bulletsLeftInInventory;
            playerInventory.inGameInventory.equippedWeaponCurrentMagBullets = bulletsLeftInMag;
            isReloading = false;

            events.OnReload.Invoke();

        }

        public void SetPlayerMovingTrue() { playerMoving = true; }
        public void SetPlayerMovingFalse() { playerMoving = false; }

        private void SpreadManagement()
        {
            if (!playerMoving && !isAiming)
            {
                if (reducedSpreadWhenNotMoving)
                {
                    spread = reducedSpread;
                }
                else
                {
                    spread = originalSpread;
                }
            }
            else if (!isAiming && playerMoving)
            {
                spread = originalSpread * 1.5f;
            }
            else if (isAiming && playerMoving)
            {
                spread = originalSpread * 0.7f;
            }
            else if (isAiming && !playerMoving)
            {
                spread = originalSpread * 0.4f;
            }
        }
        #endregion
        #region Unity Callbacks
        void Awake()
        {
            GetReferences();
        }
        private void Start()
        {
            SpawnMuzzleFlashes();
            equippedWeapon.bulletsLeftInMag = bulletsLeftInMag;
            playerInventory.inGameInventory.equippedWeaponCurrentBullets = bulletsLeftInInventory;
            playerInventory.inGameInventory.equippedWeaponCurrentMagBullets = bulletsLeftInMag;
        }
        void Update()
        {

            //float xSpread = UnityEngine.Random.Range(-spread, spread);
            //float ySpread = UnityEngine.Random.Range(-spread, spread);
            //Vector3 spreadV3 = parent.forward + new Vector3(xSpread, ySpread, 0);
            //Debug.DrawRay(parent.position, spreadV3 * 100, Color.red);

            InputManagement();
            SpreadManagement();
        }

        private void DebugRay(RaycastHit hit)
        {
            //Debug.DrawRay(parent.position, hit.point * 100, Color.red);
        }

        private void OnDrawGizmos()
        {
            float xSpread = UnityEngine.Random.Range(-spread, spread);
            float ySpread = UnityEngine.Random.Range(-spread, spread);

            Vector3 spreadV3 = parent.forward + new Vector3(xSpread, ySpread, 0);
            Gizmos.DrawRay(parent.position, spreadV3);
        }
        #endregion
    }
 
    /*
     * #region Custom inspector
#if UNITY_EDITOR
    [CustomEditor(typeof(WeaponController))]
    public class CustomWeaponControllerInspector : Editor
    {
        bool paused = false, resumed;
        private bool gameOver;
        private bool photoMode;

        public override void OnInspectorGUI()
        {
            #region Prepare


            //Define
            GUIStyle header = new GUIStyle();
            GUIStyle leftAlignedHeader = new GUIStyle();
            GUIStyle blackButton = new GUIStyle();
            GUIStyle whiteButton = new GUIStyle();
            GUIStyle redButton = new GUIStyle();
            GUIStyle subclass = new GUIStyle();
            Font font;

            //Get Font
            font = (Font)Resources.Load(ResourcesPathContainer.oswaldFontPath);
            //Set Header GUIStyle
            header.font = font;
            header.fontSize = 20;
            header.fontStyle = FontStyle.Bold;
            header.alignment = TextAnchor.MiddleCenter;
            header.normal.textColor = Color.white;
            //Set Header GUIStyle
            leftAlignedHeader.font = font;
            leftAlignedHeader.fontSize = 18;
            leftAlignedHeader.alignment = TextAnchor.MiddleCenter;
            leftAlignedHeader.fontStyle = FontStyle.Bold;
            leftAlignedHeader.normal.textColor = Color.white;
            //Set Black Button GUIStyle
            blackButton.font = font;
            blackButton.fontSize = 16;
            blackButton.fontStyle = FontStyle.Normal;
            blackButton.alignment = TextAnchor.MiddleCenter;
            blackButton.normal.textColor = Colors.GhostWhite;
            blackButton.active.textColor = Colors.GhostWhite;
            blackButton.normal.background = (Texture2D)Resources.Load(ResourcesPathContainer.editorButtonBlack);
            blackButton.active.background = (Texture2D)Resources.Load(ResourcesPathContainer.editorButtonBlackClick);

            //Set white Button GUIStyle
            whiteButton.font = font;
            whiteButton.fontSize = 13;
            whiteButton.fontStyle = FontStyle.Bold;
            whiteButton.alignment = TextAnchor.MiddleCenter;
            whiteButton.normal.textColor = Colors.Black;
            whiteButton.active.textColor = Colors.DarkGray;
            whiteButton.normal.background = (Texture2D)Resources.Load(ResourcesPathContainer.editorButtonWhite);
            whiteButton.active.background = (Texture2D)Resources.Load(ResourcesPathContainer.editorButtonWhiteClick);

            //Set Button GUIStyle
            redButton.font = font;
            redButton.fontSize = 13;
            redButton.fontStyle = FontStyle.Bold;
            redButton.alignment = TextAnchor.MiddleCenter;
            redButton.normal.textColor = Color.white;
            redButton.normal.background = (Texture2D)Resources.Load(ResourcesPathContainer.editorButtonRed);
            redButton.active.background = (Texture2D)Resources.Load(ResourcesPathContainer.editorButtonRedClick);


            //Set Subclass GUIStyle
            subclass.font = font;
            subclass.fontSize = 15;
            subclass.alignment = TextAnchor.MiddleCenter;
            subclass.normal.textColor = Color.white;

            #endregion

            #region GetProperties

            //Set Target
            WeaponController weaponController = (WeaponController)target;

            SerializedProperty pses = serializedObject.FindProperty("playerStatsEventSystem");
            SerializedProperty wes = serializedObject.FindProperty("weaponEventSystem");
            SerializedProperty damage = serializedObject.FindProperty("damage");
            SerializedProperty equipped = serializedObject.FindProperty("equippedWeapon");
            SerializedProperty effectiveRange = serializedObject.FindProperty("effectiveRange");
            SerializedProperty reloadTime = serializedObject.FindProperty("reloadTime");
            SerializedProperty impactForce = serializedObject.FindProperty("impactForce");
            SerializedProperty bulletsPerMagazine = serializedObject.FindProperty("bulletsPerMagazine");
            SerializedProperty totalInitBullets = serializedObject.FindProperty("totalInitBullets");
            SerializedProperty bulletsLeftInMag = serializedObject.FindProperty("bulletsLeftInMag");
            SerializedProperty spread = serializedObject.FindProperty("spread");
            SerializedProperty originalSpread = serializedObject.FindProperty("originalSpread");
            SerializedProperty reducedSpreadWhenNotMoving = serializedObject.FindProperty("reducedSpreadWhenNotMoving");
            SerializedProperty reducedSpread = serializedObject.FindProperty("reducedSpread");
            SerializedProperty runningSpread = serializedObject.FindProperty("runningSpread");
            SerializedProperty crouchSpread = serializedObject.FindProperty("crouchSpread");
            SerializedProperty crosshairsMaxSize = serializedObject.FindProperty("crosshairsMaxSize");
            SerializedProperty crosshairsReactionSpeed = serializedObject.FindProperty("crosshairsReactionSpeed");
            SerializedProperty timeBetweenShotsFromDifferentPresses = serializedObject.FindProperty("timeBetweenShotsFromDifferentPresses");
            SerializedProperty timeBetweenShotsOfSamePress = serializedObject.FindProperty("timeBetweenShotsOfSamePress");
            SerializedProperty bulletsPerBurst = serializedObject.FindProperty("bulletsPerBurst");
            SerializedProperty bulletsPerSpreadShot = serializedObject.FindProperty("bulletsPerSpreadShot");
            SerializedProperty singleShotWeapon = serializedObject.FindProperty("singleShotWeapon");
            SerializedProperty automaticWeapon = serializedObject.FindProperty("automaticWeapon");
            SerializedProperty burstWeapon = serializedObject.FindProperty("burstWeapon");
            SerializedProperty spreadWeapon = serializedObject.FindProperty("spreadWeapon");
            SerializedProperty weaponSprite = serializedObject.FindProperty("weaponSprite");
            #endregion

            GUILayout.Space(16);
            EditorGUILayout.LabelField("Weapon Controller", header);
            GUILayout.Space(10);
            #region Player Stats ES Object
            serializedObject.Update();
            if (weaponController.playerStatsEventSystem == null)
            {
                EditorGUILayout.HelpBox("If you haven't created a Player Stats Scriptable Event System, go to Project view, right click and select Create>Scriptable Event Systems/Player Stats Event System and assign it to this controller and the respective listeners", MessageType.Info);
                EditorGUILayout.HelpBox("Please assign the Pkayer Stats Scriptable Event System (there should be only one per project).\n \nOnce assigned, the field will disappear. If the selected Scriptable Object goes missing, this warning and the respective field will popup again.", MessageType.Warning);
                EditorGUILayout.PropertyField(pses);
            }
            if (weaponController.weaponUIEventSystem == null)
            {
                EditorGUILayout.HelpBox("If you haven't created a Weapon Scriptable Event System, go to Project view, right click and select Create>Scriptable Event Systems/Weapon Event System and assign it to this controller and the respective listeners", MessageType.Info);
                EditorGUILayout.HelpBox("Please assign the Weapon Scriptable Event System (there should be only one per project).\n \nOnce assigned, the field will disappear. If the selected Scriptable Object goes missing, this warning and the respective field will popup again.", MessageType.Warning);
                EditorGUILayout.PropertyField(wes);
            }
            EditorGUILayout.PropertyField(equipped);
            if(!weaponController.equippedWeapon.initialized)
            {
                EditorGUILayout.HelpBox("The Weapon Archetype used on this controller has not been configured, please select it and finish configuring it.", MessageType.Error);
            }
            if (!weaponController.equippedWeapon.custom)
            {
                GUILayout.Label($"Equipped: {weaponController.equippedWeapon.selectedWeapon}, {weaponController.equippedWeapon.weaponClass}", leftAlignedHeader);
            }
            else
            {
                GUILayout.Label($"Equipped: {weaponController.equippedWeapon.weaponName}, {weaponController.equippedWeapon.weaponClass}", leftAlignedHeader);
            }
            GUILayout.Label($"Base Damage: {weaponController.equippedWeapon.damage} points per bullet", subclass);
            GUILayout.Label($"Effective Range: up to {weaponController.equippedWeapon.effectiveRange} meters", subclass);
            GUILayout.Label($"Reload time: {weaponController.equippedWeapon.reloadTime} seconds", subclass);
            GUILayout.Label($"Physics impact force: {weaponController.equippedWeapon.impactForce} units", subclass);
            GUILayout.Label($"Magazine size: {weaponController.equippedWeapon.bulletsPerMagazine} bullets", subclass);
            GUILayout.Label($"Initial bullets: {weaponController.equippedWeapon.totalInitBullets} bullets", subclass);
            GUILayout.Label($"Bullets left in magazine: {weaponController.equippedWeapon.bulletsLeftInMag} bullets", subclass);
            GUILayout.Label($"Base accuracy reduction: {weaponController.equippedWeapon.spread} points", subclass);
            GUILayout.Label($"Accuracy increased when not moving: {weaponController.equippedWeapon.reducedSpreadWhenNotMoving}", subclass);
            GUILayout.Label($"Accuracy recuction when not moving: {weaponController.equippedWeapon.reducedSpread} units", subclass);
            GUILayout.Label($"Accuracy reduction when running: {weaponController.equippedWeapon.runningSpread} units", subclass);
            GUILayout.Label($"Accuracy reduction when crouching: {weaponController.equippedWeapon.crouchSpread} units", subclass);
            GUILayout.Label($"Crosshairs max size: {weaponController.equippedWeapon.crosshairsMaxSize} pixels", subclass);
            GUILayout.Label($"Crosshairs compression/expansion speed: {weaponController.equippedWeapon.crosshairsReactionSpeed}", subclass);
            GUILayout.Label($"Time between shots/burst from different presses/on hold: {weaponController.equippedWeapon.timeBetweenShotsFromDifferentPresses} seconds", subclass);
            GUILayout.Label($"Shots fired per burst: {weaponController.equippedWeapon.bulletsPerBurst}", subclass);
            GUILayout.Label($"Bullets shot per spread shot: {weaponController.equippedWeapon.bulletsPerSpreadShot}", subclass);
            GUILayout.Label($"Time between burst shots: {weaponController.equippedWeapon.timeBetweenShotsOfSamePress} seconds", subclass);
            GUILayout.Label($"Can fire in Single Mode: {weaponController.equippedWeapon.singleShotWeapon}", subclass);
            GUILayout.Label($"Can fire in Auto Mode: {weaponController.equippedWeapon.automaticWeapon}", subclass);
            GUILayout.Label($"Can fire in Burst Mode: {weaponController.equippedWeapon.burstWeapon}", subclass);
            GUILayout.Label($"Can fire in Spread Mode: {weaponController.equippedWeapon.spreadShotWeapon}", subclass);
            //GUILayout.Label($"Base Damage: {weaponController.equippedWeapon.damage} points per pellet", subclass);
            serializedObject.ApplyModifiedProperties();

            #endregion
            #region Event Behavior Testing
            /*
            EditorGUILayout.LabelField("Event Behavior Testing", leftAlignedHeader);
            GUILayout.Space(12);
            serializedObject.Update();
            #region Cash
            GUILayout.BeginHorizontal();
            GUILayout.Label("Cash", subclass);
            GUILayout.FlexibleSpace();
            var cashToAdd = EditorGUILayout.IntField(1, CommonGUILayoutOptions.width32);
            if (GUILayout.Button("Add", blackButton, CommonGUILayoutOptions.width40, CommonGUILayoutOptions.height20))
            {
            }
            if (GUILayout.Button("Subtract", blackButton, CommonGUILayoutOptions.width96, CommonGUILayoutOptions.height20))
            {
            }
            if (GUILayout.Button("Reset", blackButton, CommonGUILayoutOptions.width48, CommonGUILayoutOptions.height20))
            {
            }
            GUILayout.EndHorizontal();
            #endregion
            #region Points
            GUILayout.BeginHorizontal();
            GUILayout.Label("Points", subclass);
            GUILayout.FlexibleSpace();
            var pointsToAdd = EditorGUILayout.IntField(1, CommonGUILayoutOptions.width32);
            if (GUILayout.Button("Add", blackButton, CommonGUILayoutOptions.width40, CommonGUILayoutOptions.height20))
            {
            }
            if (GUILayout.Button("Subtract", blackButton, CommonGUILayoutOptions.width96, CommonGUILayoutOptions.height20))
            {
            }
            if (GUILayout.Button("Reset", blackButton, CommonGUILayoutOptions.width48, CommonGUILayoutOptions.height20))
            {
            }
            GUILayout.EndHorizontal();
            #endregion
            #region Killed
            GUILayout.BeginHorizontal();
            GUILayout.Label("Killed", subclass);
            GUILayout.FlexibleSpace();
            var killedToAdd = EditorGUILayout.IntField(1, CommonGUILayoutOptions.width32);
            if (GUILayout.Button("Add", blackButton, CommonGUILayoutOptions.width40, CommonGUILayoutOptions.height20))
            {
            }
            if (GUILayout.Button("Subtract", blackButton, CommonGUILayoutOptions.width96, CommonGUILayoutOptions.height20))
            {
            }
            if (GUILayout.Button("Reset", blackButton, CommonGUILayoutOptions.width48, CommonGUILayoutOptions.height20))
            {
            }
            GUILayout.EndHorizontal();
            #endregion
            #region Wave
            GUILayout.BeginHorizontal();
            GUILayout.Label("Wave", subclass);
            GUILayout.FlexibleSpace();
            var waveToAdd = EditorGUILayout.IntField(1, CommonGUILayoutOptions.width32);
            if (GUILayout.Button("Add", blackButton, CommonGUILayoutOptions.width40, CommonGUILayoutOptions.height20))
            {
            }
            if (GUILayout.Button("Subtract", blackButton, CommonGUILayoutOptions.width96, CommonGUILayoutOptions.height20))
            {
            }
            if (GUILayout.Button("Reset", blackButton, CommonGUILayoutOptions.width48, CommonGUILayoutOptions.height20))
            {
            }
            GUILayout.EndHorizontal();
            #endregion 
            serializedObject.ApplyModifiedProperties();
            #endregion
            GUILayout.Space(4);

            EditorGUILayout.HelpBox("This Controller sends orders to relative to UI content from the Game Manager's current Player Stats, allowing to update specific Text Elements based on player interaction, e.g Add, Subtract and Reset Cash, Points and Wave number.\n \nIt also allows to debug and test behaviors without entering Play Mode.", MessageType.Info);
        }
    }
#endif

    #endregion*/
}