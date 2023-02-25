/*
#if UNITY_EDITOR

#endif




#if UNITY_ANDROID

#endif

#if UNITY_PS4

#endif

#if UNITY_PSP2

#endif

#if UNITY_STANDALONE || UNITY_EDITOR

#endif

#if UNITY_WINRT

#endif

#if UNITY_XBOXONE

#endif


#if UNITY_STANDALONE

#endif

*/

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DP.Gameplay
{
	public class BulletSpawn : MonoBehaviour 
	{

		public static BulletSpawn Instance;
        void OnEnable() { Instance = this; }
        void OnDisable() { Instance = null; }
    }

#if UNITY_EDITOR
	[CustomEditor(typeof(BulletSpawn))]
	public class CustomBulletSpawnInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
}