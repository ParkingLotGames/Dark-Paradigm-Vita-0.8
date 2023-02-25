using UnityEngine;
using DP.DevTools;
using System.Collections.Generic;
using System;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP.Gameplay
{
    /// <summary>
    /// Top-down camera, follows one or more player objects.
    /// </summary>
    public class TopDownCameraBehavior : MonoBehaviour
    {
        /// <summary>
        /// The list of player objects that the camera should follow.
        /// </summary>
        [SerializeField] List<Transform> players;

        /// <summary>
        /// The offset from the center of the players' bounds that the camera should maintain.
        /// </summary>
        [SerializeField] Vector3 offset;

        /// <summary>
        /// The time in seconds it should take for the camera to move to its new position.
        /// </summary>
        [SerializeField] float smoothing;

        /// <summary>
        /// The distance between players beyond which the camera should zoom out.
        /// </summary>
        [SerializeField] float zoomLimit;

        /// <summary>
        /// The maximum field of view that the camera should use when zoomed out.
        /// </summary>
        [SerializeField] float maxZoom;

        /// <summary>
        /// The minimum field of view that the camera should use when zoomed in.
        /// </summary>
        [SerializeField] float minZoom;

        /// <summary>
        /// The speed at which the camera should zoom in and out.
        /// </summary>
        [SerializeField] float zoomSpeed;

        /// <summary>
        /// The camera component that this behavior is controlling.
        /// </summary>
        Camera tpsCamera;

        /// <summary>
        /// The velocity of the camera's movement when it is following the players.
        /// </summary>
        Vector3 velocity;

        /// <summary>
        /// The velocity of the camera's zoom when it is zooming in or out.
        /// </summary>
        float fVelocity;

        /// <summary>
        /// The layers in the game that are considered part of the level, and should be included in bounds calculations.
        /// </summary>
        [SerializeField] LayerMask levelLayers;

        /// <summary>
        /// The bounds of the level, calculated from the positions of the players and the objects in the level.
        /// </summary>
        Bounds bounds;

        /// <summary>
        /// The shader that is used for objects in the level that are not selected.
        /// </summary>
        Shader diffuse;

        /// <summary>
        /// The shader that is used for objects in the level that are selected.
        /// </summary>
        Shader transparent;

        /// <summary>
        /// The object in the level that is currently selected.
        /// </summary>
        GameObject selected;

        /// <summary>
        /// The object in the level that was last selected before the current object.
        /// </summary>
        GameObject lastSelected;

        /// <summary>
        /// The transparent object that is currently being displayed, if any.
        /// </summary>
        GameObject currentTranspObj;

        /// <summary>
        /// Sets the camera component to the one attached to this game object.
        /// </summary>
        void Awake()
        {
            tpsCamera = GetComponent<Camera>();
            diffuse = Shader.Find("PS Vita/Lit/Diffuse/Simple Diffuse");
        }

        /// <summary>
        /// Updates the position and zoom of the camera based on the positions of the players and the bounds of the level.
        /// </summary>
        private void LateUpdate()
        {
            // Calculate the bounds of the level based on the positions of the players and the level objects.
            bounds = new Bounds(players[0].position, new Vector3(0, 0, 0));
            for (int i = 0; i < players.Count; i++)
            {
                bounds.Encapsulate(players[i].position);
            }
            // Calculate the center of the bounds and the distance between the players.
            Vector3 boundsCenter = bounds.center;
            float largestDistanceBetweenPlayers = bounds.size.x;

#if UNITY_EDITOR
            Debug.Log($" Bounds Size = {bounds.size.x}");
#endif
            // Calculate the new position of the camera, and move it smoothly towards that position.
            Vector3 newPos = boundsCenter + offset;
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothing);

            // Calculate the new field of view of the camera, and zoom it smoothly towards that field of view.
            float zoom = Mathf.Lerp(maxZoom, minZoom, largestDistanceBetweenPlayers / zoomLimit);
            tpsCamera.fieldOfView = Mathf.SmoothDamp(tpsCamera.fieldOfView, zoom, ref fVelocity, zoomSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Calculates the center of the bounds of the level based on the positions of the players and the level objects.
        /// </summary>
        /// <returns>The center of the bounds of the level.</returns>
        Vector3 GetBoundsCenter()
        {
            if (players.Count == 1)
                return players[0].position;

            // Calculate the bounds of the level based on the positions of the players and the level objects.
            var bounds = new Bounds(players[0].position, new Vector3(0, 0, 0));
            for (int i = 0; i < players.Count; i++)
            {
                bounds.Encapsulate(players[i].position);
            }

            // Return the center of the bounds.
            return bounds.center;
        }
    }

        #region CustomInspector
#if UNITY_EDITOR
        [CustomEditor(typeof(TopDownCameraBehavior))]
	[CanEditMultipleObjects]

    public class CustomTopDownCameraBehaviorInspector : Editor
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
        TopDownCameraBehavior TopDownCameraBehavior = (TopDownCameraBehavior)target;
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