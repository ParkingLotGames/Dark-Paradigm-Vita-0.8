using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DP
{
	public class PlatformSelectionManager : MonoBehaviour 
	{
		public static PlatformSelectionManager Instance;

		void Awake()
		{
			if(!Instance)
			{
				Instance = this;
				//DontDestroyOnLoad(this);
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(PlatformSelectionManager))]
	public class CustomPlatformSelectionManagerInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
}