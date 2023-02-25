using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Weapon/Headshot Sounds", fileName = "Headshot Sound Container")]
    public class HeadshotSoundsContainer : ScriptableObject 
	{
        [SerializeField]public AudioClip[] headshotSounds;
    }
}