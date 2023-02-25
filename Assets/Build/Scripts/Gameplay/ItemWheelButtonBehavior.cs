using UnityEngine;
using DP.DevTools;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.UI
{
    /// <summary>
    /// The ItemWheelButtonBehavior class is responsible for handling the behavior of a button in an item wheel.
    /// </summary>
    public class ItemWheelButtonBehavior : MonoBehaviour
    {
        /// <summary>
        /// The button associated with this behavior.
        /// </summary>
        public Button button;

        /// <summary>
        /// The item wheel controller to which this button belongs.
        /// </summary>
        ItemWheelController itemWheelController;

        /// <summary>
        /// The normal color of the stick when it is selected.
        /// </summary>
        [SerializeField] public Color stickSelectedNormalColor;

        /// <summary>
        /// The normal color of the button when it is not selected.
        /// </summary>
        [SerializeField] public Color regularNormalColor;

        /// <summary>
        /// Called when the associated part is selected.
        /// </summary>
        public void OnSelectPart()
        {
            itemWheelController.madeClickSelection = true;
        }
        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            itemWheelController = GetComponentInParent<ItemWheelController>();
        }

    }

#region CustomInspector
#if UNITY_EDITOR
[CustomEditor(typeof(ItemWheelButtonBehavior))]
	//[CanEditMultipleObjects]

    public class CustomItemWheelButtonBehaviorInspector : Editor
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

        //base.OnInspectorGUI();
        ItemWheelButtonBehavior ItemWheelButtonBehavior = (ItemWheelButtonBehavior)target;
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