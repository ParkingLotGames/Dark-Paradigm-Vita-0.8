using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
// TODO: Remove Singleton Behavior

namespace DP
{
    public class BossPool : MonoBehaviour 
	{
        public static BossPool Instance;

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
	[CustomEditor(typeof(BossPool))]
    public class CustomBossPoolInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
}