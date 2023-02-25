using UnityEngine;
using DP.DevTools;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Handles collisions between the blood particles emitted by a ParticleSystem component and other game objects.
    /// </summary>
    public class BloodCollision : MonoBehaviour
    {
        /// <summary>
        /// The ParticleSystem component that emits blood particles.
        /// </summary>
        [SerializeField]
        ParticleSystem particleSystem;

        /// <summary>
        /// A list of ParticleCollisionEvent objects, used to store information about each collision between blood particles and other objects.
        /// </summary>
        List<ParticleCollisionEvent> particleCollisionEvents = new List<ParticleCollisionEvent>();

        /// <summary>
        /// Called when a blood particle emitted by the ParticleSystem component collides with another game object.
        /// </summary>
        /// <param name="other">The game object that the blood particle collided with.</param>
        private void OnParticleCollision(GameObject other)
        {
            int events = particleSystem.GetCollisionEvents(other, particleCollisionEvents);

            for (int i = 0; i < events; i++)
            {

            }
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(BloodCollision))]
	[CanEditMultipleObjects]

    public class CustomBloodCollisionInspector : Editor
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
        BloodCollision BloodCollision = (BloodCollision)target;
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