using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    public class DissolveAnimator : MonoBehaviour 
	{

    }

    #region CustomInspector
#if UNITY_EDITOR
[CustomEditor(typeof(DissolveAnimator))]
	[CanEditMultipleObjects]

    public class CustomDissolveAnimatorInspector : Editor
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
        DissolveAnimator DissolveAnimator = (DissolveAnimator)target;
        SerializedProperty PSVSelect = serializedObject.FindProperty("PSVSelect");
        SerializedProperty PSVStart = serializedObject.FindProperty("PSVStart");
        SerializedProperty PSVCircle = serializedObject.FindProperty("PSVCircle");
        SerializedProperty PSVSquare = serializedObject.FindProperty("PSVSquare");
        SerializedProperty PSVCross = serializedObject.FindProperty("PSVCross");
        SerializedProperty PSVTriangle = serializedObject.FindProperty("PSVTriangle");
        SerializedProperty PSVL1 = serializedObject.FindProperty("PSVR1");
        SerializedProperty PSVR1 = serializedObject.FindProperty("PSVL1");
        SerializedProperty PSVDUp = serializedObject.FindProperty("PSVDUp");
        SerializedProperty PSVDDown = serializedObject.FindProperty("PSVDDown");
        SerializedProperty PSVDLeft = serializedObject.FindProperty("PSVDLeft");
        SerializedProperty PSVDRight = serializedObject.FindProperty("PSVDRight");
        SerializedProperty PSVLSVertical = serializedObject.FindProperty("PSVLSVertical");
        SerializedProperty PSVLSHorizontal = serializedObject.FindProperty("PSVLSHorizontal");
        SerializedProperty PSVRSVertical = serializedObject.FindProperty("PSVRSVertical");
        SerializedProperty PSVRSHorizontal = serializedObject.FindProperty("PSVRSHorizontal");
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