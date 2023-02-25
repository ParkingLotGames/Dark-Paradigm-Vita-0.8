using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// This class triggers the transparency of multiple materials when the player collides with a collider that has a "Player" tag.
    /// </summary>
    public class TransparencyTriggerForMultipleMaterials : MonoBehaviour
    {
        /// <summary>
        /// The renderer component used to get the materials of the parent gameobject.
        /// </summary>
        private Renderer renderer;
        /// <summary>
        /// The diffuse shader used to restore the original material state.
        /// </summary>
        private Shader diffuseShader;
        /// <summary>
        /// The transparent shader used to make the material transparent.
        /// </summary>
        private Shader transparentShader;
        /// <summary>
        /// A boolean flag used to keep track of the transparency state of the material.
        /// </summary>
        private bool transparenting;
        /// <summary>
        /// The tag used to identify the player gameobject.
        /// </summary>
        private string playerTag = "Player";
        /// <summary>
        /// The array of materials to be made transparent or restored to their original state.
        /// </summary>
        private Material[] mats;
        /// <summary>
        /// The length of the material array.
        /// </summary>
        private int matsLength;
        /// <summary>
        /// This method is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            renderer = GetComponentInParent<Renderer>();
            mats = renderer.materials;
            matsLength = mats.Length;
            diffuseShader = Shader.Find("PS Vita/Lit/Diffuse/Simple Diffuse");
            transparentShader = Shader.Find("PS Vita/Lit/Diffuse/Transparent");
        }

        /// <summary>
        /// This method is called every frame while the collider is colliding with another collider.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                for (int i = 0; i < matsLength; i++)
                {
                    if (mats[i].shader != transparentShader)
                        mats[i].shader = transparentShader;
                    mats[i].SetFloat("_Alpha", 0.4f);
                }
            }
        }
        /// <summary>
        /// This method is called when the collider has stopped colliding with another collider.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(playerTag))
            {
                for (int i = 0; i < matsLength; i++)
                {
                    mats[i].shader = diffuseShader;
                }
            }
        }
    }
}