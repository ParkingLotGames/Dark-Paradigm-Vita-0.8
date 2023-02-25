using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DP.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Archetypes/Powerup", order = 2)]
    public class Powerup : ScriptableObject
    {
        [SerializeField] public bool initialized;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Powerup))]
    public class CustomPowerupInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            Powerup powerup = (Powerup)target;
            if (!powerup.initialized)
            {
                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Init M1911"))
                {
                    powerup.initialized = true;
                }
                if (GUILayout.Button("Init Glock"))
                {
                    powerup.initialized = true;
                }
                if (GUILayout.Button("Init Magnum"))
                {
                    powerup.initialized = true;
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Init SCAR-L"))
                {

                    powerup.initialized = true;
                }
                if (GUILayout.Button("Init SCAR-H"))
                {
                    powerup.initialized = true;
                }
                if (GUILayout.Button("Init AR-15"))
                {
                    powerup.initialized = true;
                }
                GUILayout.EndHorizontal();
            }
            base.OnInspectorGUI();
        }
    }
#endif
}