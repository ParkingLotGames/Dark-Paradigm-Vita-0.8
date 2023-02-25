using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DP
{
    /// <summary>
    /// Handles ground collision events and visual representation of the ground collider.
    /// </summary>
    public class GroundCollider : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance of the ground collider.
        /// </summary>
        public static GroundCollider Instance;

        /// <summary>
        /// Draws a wire cube around the ground collider to represent its bounds.
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(GroundCollider))]
	public class CustomGroundColliderInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
}