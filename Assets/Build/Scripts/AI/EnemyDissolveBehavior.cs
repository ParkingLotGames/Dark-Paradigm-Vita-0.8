using DP.DevTools;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DP.AI
{
    /// <summary>
    /// Controls the dissolve animation for an enemy character.
    /// </summary>
    public class EnemyDissolveBehavior : MonoBehaviour
    {
        [SerializeField] Shader dissolveShader, regularShader;
        [SerializeField] float dissolveDuration;
        [SerializeField] UnityEvent OnStartDissolve, OnStartUndissolve;
        SkinnedMeshRenderer[] skinnedMeshRenderers;
        int skinnedMeshRenderersLength;
        float currentDissolve;
        private float dissolveSpeed;

        private void Awake()
        {
            skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            skinnedMeshRenderersLength = skinnedMeshRenderers.Length;
        }

        /// <summary>
        /// Updates the dissolve animation each frame.
        /// </summary>
        private void Update()
        {
            InitDissolve();
        }

        /// <summary>
        /// Initiates the dissolve animation.
        /// </summary>
        public void Despawn()
        {
            InitDissolve();
        }

        /// <summary>
        /// Initiates the undissolve animation.
        /// </summary>
        public void Spawn()
        {
            InitUndissolve();
        }

        /// <summary>
        /// Initializes the dissolve animation.
        /// </summary>
        private void InitDissolve()
        {
            for (int i = 0; i < skinnedMeshRenderersLength; i++)
            {
                skinnedMeshRenderers[i].material.shader = dissolveShader;
                skinnedMeshRenderers[i].material.SetFloat("_DissolveAmount", 0);
                currentDissolve = 0;
                OnStartDissolve.Invoke();
            }
        }

        /// <summary>
        /// Initializes the undissolve animation.
        /// </summary>
        private void InitUndissolve()
        {
            for (int i = 0; i < skinnedMeshRenderersLength; i++)
            {
                skinnedMeshRenderers[i].material.shader = dissolveShader;
                skinnedMeshRenderers[i].material.SetFloat("_DissolveAmount", 0.65f);
                currentDissolve = 0.65f;
                OnStartUndissolve.Invoke();
            }
        }

        /// <summary>
        /// Performs the dissolve animation.
        /// </summary>
        public void DissolveAnimation()
        {
            for (int i = 0; i < skinnedMeshRenderersLength; i++)
            {
                if (currentDissolve < 0.65)
                {
                    skinnedMeshRenderers[i].material.SetFloat("_DissolveAmount", currentDissolve);
                    currentDissolve += Time.deltaTime * dissolveSpeed;
                }
            }
        }

        /// <summary>
        /// Performs the undissolve animation.
        /// </summary>
        public void UndissolveAnimation()
        {
            for (int i = 0; i < skinnedMeshRenderersLength; i++)
            {
                if (currentDissolve > 0)
                {
                    skinnedMeshRenderers[i].material.SetFloat("_DissolveAmount", currentDissolve);
                    currentDissolve -= Time.deltaTime * dissolveSpeed;
                }
            }
        }
    }
}