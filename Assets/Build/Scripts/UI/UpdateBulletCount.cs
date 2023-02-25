using UnityEngine;
using DP.DevTools;
using DP.ScriptableObjects;
using UnityEngine.UI;
using DP.Controllers;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.UI
{
    /// <summary>
    /// This class updates the bullet count UI by getting the current number of bullets from the weapon controller and displaying it in the UI text component.
    /// </summary>
    public class UpdateBulletCount : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        /// <summary>
        /// The weapon controller that holds information about the current weapon and bullets.
        /// </summary>
        WeaponController weaponController;

        /// <summary>
        /// The UI text component that displays the bullet count.
        /// </summary>
        Text text;
        #endregion

        #region Methods

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            text = GetComponent<Text>();
        }

        /// <summary>
        /// This method updates the bullet count UI by getting the current number of bullets from the weapon controller and displaying it in the UI text component.
        /// </summary>
        public void UpdateCount()
        {
            // Sets the text of the text component to show the number of bullets left in the magazine and the number of bullets left in the inventory.
            text.text = weaponController.bulletsLeftInMag + " / " + (weaponController.bulletsLeftInInventory);
        }

        #endregion
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(UpdateBulletCount))]
	//[CanEditMultipleObjects]

    public class CustomUpdateBulletCountInspector : Editor
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
        UpdateBulletCount UpdateBulletCount = (UpdateBulletCount)target;
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