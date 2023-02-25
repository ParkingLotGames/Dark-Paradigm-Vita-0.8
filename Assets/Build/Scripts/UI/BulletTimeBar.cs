using UnityEngine;
using DP.DevTools;
using UnityEngine.UI;
using DP.Gameplay;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.UI
{
    /// <summary>
    /// Controls the behavior and appearance of the bullet time bar UI element.
    /// </summary>
    public class BulletTimeBar : MonoBehaviour
    {
        /// <summary>
        /// The gradient that determines the color of the bullet time bar.
        /// </summary>
        [SerializeField] public Gradient bulletTimeBarColor;

        /// <summary>
        /// The slider component that displays the current bullet time value.
        /// </summary>
        [SerializeField] public Slider bulletTimeBarSlider;

        /// <summary>
        /// The image component that displays the current bullet time bar color.
        /// </summary>
        [SerializeField] public Image bulletTimeBarImage;

        /// <summary>
        /// The bullet time controller that provides the current bullet time value.
        /// </summary>
        [SerializeField] public BulletTimeController bulletTimeController;

        /// <summary>
        /// The current value of the bullet time bar, used for smoothing out the bar color transitions.
        /// </summary>
        float value;

        /// <summary>
        /// Updates the bullet time bar to reflect the current bullet time value.
        /// </summary>
        public void UpdateBulletTimeBar()
        {
            /// <summary>
            /// Sets the maximum value of the bullet time slider to the maximum bullet time value.
            /// </summary>
            bulletTimeBarSlider.maxValue = bulletTimeController.maxBulletTime;

            /// <summary>
            /// Sets the current value of the bullet time slider to the current bullet time value.
            /// </summary>
            bulletTimeBarSlider.value = bulletTimeController.playerInventory.inGameInventory.bulletTimeLeft;

            /// <summary>
            /// Calculates the current color of the bullet time bar based on the current bullet time value and the gradient colors.
            /// </summary>
            Color currentColor = bulletTimeBarColor.Evaluate(bulletTimeController.playerInventory.inGameInventory.bulletTimeLeft / bulletTimeController.maxBulletTime);

            /// <summary>
            /// Sets the bullet time bar image color to the current color if bullet time is not enabled.
            /// </summary>
            if (!bulletTimeController.bulletTimeEnabled)
                bulletTimeBarImage.color = bulletTimeBarColor.Evaluate(bulletTimeController.playerInventory.inGameInventory.bulletTimeLeft / bulletTimeController.maxBulletTime);
            /// <summary>
            /// Otherwise, sets the bullet time bar image color to a lerped value between the current color and the final color, based on the current time.
            /// </summary>
            else
                bulletTimeBarImage.color = Color.Lerp(currentColor, bulletTimeBarColor.Evaluate(1), Mathf.PingPong(Time.unscaledTime * 2, 1));
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(BulletTimeBar))]
	//[CanEditMultipleObjects]

    public class CustomBulletTimearInspector : Editor
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
        BulletTimeBar bulletTimeBar = (BulletTimeBar)target;
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