/*
#if UNITY_EDITOR

#endif




#if UNITY_ANDROID

#endif

#if UNITY_PS4

#endif

#if UNITY_PSP2

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
	public class EnemyPool : MonoBehaviour 
	{

		void Awake()
		{

		}
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(EnemyPool))]
	public class CustomEnemyPoolInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
}