using UnityEngine;
using DP.DevTools;
using UnityEngine.UI;
using DP.Controllers;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.UI
{
    /// <summary>
    /// A class for managing the stamina bar UI for the player character.
    /// </summary>
    public class StaminaBar : MonoBehaviour
    {
        [SerializeField]
        public Gradient staminaBarColor;

        [SerializeField]
        public Slider StaminaBarSlider;

        [SerializeField]
        public Image StaminaBarImage;

        [SerializeField]
        public PlayerMovementController playerStamina;

        /// <summary>
        /// Updates the stamina bar UI based on the current and maximum stamina of the player.
        /// </summary>
        public void UpdateStaminaBar()
        {
            float currentStamina = playerStamina.currentStamina;
            float maxStamina = playerStamina.maxStamina;
            StaminaBarSlider.value = currentStamina;
            StaminaBarSlider.maxValue = maxStamina;
            StaminaBarImage.color = staminaBarColor.Evaluate(currentStamina / maxStamina);
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(StaminaBar))]
	//[CanEditMultipleObjects]

    public class CustomStaminaBarInspector : Editor
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
        StaminaBar staminaBar = (StaminaBar)target;
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