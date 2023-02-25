using UnityEngine;
using DP.ResourceManagement;
using Essentials;
using DP.Gameplay;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DP.ScriptableObjects
{
    /// <summary>
    /// Weapon class that defines a weapon's properties such as damage, effective range, reloading time, and more.
    /// </summary>
    [CreateAssetMenu(menuName = "Archetypes/Weapon", order = 1)]
    public class Weapon : ScriptableObject
    {

        /// <summary>
        /// Indicates whether the weapon has been initialized.
        /// </summary>
        public bool initialized;

        /// <summary>
        /// Enumeration of the different classes of weapons.
        /// </summary>
        public enum WeaponClass
        {
            Pistol = 0,
            SMG = 1,
            AR = 2,
            Sniper = 3,
            DMR = 4,
            LMG = 5,
            Special = 6,
            Secret = 7,
            Undefined_Class = 50
        }

        /// <summary>
        /// Enumeration of the different selected weapons.
        /// </summary>
        public enum SelectedWeapon
        {
            M1911 = 0,
            Glock = 1,
            Magnum = 2,
            MP5 = 3,
            Uzi = 4,
            SCARL = 5,
            SCARH = 6,
            AR15 = 7,
            AWM = 8,
            DMR = 9,
            LMG = 10,
            GrenadeLauncher = 11,
            Minigun = 12,
            Railgun = 13,
            Custom = 50,
            Undefined_Weapon = 51
        }

        /// <summary>
        /// Enumeration of the different sprite sizes.
        /// </summary>
        public enum SpriteSize { Small = 0, Large = 1 }

        /// <summary>
        /// Indicates the selected weapon.
        /// </summary>
        [Tooltip("Indicates the selected weapon.")]
        [SerializeField] public SelectedWeapon selectedWeapon = (SelectedWeapon)51;

        /// <summary>
        /// Indicates whether the weapon is custom or not.
        /// </summary>
        [Tooltip("Indicates whether the weapon is custom or not.")]
        public bool custom;

        /// <summary>
        /// Name of the custom weapon.
        /// </summary>
        [Tooltip("Name your custom weapon.")]
        public string weaponName = "Custom Weapon";

        /// <summary>
        /// Class of the custom weapon.
        /// </summary>
        [Tooltip("Define a Class for your custom weapon.")]
        public WeaponClass weaponClass = (WeaponClass)50;

        /// <summary>
        /// Size of the weapon sprite.
        /// </summary>
        [Tooltip("Size of the weapon sprite.")]
        public SpriteSize spriteSize;

        /// <summary>
        /// Damage caused by each bullet hit.
        /// </summary>
        [Tooltip("Points deducted from enemy health on each hit.")]
        public int damage;

        /// <summary>
        /// Effective range of the weapon.
        /// </summary>
        [Tooltip("Weapon's effective range.")]
        public float effectiveRange;

        /// <summary>
        /// Force applied to hit object if it has a rigidbody.
        /// </summary>
        [Tooltip("Force applied to hit object if it has a rigidbody.")]
        public float impactForce;

        /// <summary>
        /// Time it takes to reload the weapon.
        /// </summary>
        [Tooltip("Time it will take for this gun to reload.")]
        public float reloadTime;

        /// <summary>
        /// Number of bullets in a magazine.
        /// </summary>
        [Tooltip("Number of bullets in a magazine.")]
        public int bulletsPerMagazine;

        /// <summary>
        /// Total number of bullets available on pickup.
        /// </summary>
        [Tooltip("Total number of bullets available on pickup.")]
        public int totalInitBullets;

        /// <summary>
        /// Number of bullets left in the current magazine.
        /// </summary>
        [Tooltip("Number of bullets left in the current magazine.")]
        public int bulletsLeftInMag;

        /// <summary>
        /// Number of bullets left in the inventory.
        /// </summary>
        [Tooltip("Number of bullets left in the inventory.")]
        public int bulletsLeftInInventory;

        /// <summary>
        /// Random spread pattern depending on how long the gun has been shooting or if the character is moving.
        /// </summary>
        [Tooltip("This will define a random spread pattern depending on how long has the gun been shooting or if the character is moving.")]
        public float spread;

        /// <summary>
        /// Original spread value of the weapon.
        /// </summary>
        [Tooltip(" Original spread value of the weapon.")]
        public float originalSpread;

        /// <summary>
        /// Indicates whether the reduced spread value should be used when the player is not moving.
        /// </summary>
        [Tooltip("Check to define if the Spread will be replaced by the Reduced Spread value when the player is not moving.")]
        public bool reducedSpreadWhenNotMoving;

        /// <summary>
        /// Reduced spread size value if the character is not moving.
        /// </summary>
        [Tooltip("This will define a reduced spread pattern if the character is not moving.")]
        public float reducedSpread;

        /// <summary>
        /// Spread size while running.
        /// </summary>
        [Tooltip("This will define a spread pattern while running.")]
        public float runningSpread;

        /// <summary>
        /// Spread size while crouching.
        /// </summary>
        [Tooltip("This will define a spread pattern while crouching.")]
        public float crouchSpread;

        /// <summary>
        /// Maximum size of the crosshairs when shooting or moving.
        /// </summary>
        [Tooltip("Maximum Crosshairs size when the player is shooting or moving.")]
        public float crosshairsMaxSize;

        /// <summary>
        /// Speed of crosshairs expansion/compression.
        /// </summary>
        [Tooltip("Crosshairs expansion/compression speed.")]
        public float crosshairsReactionSpeed;

        /// <summary>
        /// Time between shots when pressing the fire button multiple times.
        /// </summary>
        [Tooltip("Time between shots when pressing the fire button multiple times.")]
        public float timeBetweenShotsFromDifferentPresses;

        /// <summary>
        /// Time between shots when holding down the fire button.
        /// </summary>
        [Tooltip("Time between shots when holding down the fire button.")]
        public float timeBetweenShotsOfSamePress;

        /// <summary>
        /// Number of bullets per burst.
        /// </summary>
        [Tooltip("Number of bullets per burst.")]
        public int bulletsPerBurst;

        /// <summary>
        /// Number of bullets per spread shot.
        /// </summary>
        [Tooltip("Number of bullets per spread shot.")]
        public int bulletsPerSpreadShot;

        /// <summary>
        /// Indicates whether the weapon is a single-shot weapon.
        /// </summary>
        [Tooltip("Indicates whether the weapon is a single-shot weapon.")]
        public bool singleShotWeapon;

        /// <summary>
        /// Indicates whether the weapon is an automatic weapon.
        /// </summary>
        [Tooltip("Indicates whether the weapon is an automatic weapon.")]
        public bool automaticWeapon;

        /// <summary>
        /// Indicates whether the weapon is a burst weapon.
        /// </summary>
        [Tooltip("Indicates whether the weapon is a burst weapon.")]
        public bool burstWeapon;

        /// <summary>
        /// Indicates whether the weapon is a spread shot weapon.
        /// </summary>
        [Tooltip("Indicates whether the weapon is a spread shot weapon.")]
        public bool spreadShotWeapon;

        /// <summary>
        /// Sprite of the weapon.
        /// </summary>
        [Tooltip("Sprite of the weapon.")]
        public Sprite weaponSprite;

        /// <summary>
        /// Sound effects of the weapon.
        /// </summary>
        [Tooltip("Sound effects of the weapon.")]
        public WeaponSounds weaponSounds = new WeaponSounds();

        /// <summary>
        /// Array of muzzle flash game objects.
        /// </summary>
        [Tooltip("Array of muzzle flash game objects.")]
        public GameObject[] muzzleFlashes;

        /// <summary>
        /// Number of muzzle flashes.
        /// </summary>
        [Tooltip("Number of muzzle flashes.")]
        public int muzzleFlashesCount;

        /// <summary>
        /// Resting size of the crosshairs.
        /// </summary>
        [Tooltip("Resting size of the crosshairs.")]
        public float crosshairsRestingSize;
        /// <summary>
        /// Name of the mesh prefab for the gallery.
        /// </summary>
        [Tooltip("Name of the mesh prefab for the gallery.")]
        public string meshPrefabForGalleryName;

        /// <summary>
        /// Indicates whether the weapon has been picked up.
        /// </summary>
        [Tooltip("Indicates whether the weapon has been picked up.")]
        public bool hasBeenPickedUp;

        /// <summary>
        /// Resets the weapon's properties.
        /// </summary>
        public void ResetWeapon()
        {
            hasBeenPickedUp = true;
            bulletsLeftInMag = bulletsPerMagazine;
            bulletsLeftInInventory = totalInitBullets - bulletsPerMagazine;
        }
    }


#if UNITY_EDITOR
        [CustomEditor(typeof(Weapon))]
    public class CustomWeaponInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            #region Prepare
            //Define
            GUIStyle header = new GUIStyle();
            GUIStyle leftAlignedHeader = new GUIStyle();
            GUIStyle blackButton = new GUIStyle();
            GUIStyle whiteButton = new GUIStyle();
            GUIStyle initbutton = new GUIStyle();
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
            leftAlignedHeader.fontStyle = FontStyle.Bold;
            leftAlignedHeader.normal.textColor = Color.white;
            //Set Black Button GUIStyle
            blackButton.font = font;
            blackButton.fontSize = 13;
            blackButton.fontStyle = FontStyle.Bold;
            blackButton.alignment = TextAnchor.MiddleCenter;
            blackButton.normal.textColor = Colors.GhostWhite;
            blackButton.active.textColor = Colors.LightYellow;
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
            initbutton.font = font;
            initbutton.fontSize = 13;
            initbutton.fontStyle = FontStyle.Bold;
            initbutton.alignment = TextAnchor.MiddleCenter;
            initbutton.normal.textColor = Color.white;
            initbutton.normal.background = (Texture2D)Resources.Load(ResourcesPathContainer.editorButtonRed);
            initbutton.active.background = (Texture2D)Resources.Load(ResourcesPathContainer.editorButtonRedClick);


            //Set Subclass GUIStyle
            subclass.font = font;
            subclass.fontSize = 15;
            subclass.normal.textColor = Color.white;

            var width80 = GUILayout.Width(80);
            #endregion

            #region GetProperties
            Weapon weapon = (Weapon)target;
            SerializedProperty wpnClass = serializedObject.FindProperty("weaponClass");
            SerializedProperty wpnName = serializedObject.FindProperty("weaponName");
            SerializedProperty dmg = serializedObject.FindProperty("damage");
            SerializedProperty singleFire = serializedObject.FindProperty("singleShotWeapon");
            SerializedProperty autoFire = serializedObject.FindProperty("automaticWeapon");
            SerializedProperty burstFire = serializedObject.FindProperty("burstWeapon");
            SerializedProperty spreadShotFire = serializedObject.FindProperty("spreadShotWeapon");
            SerializedProperty bulletsPerSpreadShot = serializedObject.FindProperty("bulletsPerSpreadShot");
            SerializedProperty range = serializedObject.FindProperty("effectiveRange");
            SerializedProperty force = serializedObject.FindProperty("impactForce");
            SerializedProperty reload = serializedObject.FindProperty("reloadTime");
            SerializedProperty bulletsPerMag = serializedObject.FindProperty("bulletsPerMagazine");
            SerializedProperty initBullets = serializedObject.FindProperty("totalInitBullets");
            SerializedProperty currentBullets = serializedObject.FindProperty("bulletsLeftInMag");
            SerializedProperty spread = serializedObject.FindProperty("spread");
            SerializedProperty doReduceSpread = serializedObject.FindProperty("reducedSpreadWhenNotMoving");
            SerializedProperty reducedSpread = serializedObject.FindProperty("reducedSpread");
            SerializedProperty timeToShootAgain = serializedObject.FindProperty("timeBetweenShotsFromDifferentPresses");
            SerializedProperty runSpread = serializedObject.FindProperty("runningSpread");
            SerializedProperty crouchSpread = serializedObject.FindProperty("crouchSpread");
            SerializedProperty timeBurstOrPellets = serializedObject.FindProperty("timeBetweenShotsOfSamePress");
            SerializedProperty bulletsPerBurst = serializedObject.FindProperty("bulletsPerBurst");
            SerializedProperty XhairsSize = serializedObject.FindProperty("crosshairsMaxSize");
            SerializedProperty XhairsSpeed = serializedObject.FindProperty("crosshairsReactionSpeed");
            SerializedProperty sprite = serializedObject.FindProperty("weaponSprite");
            SerializedProperty weaponSounds = serializedObject.FindProperty("weaponSounds");
            SerializedProperty muzzleFlashes = serializedObject.FindProperty("muzzleFlashes");
            SerializedProperty weaponInit = serializedObject.FindProperty("initialized");
            //crosshairsMaxSize, reducedSpread, reducedSpreadWhenNotMoving, spread, effectiveRange, impactForce, reloadTime, bulletsPerMagazine, totalInitBullets, bulletsLeftInMag
            #endregion

            #region Custom Weapon Properties
            if (weapon.initialized)
            {
                if (!weapon.custom)
                {
                    EditorGUILayout.LabelField($"{weapon.selectedWeapon.ToString()}, {weapon.weaponClass.ToString()}", header);
                    GUILayout.Space(8);
                }
                if (weapon.custom)
                {

                    EditorGUILayout.LabelField($"{weapon.weaponName.ToString()}, {weapon.weaponClass.ToString()}", header);
                    GUILayout.Space(8);
                    serializedObject.Update();
                    EditorGUILayout.PropertyField(wpnName);
                    EditorGUILayout.PropertyField(wpnClass);
                    serializedObject.ApplyModifiedProperties();
                }
            }
            #endregion

            #region Weapon Presets
            if (!weapon.initialized)
            {
                EditorGUILayout.LabelField("Weapon Presets", leftAlignedHeader);

                GUILayout.Space(8);
                #region Pistols
                GUILayout.BeginHorizontal();
                GUILayout.Label("Pistols", subclass);
                if (GUILayout.Button("M1911", blackButton, width80))
                {
                    weapon.selectedWeapon = Weapon.SelectedWeapon.M1911;
                    weapon.weaponClass = Weapon.WeaponClass.Pistol;
                    weapon.damage = 35;
                    weapon.singleShotWeapon = true;
                    weapon.automaticWeapon = false;
                    weapon.burstWeapon = false;
                    weapon.initialized = true;
                    weapon.custom = false;
                }
                if (GUILayout.Button("Glock", blackButton, width80))
                {

                    weapon.selectedWeapon = Weapon.SelectedWeapon.Glock;
                    weapon.weaponClass = Weapon.WeaponClass.Pistol;
                    weapon.damage = 25;
                    weapon.singleShotWeapon = true;
                    weapon.automaticWeapon = true;
                    weapon.burstWeapon = false;
                    weapon.initialized = true;
                    weapon.custom = false;
                }
                if (GUILayout.Button("Magnum", blackButton, width80))
                {
                    weapon.selectedWeapon = Weapon.SelectedWeapon.Magnum;
                    weapon.weaponClass = Weapon.WeaponClass.Pistol;
                    weapon.damage = 75;
                    weapon.singleShotWeapon = true;
                    weapon.automaticWeapon = false;
                    weapon.burstWeapon = false;
                    weapon.initialized = true;
                    weapon.custom = false;
                }
                GUILayout.EndHorizontal();

                #endregion
                #region SMGs
                GUILayout.BeginHorizontal();
                GUILayout.Label("Sub-Machine Guns", subclass);
                if (GUILayout.Button("MP5", blackButton, width80))
                {
                    weapon.selectedWeapon = Weapon.SelectedWeapon.MP5;
                    weapon.weaponClass = Weapon.WeaponClass.SMG;
                    weapon.damage = 32;
                    weapon.singleShotWeapon = true;
                    weapon.automaticWeapon = true;
                    weapon.burstWeapon = false;
                    weapon.initialized = true;
                    weapon.custom = false;
                }
                if (GUILayout.Button("Uzi", blackButton, width80))
                {
                    weapon.selectedWeapon = Weapon.SelectedWeapon.Uzi;
                    weapon.weaponClass = Weapon.WeaponClass.SMG;
                    weapon.damage = 25;
                    weapon.singleShotWeapon = true;
                    weapon.automaticWeapon = true;
                    weapon.burstWeapon = false;
                    weapon.initialized = true;
                    weapon.custom = false;
                }
                GUILayout.EndHorizontal();

                #endregion                
                #region ARs

                GUILayout.BeginHorizontal();
                GUILayout.Label("Assault Rifles", subclass);
                if (GUILayout.Button("SCAR-L", blackButton, width80))
                {

                    weapon.initialized = true;
                    weapon.custom = false;
                }
                if (GUILayout.Button("SCAR-H", blackButton, width80))
                {
                    weapon.initialized = true;
                    weapon.custom = false;
                }
                if (GUILayout.Button("AR-15", blackButton, width80))
                {
                    weapon.initialized = true;
                    weapon.custom = false;
                }
                GUILayout.EndHorizontal();
                #endregion
                #region Custom

                GUILayout.BeginHorizontal();
                GUILayout.Label("Custom", subclass);
                if (GUILayout.Button("Create Custom", blackButton, GUILayout.Width(96)))
                {
                    weapon.custom = true;
                    weapon.selectedWeapon = Weapon.SelectedWeapon.Custom;
                    weapon.initialized = true;
                }
                GUILayout.EndHorizontal();
                #endregion
            } 
            #endregion

            if (weapon.initialized)
            {
                serializedObject.Update();
                EditorGUILayout.PropertyField(dmg);
                EditorGUILayout.PropertyField(range);
                EditorGUILayout.PropertyField(singleFire);
                EditorGUILayout.PropertyField(autoFire);
                EditorGUILayout.PropertyField(burstFire);
                EditorGUILayout.PropertyField(spreadShotFire);
                EditorGUILayout.PropertyField(bulletsPerMag); 
                EditorGUILayout.PropertyField(initBullets);
                EditorGUILayout.PropertyField(currentBullets);
                EditorGUILayout.PropertyField(force);
                EditorGUILayout.PropertyField(reload);
                EditorGUILayout.PropertyField(XhairsSize);
                EditorGUILayout.PropertyField(XhairsSpeed);
                EditorGUILayout.PropertyField(spread);
                EditorGUILayout.PropertyField(doReduceSpread);
                EditorGUILayout.PropertyField(reducedSpread);
                EditorGUILayout.PropertyField(timeToShootAgain);
                EditorGUILayout.PropertyField(runSpread);
                EditorGUILayout.PropertyField(crouchSpread);
                EditorGUILayout.PropertyField(timeBurstOrPellets);
                EditorGUILayout.PropertyField(bulletsPerBurst);
                EditorGUILayout.PropertyField(bulletsPerSpreadShot);
                EditorGUILayout.PropertyField(sprite);
                EditorGUILayout.PropertyField(weaponSounds, includeChildren: true);
                EditorGUILayout.PropertyField(muzzleFlashes, includeChildren: true);

                //serializedObject.ApplyModifiedProperties();
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
                #region Init Button
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Init Weapon", initbutton, width80))
                {
                    //init values
                    weapon.selectedWeapon = Weapon.SelectedWeapon.Undefined_Weapon;
                    weapon.singleShotWeapon = false;
                    weapon.weaponClass = Weapon.WeaponClass.Undefined_Class;
                    weapon.automaticWeapon = false;
                    weapon.burstWeapon = false;
                    weapon.damage = 0;
                    weapon.custom = false;

                    //return to preset selection
                    weapon.initialized = false;
                }
                GUILayout.EndHorizontal(); 
                #endregion
            }
        }
    }
#endif
}