using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;
using DP.Controllers;

namespace DP.UI
{
    /// <summary>
    /// Represents a dynamic crosshairs element that can expand and compress depending on player and weapon states.
    /// </summary>
    public class DynamicCrosshairs : MonoBehaviour
    {
        /// <summary>
        /// The singleton instance of this class.
        /// </summary>
        public static DynamicCrosshairs Instance;

        /// <summary>
        /// The WeaponController component that controls the weapon states.
        /// </summary>
        [SerializeField] public WeaponController weaponController;

        /// <summary>
        /// The resting size of the crosshairs when the player is not moving or shooting.
        /// </summary>
        public float restingSize;

        /// <summary>
        /// The maximum size of the crosshairs when the player is moving or shooting.
        /// </summary>
        public float maxSize;

        /// <summary>
        /// The current size of the crosshairs that is being updated every frame.
        /// </summary>
        public float currentSize;

        /// <summary>
        /// The speed at which the crosshairs expands or compresses.
        /// </summary>
        public float reactionSpeed;

        /// <summary>
        /// The RectTransform of the reticle UI element.
        /// </summary>
        RectTransform reticleTransform;

        /// <summary>
        /// The Image components of the crosshairs UI element.
        /// </summary>
        Image[] crosshairsParts;

        /// <summary>
        /// The number of Image components in the crosshairs UI element.
        /// </summary>
        int crosshairsPartsCount;

        /// <summary>
        /// The color of the crosshairs when they are visible.
        /// </summary>
        Color crosshairsVisibleColor;

        /// <summary>
        /// The color of the crosshairs when they are not visible.
        /// </summary>
        Color crosshairsNotVisibleColor;

        /// <summary>
        /// The current color of the crosshairs that is being updated every frame.
        /// </summary>
        Color currentColor;

        /// <summary>
        /// Indicates whether the player is moving or not.
        /// </summary>
        private bool playerMoving;
        /// <summary>
        /// Sets the playerMoving variable to true.
        /// </summary>
        public void PlayerMovingTrue() { playerMoving = true; }
        /// <summary>
        /// Sets the playerMoving variable to false.
        /// </summary>
        public void PlayerMovingFalse() { playerMoving = false; }

        private void Start()
        {
            reticleTransform = GetComponent<RectTransform>();
            crosshairsParts = reticleTransform.GetComponentsInChildren<Image>();
            crosshairsPartsCount = crosshairsParts.Length;
            crosshairsVisibleColor = crosshairsParts[1].color;
            crosshairsNotVisibleColor = crosshairsVisibleColor;
            crosshairsNotVisibleColor.a = 0;
            GetCrosshairsValues();
        }

        private void Update()
        {
            currentColor = crosshairsParts[3].color;
            if (playerMoving || weaponController.isShooting && !weaponController.isReloading && weaponController.bulletsLeftInMag > 0)
                ExpandCrosshairs();
            else
                CompressCrosshairs();


            if (weaponController.isAiming)
            {
                for (int i = 0; i < crosshairsPartsCount; i++)
                {
                    if (crosshairsParts[i].color != crosshairsNotVisibleColor)
                        crosshairsParts[i].color = Color.Lerp(currentColor, crosshairsNotVisibleColor, 25 * Time.fixedDeltaTime);
                }
            }
            if (!weaponController.isAiming)
            {
                for (int i = 0; i < crosshairsPartsCount; i++)
                {
                    if (crosshairsParts[i].color != crosshairsVisibleColor)
                        crosshairsParts[i].color = Color.Lerp(currentColor, crosshairsVisibleColor, 10 * Time.fixedDeltaTime);
                }
            }
        }
        /// <summary>
        /// Lerps the current size of the crosshairs to the maximum size.
        /// </summary>
        public void ExpandCrosshairs()
        {
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * reactionSpeed);
            reticleTransform.sizeDelta = new Vector2(currentSize, currentSize);
        }
        /// <summary>
        /// Lerps the current size of the crosshairs to the resting size.
        /// </summary>
        public void CompressCrosshairs()
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * reactionSpeed);
            reticleTransform.sizeDelta = new Vector2(currentSize, currentSize);
        }
        /// <summary>
        /// Gets the crosshairs resting size, maximum size, and reaction speed from the WeaponController component.
        /// </summary>
        public void GetCrosshairsValues()
        {
            restingSize = weaponController.crosshairsRestingSize;
            maxSize = weaponController.crosshairsMaxSize;
            reactionSpeed = weaponController.crosshairsReactionSpeed;
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(DynamicCrosshairs))]
    //[CanEditMultipleObjects]

    public class CustomDynamicCrosshairsInspector : Editor
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
            DynamicCrosshairs DynamicCrosshairs = (DynamicCrosshairs)target;
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