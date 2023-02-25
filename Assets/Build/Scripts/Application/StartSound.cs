﻿using UnityEngine;
using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Plays all AudioSources attached to this component.
    /// </summary>
    public class StartSound : MonoBehaviour
    {
        /// <summary>
        /// An array of AudioSources to play.
        /// </summary>
        [SerializeField] private AudioSource[] audios;

        /// <summary>
        /// Plays all AudioSources that are not currently playing.
        /// </summary>
        public void PlaySound()
        {
            for (int i = 0; i < audios.Length; i++)
            {
                if (!audios[i].isPlaying)
                {
                    audios[i].Play();
                }
            }
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(StartSound))]
	[CanEditMultipleObjects]

    public class CustomStartSoundInspector : Editor
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
        StartSound startSound = (StartSound)target;
        //SerializedProperty example = serializedObject.FindProperty("Example");
        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 80;
        //EditorGUILayout.PropertyField(example);
        if(GUILayout.Button("Play"))
            startSound.PlaySound();

            EditorGUILayout.EndHorizontal();


        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion
}