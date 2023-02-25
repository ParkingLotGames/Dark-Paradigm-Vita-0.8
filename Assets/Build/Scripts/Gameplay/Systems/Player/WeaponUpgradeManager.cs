

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace DP
{
    public class WeaponUpgradeManager : MonoBehaviour
    {
    }

#region CustomInspector
#if UNITY_EDITOR
	[CustomEditor(typeof(WeaponUpgradeManager))]
	//[CanEditMultipleObjects]
	public class CustomWeaponUpgradeManagerInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
#endregion
}