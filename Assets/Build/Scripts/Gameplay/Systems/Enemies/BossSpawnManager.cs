using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DP
{
	public class BossSpawnManager : MonoBehaviour 
	{
		public static BossSpawnManager Instance;

		void Awake()
		{
			if(!Instance)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(BossSpawnManager))]
	public class CustomBossSpawnManagerInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
}