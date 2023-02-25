using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.ScriptableObjects
{
    [CreateAssetMenu]
    public class FootstepSoundsContainer : ScriptableObject
    {
        [SerializeField] public AudioClip[] footstepSounds;
    }
}