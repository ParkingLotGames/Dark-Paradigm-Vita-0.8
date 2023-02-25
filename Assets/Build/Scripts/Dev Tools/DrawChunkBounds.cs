using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DP.DevTools
{
    /// <summary>
    /// This class draws the bounding boxes for all the child Chunks objects in the scene using Unity's <c>Gizmos</c> API.
    /// </summary>
    public class DrawChunkBounds : MonoBehaviour
    {

        /// <summary>
        /// An array of all the child Chunk objects in the scene.
        /// </summary>
        /// <value>An array of Chunks objects.</value>
        Chunk[] chunks => GetComponentsInChildren<Chunk>();

#if !UNITY_EDITOR
    /// <summary>
    /// This method is called when the object is instantiated. In a build, it destroys all the Chunks components on this object and then destroys the object itself.
    /// </summary>
    private void Awake()
    {
        var chunksCount = chunks.Length;
        for (int i = 0; i < chunksCount; i++)
            Destroy(chunks[i].GetComponent<Chunk>());
        Destroy(this);
    }
#endif

        /// <summary>
        /// This method is called when the scene is drawn in the Unity editor. It draws a bounding box around each Chunk object in the scene.
        /// </summary>
        private void OnDrawGizmos()
        {
            var chunksCount = chunks.Length;
            for (int i = 0; i < chunksCount; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(chunks[i].transform.position, chunks[i].transform.localScale);
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(DrawChunkBounds))]
    public class CustomDrawChunkBoundsInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
#endif
}