using UnityEngine;
using DP.DevTools;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Handles displaying a notification toast in the game.
    /// </summary>
    public class NotificationToast : MonoBehaviour
    {
        /// <summary>
        /// The instance of the NotificationToast that will be used to display notifications.
        /// </summary>
        public static NotificationToast Instance;

        /// <summary>
        /// The text object used to display the notification message.
        /// </summary>
        Text text;

        /// <summary>
        /// The image object used to display the notification icon.
        /// </summary>
        [SerializeField] Image notifImage;

        /// <summary>
        /// Called when the NotificationToast object is created.
        /// </summary>
        private void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);

            text = GetComponentInChildren<Text>(true);
        }

        /// <summary>
        /// Sets the notification text and icon for the NotificationToast and displays it.
        /// </summary>
        /// <param name="notification">The notification text to display.</param>
        /// <param name="image">The notification icon to display.</param>
        public void SetNotification(string notification, Sprite image)
        {
            gameObject.SetActive(false);
            text.text = notification;
            if (image)
            {
                notifImage.sprite = image;
                notifImage.gameObject.SetActive(true);
            }
            else
                notifImage.gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }


    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(NotificationToast))]
	[CanEditMultipleObjects]

    public class CustomNotificationToastInspector : Editor
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
        NotificationToast NotificationToast = (NotificationToast)target;
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