using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace PLS.DevTools
{
    public class AppBuildType : MonoBehaviour 
	{
        public static AppBuildType Instance;
        public enum Type { dev, preview, release }
        [SerializeField]public Type type;
        private void Awake() { if (!Instance) { Instance = this; DontDestroyOnLoad(this); } else Destroy(gameObject); }
    }
    #if UNITY_EDITOR
    [CustomEditor(typeof(AppBuildType))]
    public class CustomAppBuildTypeInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            AppBuildType appBuildType = (AppBuildType)target;

            SerializedProperty abt = serializedObject.FindProperty("type");

            GUILayout.BeginHorizontal();
            serializedObject.Update();
            EditorGUILayout.PropertyField(abt);
            serializedObject.ApplyModifiedProperties();
            GUILayout.EndHorizontal();
        }
    }
        #endif
}