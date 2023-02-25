using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor; 
#endif

namespace DP.Gameplay
{
    /// <summary>
    /// This script controls the day and night cycle in the scene, including the movement of the sun and moon, the color of the sky, and the intensity of the sun and moon.
    /// </summary>
    public class DayNightCycle : MonoBehaviour
    {
        [SerializeField] Light sun, moon;
        [SerializeField] float secondsInFullDay = 1440f, secondsInDebugDay = 24f;
        [Range(0, 1)] [SerializeField] float currentTimeOfDay = 0;
        [SerializeField] bool debug;

        [Space]
        [SerializeField] Animator sunAnimator;
        [SerializeField] Animator moonAnimator;
        [SerializeField] Color[] daySkyboxColors, nightSkyboxColors;
        Color currentColor, selectedDayColor, selectedNightColor;
        Quaternion currentSunEuler, currentMoonEuler;
        float sunInitialIntensity, moonInitialIntensity;

        /// <summary>
        /// Start is called before the first frame update. It initializes the intensity of the sun and moon.
        /// </summary>
        void Start()
        {
            sunInitialIntensity = sun.intensity;
            moonInitialIntensity = moon.intensity;
        }

        /// <summary>
        /// Update is called once per frame. It updates the position of the sun and moon, as well as the color of the sky, based on the current time of day.
        /// </summary>
        void Update()
        {
            currentColor = RenderSettings.skybox.GetColor("_SkyTint");
            UpdateSun();
            UpdateMoon();
#if UNITY_EDITOR
            if (!debug)
                currentTimeOfDay += (Time.deltaTime / secondsInFullDay);
            else
                currentTimeOfDay += (Time.deltaTime / secondsInDebugDay);
#endif
#if !UNITY_EDITOR
        currentTimeOfDay += (Time.deltaTime / secondsInFullDay);
#endif
            if (currentTimeOfDay >= 1)
                currentTimeOfDay = 0;
        }

        /// <summary>
        /// UpdateSun is called every frame. It updates the position and intensity of the sun, as well as the color of the sky, based on the current time of day.
        /// </summary>
        public void UpdateSun()
        {
            currentSunEuler = Quaternion.Euler((currentTimeOfDay * 360f) - 90, -30, 0);
            sun.transform.parent.rotation = currentSunEuler;
            float sunIntensityMultiplier = 1.23f;
            if (currentTimeOfDay >= 0.25f && currentTimeOfDay <= .49f)
            {
                if (selectedDayColor == Color.clear)
                    selectedDayColor = daySkyboxColors[Random.Range(0, daySkyboxColors.Length - 1)];
                StartCoroutine(SkyColorLerp(selectedDayColor, 360));
                if (currentColor == selectedDayColor)
                    StopAllCoroutines();
                if (!sun.enabled)
                    sun.enabled = true;
                sunAnimator.Play("Dawn Sun");
            }
            else if (currentTimeOfDay >= 0.75f)
            {
                sunAnimator.Play("Sunset Sun");
            }
            if (currentTimeOfDay >= .75f || currentTimeOfDay <= .14f)
            {
                selectedDayColor = Color.clear;
            }
            if (currentTimeOfDay >= .88f || currentTimeOfDay <= .14f)
            {
                if (sun.enabled)
                    sun.enabled = false;
            }
            sun.intensity = sunIntensityMultiplier;
        }

        /// <summary>
        /// UpdateMoon is called every frame. It updates the position and intensity of the moon, based on the current time of day
        /// </summary>
        public void UpdateMoon()
        {
            currentMoonEuler = Quaternion.Euler((currentTimeOfDay * 360f) - 270, -30, 0);
            moon.transform.parent.rotation = currentMoonEuler;
            float moonIntensityMultiplier = 1.23f;
            if (currentTimeOfDay >= 0.15f && currentTimeOfDay <= .24f)
            {
                moonAnimator.Play("Dawn Moon");
            }
            else if (currentTimeOfDay >= 0.25f && currentTimeOfDay <= .59f)
            {
                if (moon.enabled)
                    moon.enabled = false;
                selectedNightColor = Color.clear;
                moonAnimator.Play("Empty");
            }
            else if (currentTimeOfDay >= 0.75f || currentTimeOfDay <= .24f)
            {
                if (selectedNightColor == Color.clear)
                    selectedNightColor = nightSkyboxColors[Random.Range(0, nightSkyboxColors.Length - 1)];
                StartCoroutine(SkyColorLerp(selectedNightColor, 360));
                if (currentColor == selectedNightColor)
                    StopAllCoroutines();
                if (!moon.enabled)
                    moon.enabled = true;
                moonAnimator.Play("Sunset Moon");
            }
            moon.intensity = moonIntensityMultiplier;
        }

        /// <summary>
        /// SkyColorLerp is called by UpdateSun and UpdateMoon. It interpolates the color of the sky from the current color to the specified end color over the specified duration of time.
        /// </summary>
        /// <param name="endValue">The end color of the sky.</param>
        /// <param name="duration">The duration of the color interpolation.</param>
        IEnumerator SkyColorLerp(Color endValue, float duration)
        {
            float time = 0;
            while (time < duration)
            {
                Color startValue = currentColor;
                RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(startValue, endValue, time / duration / 10));
                time += Time.deltaTime;
                yield return null;
            }
            RenderSettings.skybox.SetColor("_SkyTint", endValue);
        }
    }

        #region CustomInspector
#if UNITY_EDITOR
        [CustomEditor(typeof(DayNightCycle))]
	[CanEditMultipleObjects]

    public class CustomDayNightCycleInspector : Editor
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
            DayNightCycle dayNightCycle = (DayNightCycle)target;
            //SerializedProperty render sample = serializedObject.FindProperty("Example");

            if (GUILayout.Button("Position Light According to Day Time"))
            {
                dayNightCycle.UpdateSun();
                dayNightCycle.UpdateMoon();
            }

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