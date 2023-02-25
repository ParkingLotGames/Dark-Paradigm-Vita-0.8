using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DP.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Impactable Layers", menuName = "Impactable Layers", order = 1)]
    /// <summary>
    /// This class represents the impactable layers that an object can collide with in the game environment.
    /// </summary>
    public class ImpactableLayers : ScriptableObject
    {
        /// <summary>
        /// The instance of the ImpactableLayers class.
        /// </summary>
        public static ImpactableLayers Instance;

        /// <summary>
        /// Represents the different bit masks that an object can collide with in the game environment.
        /// </summary>
        [System.Serializable]
        public class EnvironmentBitMasks
        {
            /// <summary>
            /// The layer mask for concrete objects.
            /// </summary>
            public LayerMask concreteLayer;

            /// <summary>
            /// The layer mask for water objects.
            /// </summary>
            public LayerMask waterLayer;

            /// <summary>
            /// The layer mask for metal objects.
            /// </summary>
            public LayerMask metalLayer;

            /// <summary>
            /// The layer mask for wooden objects.
            /// </summary>
            public LayerMask woodLayer;
        }

        /// <summary>
        /// The different bit masks that an enemy object can collide with in the game environment.
        /// </summary>
        [System.Serializable]
        public class EnemyBitMasks
        {
            /// <summary>
            /// The layer mask for the leg of an enemy.
            /// </summary>
            public LayerMask legLayer;

            /// <summary>
            /// The layer mask for the upper leg of an enemy.
            /// </summary>
            public LayerMask upLegLayer;

            /// <summary>
            /// The layer mask for the hips of an enemy.
            /// </summary>
            public LayerMask hipsLayer;

            /// <summary>
            /// The layer mask for the torso of an enemy.
            /// </summary>
            public LayerMask torsoLayer;

            /// <summary>
            /// The layer mask for the chest of an enemy.
            /// </summary>
            public LayerMask chestLayer;

            /// <summary>
            /// The layer mask for the neck of an enemy.
            /// </summary>
            public LayerMask neckLayer;

            /// <summary>
            /// The layer mask for the head of an enemy.
            /// </summary>
            public LayerMask headLayer;

            /// <summary>
            /// The layer mask for the arm of an enemy.
            /// </summary>
            public LayerMask armLayer;

            /// <summary>
            /// The layer mask for the forearm of an enemy.
            /// </summary>
            public LayerMask forearmLayer;
        }

        /// <summary>
        /// The environment bit masks.
        /// </summary>
        public EnvironmentBitMasks environmentBitMasks = new EnvironmentBitMasks();

        /// <summary>
        /// The enemy bit masks.
        /// </summary>
        public EnemyBitMasks enemyBitMasks = new EnemyBitMasks();
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(ImpactableLayers))]
	//[CanEditMultipleObjects]
	public class CustomImpactableLayersInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
#endregion
}