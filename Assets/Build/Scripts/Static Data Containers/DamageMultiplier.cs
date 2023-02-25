using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DP.StaticDataContainers
{
    /// <summary>
    /// Contains damage multipliers for different body parts that can be used in combat calculations.
    /// </summary>
    public static class DamageMultiplier
    {
        public static float headDamageMultiplier = 2.8f;//2.4
        public static float neckDamageMultiplier = 1.7f;//1.3
        public static float armDamageMultiplier = .6f;//.2
        public static float forearmDamageMultiplier = .4f;//.1
        public static float chestDamageMultiplier = 1.8f;//.8
        public static float torsoDamageMultiplier = 1.3f;//.4
        public static float legDamageMultiplier = .6f;//.1
        public static float upLegDamageMultiplier = .4f;//.2
        public static float hipsDamageMultiplier = .4f;//.4
    }

#if UNITY_EDITOR
[CustomEditor(typeof(DamageMultiplier))]
	public class CustomDamageMultiplierInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
		}
	}
#endif
}