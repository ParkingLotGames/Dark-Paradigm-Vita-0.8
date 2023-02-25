using UnityEngine;
using System.Collections.Generic;
using DP.ResourceManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DP.CrossPlatform
{
    /// <summary>
    /// A manager that initializes the Control Freak plugin on specific platforms.
    /// </summary>
    public class ControlFreakManager : MonoBehaviour
    {
        private void Awake()
        {
#if UNITY_ANDROID
        Instantiate(Resources.Load<GameObject>(ResourcesPathContainer.androidCF2Path));
#endif
#if UNITY_PSP2
        Instantiate(Resources.Load<GameObject>(ResourcesPathContainer.vitaCF2Path));
#endif
#if UNITY_PS4 || UNITY_STANDALONE
            Destroy(this);
#endif
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(ControlFreakManager))]
    public class CustomControlFreakManagerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
#if UNITY_PSP2
            EditorGUILayout.HelpBox("An Instance Of Control Freak for PS Vita will be created at launch", MessageType.Info); 
#endif
            ControlFreakManager controlFreakManager = (ControlFreakManager)target;
        }
    }
#endif
}