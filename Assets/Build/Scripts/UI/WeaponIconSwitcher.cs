using UnityEngine;
using DP.DevTools;
using DP.Controllers;
using UnityEngine.UI;
using DP.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.UI
{
    /// <summary>
    /// A script for switching weapon icons between small and large sprites.
    /// </summary>
    public class WeaponIconSwitcher : MonoBehaviour
    {
        [SerializeField] private WeaponController weaponController;
        [SerializeField] private Image smallSprite;
        [SerializeField] private Image largeSprite;

        /// <summary>
        /// Update the weapon icon when switching between small and large sprite size.
        /// </summary>
        public void UpdateIconTriangleSwap()
        {
            // Update the weapon icon based on the equipped weapon's sprite size.
            if (weaponController.equippedWeapon.spriteSize == Weapon.SpriteSize.Small)
            {
                smallSprite.sprite = weaponController.equippedWeapon.weaponSprite;
                largeSprite.gameObject.SetActive(false);
                smallSprite.gameObject.SetActive(true);
            }
            else
            {
                largeSprite.sprite = weaponController.equippedWeapon.weaponSprite;
                smallSprite.gameObject.SetActive(false);
                largeSprite.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Update the weapon icon when switching between different items on the weapon wheel.
        /// </summary>
        public void UpdateIconItemWheelSwap()
        {
            // Update the weapon icon based on the equipped weapon's sprite size.
            if (weaponController.equippedWeapon.spriteSize == Weapon.SpriteSize.Small)
            {
                smallSprite.sprite = weaponController.equippedWeapon.weaponSprite;
                largeSprite.gameObject.SetActive(false);
                smallSprite.gameObject.SetActive(true);
            }
            else
            {
                largeSprite.sprite = weaponController.equippedWeapon.weaponSprite;
                smallSprite.gameObject.SetActive(false);
                largeSprite.gameObject.SetActive(true);
            }
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(WeaponIconSwitcher))]
	//[CanEditMultipleObjects]

    public class CustomWeaponIconSwitcherInspector : Editor
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
        WeaponIconSwitcher WeaponIconSwitcher = (WeaponIconSwitcher)target;
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