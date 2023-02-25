using UnityEngine;

using DP.ResourceManagement;
using PLS.DevTools;
#if UNITY_EDITOR
using UnityEditor;
#endif

// TODO: Consider if this is needed and define a custom symbol intead of performing a bool comparison

namespace DP.CrossPlatform
{
    public class GraphyManager : MonoBehaviour
    {
        private void Start()
        {
#if UNITY_PSP2
            if (AppBuildType.Instance.type != AppBuildType.Type.release && Application.isEditor)
                Instantiate(Resources.Load<GameObject>(ResourcesPathContainer.graphyPath));
#endif
#if !UNITY_PSP2
            if (AppBuildType.Instance.type != AppBuildType.Type.release)
                    Instantiate(Resources.Load(ResourcesPathContainer.graphyPath));
#endif
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(GraphyManager))]
    public class CustomGraphyManagerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
#if UNITY_PSP2
            EditorGUILayout.HelpBox("Graphy will not be loaded on PS Vita due to its limited CPU power, but you can still use it in the editor", MessageType.Info); 
#endif
            GraphyManager graphyManager = (GraphyManager)target;
        }
    }
#endif
}