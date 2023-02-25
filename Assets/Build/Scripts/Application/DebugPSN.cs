using UnityEngine;
using DP.DevTools;
using UnityEngine.UI;
using Sony.NP;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// A script to display PlayStation Network (PSN) statistics in a Unity game's UI, including network information, sign-in status, and more.
    /// </summary>
    public class DebugPSN : MonoBehaviour
    {
        Text text;
        int run;

        /// <summary>
        /// This function is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            // Get a reference to the Text component attached to the GameObject this script is attached to.
            text = GetComponent<Text>();
        }

        /// <summary>
        /// This function is called before the first frame update.
        /// </summary>
        private void Start()
        {
            // Start the coroutine to update the PSN statistics in the Text component.
            StartCoroutine(UpdateStats());
        }

        /// <summary>
        /// A coroutine to repeatedly update the PSN statistics in the Text component.
        /// </summary>
        IEnumerator UpdateStats()
        {
            while (true)
            {
                // Wait for 0.5 seconds before executing the next iteration of the loop.
                yield return new WaitForSeconds(.5f);

                // Increment the number of times this script has been run.
                run++;

                // Update the Text component with PSN statistics, using preprocessor directives to run on PS Vita hardware but not the Unity Editor.
#if UNITY_PSP2 && !UNITY_EDITOR
            text.text = $"Connection Up: {Sony.NP.System.connectionUp} \nGet Net Info: {Sony.NP.System.GetNetInfo()} \nGet Network Device" +
                $"{Sony.NP.System.GetNetworkDeviceType()} \nGet Network Time{Sony.NP.System.GetNetworkTime()} \nIs Connected: {Sony.NP.System.IsConnected}" +
                $"\nIs Signed In:{Sony.NP.User.IsSignedIn} \nIs Signed In PSN {Sony.NP.User.IsSignedInPSN} \nIs Signin Busy {User.IsSigninBusy}" +
                $"\n Is User Profile Busy{User.IsUserProfileBusy} \nUser Signed In {User.signedIn} \nUserProfileBusy(){User.UserProfileIsBusy()}";
#endif
#if UNITY_EDITOR
                text.text = $"\n{run} times run";
#endif
            }
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(DebugPSN))]
	[CanEditMultipleObjects]

    public class CustomDebugPSNInspector : Editor
	{
    public override void OnInspectorGUI()
    {
#region GUIStyles
        //Define GUIStyles
        
#endregion

#region Layout Widths
        GUILayoutOption width32 = GUILayout.Width(32);
        GUILayoutOption width40 = GUILayout.Width(40);
        GUILayoutOption width48 = GUILayout.Width(48);
        GUILayoutOption width64 = GUILayout.Width(64);
        GUILayoutOption width80 = GUILayout.Width(80);
        GUILayoutOption width96 = GUILayout.Width(96);
        GUILayoutOption width112 = GUILayout.Width(112);
        GUILayoutOption width128 = GUILayout.Width(128);
        GUILayoutOption width144 = GUILayout.Width(144);
        GUILayoutOption width160 = GUILayout.Width(160);
#endregion

        base.OnInspectorGUI();
        DebugPSN DebugPSN = (DebugPSN)target;
        //SerializedProperty example = serializedObject.FindProperty("Example");
        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();

        EditorGUIUtility.labelWidth = 80;
        //EditorGUILayout.PropertyField(example);

        EditorGUILayout.EndHorizontal();


        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion
}