using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DP.DevTools
{
    /// <summary>
    /// Replaces the docks level in a platform-dependent way.
    /// </summary>
    public class PlatformDependantDocksLevelReplacer : MonoBehaviour
    {
        /// <summary>
        /// The instance of the PlatformDependantDocksLevelReplacer class.
        /// </summary>
        public static PlatformDependantDocksLevelReplacer Instance;

        /// <summary>
        /// The current level GameObject.
        /// </summary>
        GameObject current;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            Destroy(this);
        }

        /// <summary>
        /// Gets the Android level.
        /// </summary>
        public void GetAndroidLevel()
        {
            GameObject level = Instantiate((GameObject)Resources.Load(ResourceManagement.ResourcesPathContainer.androidDocks));
            level.transform.parent = transform;
            level.transform.position = transform.position;
        }

        /// <summary>
        /// Gets the PSVita level.
        /// </summary>
        public void GetPSVitaLevel()
        {
            var level = Instantiate((GameObject)Resources.Load(ResourceManagement.ResourcesPathContainer.vitaDocks));
            level.transform.parent = transform;
            level.transform.position = transform.position;
        }

        /// <summary>
        /// Gets the Android Chunky level.
        /// </summary>
        public void GetAndroidChunkyLevel()
        {
            GameObject level = Instantiate((GameObject)Resources.Load(ResourceManagement.ResourcesPathContainer.androidDocksChunky));
            level.transform.parent = transform;
            level.transform.position = transform.position;
        }

        /// <summary>
        /// Gets the PSVita Chunky level.
        /// </summary>
        public void GetPSVitaChunkyLevel()
        {
            var level = Instantiate((GameObject)Resources.Load(ResourceManagement.ResourcesPathContainer.vitaDocksChunky));
            level.transform.parent = transform;
            level.transform.position = transform.position;
        }

        /// <summary>
        /// Gets the PS4 level.
        /// </summary>
        public void GetPS4Level()
        {
            var level = Instantiate((GameObject)Resources.Load(ResourceManagement.ResourcesPathContainer.PS4Docks));
            level.transform.parent = transform;
            level.transform.position = transform.position;
        }

        /// <summary>
        /// Gets the PC level.
        /// </summary>
        public void GetPCLevel()
        {
            var level = Instantiate((GameObject)Resources.Load(ResourceManagement.ResourcesPathContainer.PCDocks));
            level.transform.parent = transform;
            level.transform.position = transform.position;
        }

        /// <summary>
        /// Gets the current level GameObject.
        /// </summary>
        /// <returns>The current level GameObject, or null if it doesn't exist.</returns>
        public GameObject GetCurrent()
        {
            if (GetComponentInChildren<LevelPrefab>())
                current = GetComponentInChildren<LevelPrefab>().gameObject;
            return current;
        }

        /// <summary>
        /// Removes the current level GameObject.
        /// </summary>
        public void RemoveCurrent()
        {
            var level = GetCurrent();
            if (level)
                DestroyImmediate(level);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(PlatformDependantDocksLevelReplacer))]
    public class CustomPlatformDependantLevelReplacerInspector : Editor
    {
        public override void OnInspectorGUI()
        {

            PlatformDependantDocksLevelReplacer PDLR = (PlatformDependantDocksLevelReplacer)target;
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Load PS Vita Level")) { PDLR.RemoveCurrent(); PDLR.GetPSVitaLevel(); }
            if (GUILayout.Button("Load PS Vita Chunky Level")) { PDLR.RemoveCurrent(); PDLR.GetPSVitaChunkyLevel(); }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Load Android Level")) { PDLR.RemoveCurrent(); PDLR.GetAndroidLevel(); }
            if (GUILayout.Button("Load Android Chunky Level")) { PDLR.RemoveCurrent(); PDLR.GetAndroidChunkyLevel(); }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Load PS4 Level")) { PDLR.RemoveCurrent(); PDLR.GetPS4Level(); }
            if (GUILayout.Button("Load PC Level")) { PDLR.RemoveCurrent(); PDLR.GetPCLevel(); }
            EditorGUILayout.EndHorizontal();
        }
    }
#endif
}