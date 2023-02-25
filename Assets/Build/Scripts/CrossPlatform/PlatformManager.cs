#if UNITY_PSP2
using DP.PSVita.System;
using DP.StaticDataContainers;
#endif
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace DP.CrossPlatform
{
    /// <summary>
    /// This class manages the PlatformManager object, which is used to control platform-specific behavior within the game.
    ///  PS Vita: Used to load or prevent loading (by pressing L) the Overclocking plugin.
    /// </summary>
    public class PlatformManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance of the PlatformManager object.
        /// </summary>
        public static PlatformManager Instance;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
                Destroy(gameObject);

#if UNITY_PSP2 && !UNITY_EDITOR
        if (!Input.GetButton(PSVitaInputValues.L)) ;
        gameObject.AddComponent<SimpleNativePlugin>();
#endif
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(PlatformManager))]
    public class CustomPlatformManagerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
#if UNITY_PSP2
            EditorGUILayout.HelpBox("PS Vita Overclock plugin will launch during app boot if L is not hold", MessageType.Info);
#endif
            PlatformManager platformManager = (PlatformManager)target;
        }
    }
#endif
}