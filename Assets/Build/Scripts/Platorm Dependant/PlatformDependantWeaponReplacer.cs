using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DP
{
	public class PlatformDependantWeaponReplacer : MonoBehaviour 
	{
		public static PlatformDependantWeaponReplacer Instance;

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
	[CustomEditor(typeof(PlatformDependantWeaponReplacer))]
	public class CustomPlatformDependantWeaponReplacerInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
}