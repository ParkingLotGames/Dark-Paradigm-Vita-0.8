using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.Gameplay
{
    /// <summary>
    /// This class controls the behavior of the muzzle flash effects of a weapon.
    /// </summary>
    public class MuzzleFlashBehavior : MonoBehaviour
    {
        /// <summary>
        /// The muzzle flash components associated with this object.
        /// </summary>
        private Component[] muzzleFlashes;

        /// <summary>
        /// The number of muzzle flash components associated with this object.
        /// </summary>
        private int muzzleFlashesLength;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            muzzleFlashes = GetComponentsInChildren(typeof(Despawner), true);
            muzzleFlashesLength = muzzleFlashes.Length;
        }

        /// <summary>
        /// Activates a random muzzle flash component associated with this object.
        /// </summary>
        public void EnableMuzzleShot()
        {
            muzzleFlashes[Random.Range(0, muzzleFlashesLength)].gameObject.SetActive(true);
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(MuzzleFlashBehavior))]
        //[CanEditMultipleObjects]

        public class CustomMuzzleFlashBehaviorInspector : Editor
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
                MuzzleFlashBehavior MuzzleFlashBehavior = (MuzzleFlashBehavior)target;
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