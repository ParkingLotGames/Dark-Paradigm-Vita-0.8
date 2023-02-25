using UnityEngine;
using DP.StaticDataContainers;
using DP.ScriptableEvents;
using UnityEngine.UI;
using DP.Gameplay;
using DP.Controllers;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.UI
{
    /// <summary>
    /// The ItemWheelController class controls the behavior of the item wheel in the game.
    /// </summary>
    public class ItemWheelController : MonoBehaviour
    {
        /// <summary>
        /// Determines if the behavior of the item wheel is enabled.
        /// </summary>
        bool behaviorEnabled;
        /// <summary>
        /// The SlowMotionManager used to manage slow motion in the game.
        /// </summary>
        [SerializeField] SlowMotionManager slowMotionManager;
        /// <summary>
        /// The Canvas object that represents the item wheel.
        /// </summary>
        [SerializeField] Canvas itemWheel;
        /// <summary>
        /// The Image object that represents the up direction on the item wheel.
        /// </summary>
        [SerializeField] Image up;
        /// <summary>
        /// The Image object that represents the down direction on the item wheel.
        /// </summary>
        [SerializeField] Image down;
        /// <summary>
        /// The Image object that represents the left direction on the item wheel.
        /// </summary>
        [SerializeField] Image left;
        /// <summary>
        /// The Image object that represents the right direction on the item wheel.
        /// </summary>
        [SerializeField] Image right;
        /// <summary>
        /// The Color used to represent the selected state of an item wheel direction.
        /// </summary>
        [SerializeField] public Color selected;
        /// <summary>
        /// The Color used to represent the not selected state of an item wheel direction.
        /// </summary>
        [SerializeField] public Color notSelected;
        /// <summary>
        /// The horizontal input value for the item wheel.
        /// </summary>
        float horizontal;
        /// <summary>
        /// The vertical input value for the item wheel.
        /// </summary>
        float vertical;
        /// <summary>
        /// Determines if the item wheel time is enabled.
        /// </summary>
        public bool itemWheelTimeEnabled;
        /// <summary>
        /// Determines if the item wheel key is still being held down since a click selection was made.
        /// </summary>
        public bool itemWheelKeyStillHoldSinceClickSelection;
        /// <summary>
        /// Determines if a click selection was made.
        /// </summary>
        public bool madeClickSelection;
        /// <summary>
        /// The Image object that represents the currently selected joystick direction.
        /// </summary>
        Image SelectedImage;
        /// <summary>
        /// The UnityEvent that is invoked when the item wheel is disabled by a click selection.
        /// </summary>
        [SerializeField] UnityEvent OnItemWheelDisableByClickSelection;
        /// <summary>
        /// The UnityEvent that is invoked when the input for a click selection is released.
        /// </summary>
        [SerializeField] UnityEvent OnClickSelectionInputReleased;

        /// <summary>
        /// Returns the Image object that represents the currently selected joystick direction.
        /// </summary>
        /// <returns>The currently selected joystick direction Image object.</returns>
        Image GetCurrentJoystickSelection()
        {
            return SelectedImage;
        }

        /// <summary>
        /// Enables the behavior of the item wheel.
        /// </summary>
        public void EnableBehavior()
        {
            behaviorEnabled = true;
        }

        /// <summary>
        /// Disables the behavior of the item wheel.
        /// </summary>
        public void DisableBehavior()
        {
            behaviorEnabled = false;
        }

        /// <summary>
        /// Hides the item wheel by click selection.
        /// </summary>
        public void HideItemWhelByClickSelection()
        {
            HideItemWheel();
        }

        /// <summary>
        /// Initializes the ItemWheelController object by disabling the item wheel.
        /// </summary>
        private void Awake()
        {
            itemWheel.enabled = (false);
        }

        /// <summary>
        /// Update is called once per frame and updates the behavior of the item wheel.
        /// </summary>
        void Update()
        {
            if (behaviorEnabled)
            {
                itemWheelKeyStillHoldSinceClickSelection = madeClickSelection &&
                    slowMotionManager.itemWheelTimeInputDetected ? true : false;
                if (!slowMotionManager.itemWheelTimeInputDetected)
                    madeClickSelection = false;

                if (itemWheelKeyStillHoldSinceClickSelection)
                    OnItemWheelDisableByClickSelection.Invoke();
                else
                    OnClickSelectionInputReleased.Invoke();

                if (itemWheel.enabled)
                    WheelOptionsBehavior();
                //TODO add a method to check for ovrlapping
            }
        }

        /// <summary>
        /// Shows the item wheel.
        /// </summary>
        public void ShowItemWheel()
        {
            itemWheel.enabled = (true);
        }

        /// <summary>
        /// Hides the item wheel.
        /// </summary>
        public void HideItemWheel()
        {
            itemWheel.enabled = (false);//iw}
        }

        /// <summary>
        /// Performs the behavior for the item wheel options.
        /// </summary>
        private void WheelOptionsBehavior()
        {
            horizontal = Input.GetAxis(PSVitaInputValues.RSHorizontal);
            vertical = Input.GetAxis(PSVitaInputValues.RSVertical);
            if (vertical > 0 && horizontal > -0.4 && horizontal < 0.4)
            {
                up.color = selected;
            }
            else
            {
                up.color = notSelected;
            }

            if (vertical < 0 && horizontal > -0.4 && horizontal < 0.4)
            {
                down.color = selected;
            }
            else
            {
                down.color = notSelected;
            }

            if (horizontal > 0 && vertical > -0.4 && vertical < 0.4)
            {
                right.color = selected;
            }
            else
            {
                right.color = notSelected;
            }

            if (horizontal < 0 && vertical > -0.4 && vertical < 0.4)

            {
                left.color = selected;
            }
            else
            {
                left.color = notSelected;
            }
        }
    }
        #region CustomInspector
#if UNITY_EDITOR
        [CustomEditor(typeof(ItemWheelController))]
    //[CanEditMultipleObjects]

    public class CustomItemWheelInspector : Editor
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
            ItemWheelController ItemWheelController = (ItemWheelController)target;
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
