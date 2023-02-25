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
    /// Handles displaying random tooltips when the selectable is hovered over or selected.
    /// </summary>
    public class OnSelectableSelected : MonoBehaviour, IPointerEnterHandler, ISelectHandler
    {
        /// <summary>
        /// The tooltips to be displayed when the selectable is hovered over or selected.
        /// </summary>
        [SerializeField,TextArea] string[] tooltips;
        /// <summary>
        /// The length of the tooltips array.
        /// </summary>
        int tooltipsLenght;
        /// <summary>
        /// The Selectable component attached to the same GameObject as this script.
        /// </summary>
        Selectable selectable;

        /// <summary>
        /// Initializes the tooltipsLenght and selectable variables.
        /// </summary>
        private void Awake() { selectable = GetComponent<Selectable>(); tooltipsLenght = tooltips.Length; }
        /// <summary>
        /// Sets the tooltip to a random string from the tooltips array and displays it.
        /// </summary>
        public void SetTooltip() { MenuTooltip.Instance.text.text = tooltips[Random.Range(0, tooltipsLenght)].ToUpper(); }
        /// <summary>
        /// Displays a random tooltip when the selectable is hovered over.
        /// </summary>
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) { SetTooltip(); }
        /// <summary>
        /// Displays a random tooltip when the selectable is selected.
        /// </summary>
        public void OnSelect(BaseEventData eventData) { SetTooltip(); }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(OnSelectableSelected))]
	[CanEditMultipleObjects]

    public class CustomOnSelectableSelectedInspector : Editor
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
        OnSelectableSelected OnSelectableSelected = (OnSelectableSelected)target;
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