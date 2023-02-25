using UnityEngine;
using DP.DevTools;
using UnityEngine.UI;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Emits sound effects for button interactions, such as navigation, selection, and confirmation/cancellation.
    /// </summary>
    public class ButtonSoundEmitter : MonoBehaviour, IPointerEnterHandler, ISelectHandler
    {
        private Selectable selectable;

        private void Awake()
        {
            /// <summary>
            /// Finds the attached Selectable component.
            /// </summary>
            selectable = GetComponent<Selectable>();
        }

        /// <summary>
        /// Plays the sound effect for cancellation.
        /// </summary>
        public void PlayCancel()
        {
        }

        /// <summary>
        /// Plays the sound effect for confirmation.
        /// </summary>
        public void PlayConfirm()
        {
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            /// <summary>
            /// Plays the sound effect for button navigation.
            /// </summary>
        }

        /// <summary>
        /// Plays the sound effect for button navigation.
        /// </summary>
        public void OnSelect(BaseEventData eventData)
        {
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(ButtonSoundEmitter))]
	[CanEditMultipleObjects]

    public class CustomButtonSoundEmitterInspector : Editor
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
        ButtonSoundEmitter ButtonSoundEmitter = (ButtonSoundEmitter)target;
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