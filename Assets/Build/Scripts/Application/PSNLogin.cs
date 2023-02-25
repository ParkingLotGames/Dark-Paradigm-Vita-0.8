using UnityEngine;
using DP.DevTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// A class for handling PlayStation Network (PSN) login.
    /// </summary>
    public class PSNLogin : MonoBehaviour
    {
        #region Variables

        /// <summary>
        /// A UI text element used for displaying error messages.
        /// </summary>
        [SerializeField] Text errorToast;

        /// <summary>
        /// The sprite used when the user is signed in to PSN.
        /// </summary>
        [SerializeField] Sprite PSNSprite;

        /// <summary>
        /// The sprite used when the user is signed out of PSN.
        /// </summary>
        [SerializeField] Sprite offlineSprite;

        #region Private Variables

#if !UNITY_EDITOR
    Sony.NP.User.UserProfile user;
#endif

        bool loggedIn, PSNUpdating;

        #endregion

        #endregion

        #region Methods

#if UNITY_PSP2 && !UNITY_EDITOR

        /// <summary>
        /// Called when a new level is loaded. If the user is not signed in to PSN, connects to PSN after a 1.5 second delay.
        /// </summary>
        /// <param name="level">The index of the new level.</param>
        void OnLevelWasLoaded(int level)
        {
            if (!Sony.NP.User.IsSignedInPSN)
                Invoke("ConnectToPSN", 1.5f);
        }

        /// <summary>
        /// Called when the object is enabled. If the user is not signed in to PSN, connects to PSN after a 1.5 second delay.
        /// </summary>
        private void OnEnable()
        {
            if (!Sony.NP.User.IsSignedInPSN)
                Invoke("ConnectToPSN", 1.5f);
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// Connects to PSN.
        /// </summary>
        public void ConnectToPSN()
        {
            PSNUpdating = true;
            Sony.NP.Main.OnNPInitialized += OnNPInitialized;
            //NotificationToast.Instance.SetNotification("Connecting to Server...", PSNSprite);
            Sony.NP.Main.enableInternalLogging = true;
            Sony.NP.Main.Initialize(Sony.NP.Main.kNpToolkitCreate_DoNotInitializeTrophies);
            string sessionImage = Application.streamingAssetsPath + "/PSP2SessionImage.jpg";
            Sony.NP.Main.SetSessionImage(sessionImage);
            Sony.NP.User.OnSignedIn += OnSignedIn;
            Sony.NP.User.OnSignedOut += OnSignedOut;
            Sony.NP.User.OnSignInError += OnSignInError;
        }

        /// <summary>
        /// Called when the NP toolkit has been initialized. Signs the user in to PSN.
        /// </summary>
        /// <param name="msg">The plugin message.</param>
        void OnNPInitialized(Sony.NP.Messages.PluginMessage msg)
        {
            NotificationToast.Instance.SetNotification("Connected to Server!\nSigning in...", PSNSprite);
            Sony.NP.User.SignIn();
        }

        /// <summary>
        /// Called when the user is signed in to PSN. Gets the user's profile and displays a notification.
        /// </summary>
        /// <param name="msg">The plugin message.</param>
        void OnSignedIn(Sony.NP.Messages.PluginMessage msg)
        {
            user = Sony.NP.User.GetCachedUserProfile();
            NotificationToast.Instance.SetNotification("Signed in!", PSNSprite);

        }

        /// <summary>
        /// Called when the user is signed out of PSN. Displays a notification.
        /// </summary>
        /// <param name="msg">The plugin message.</param>
        void OnSignedOut(Sony.NP.Messages.PluginMessage msg)
        {
            NotificationToast.Instance.SetNotification("Signed out!", offlineSprite);
            PSNUpdating = false;
        }
        
        /// <summary>
        /// Called when there is an error signing the user in to PSN. Displays an error message.
        /// </summary>
        /// <param name="msg">The plugin message.</param>
        void OnSignInError(Sony.NP.Messages.PluginMessage msg)
        {
            errorToast.transform.parent.gameObject.SetActive(false);
            errorToast.text = ("Error " + msg.Text);
            errorToast.transform.parent.gameObject.SetActive(true);
            PSNUpdating = false;
        }
        
        /// <summary>
        /// Called once per frame. Updates the NP toolkit if the user is signed in to PSN.
        /// </summary>
        private void Update()
        {
            if(PSNUpdating)
            Sony.NP.Main.Update();
        }
#endif
        /// <summary>
        /// The instance of the PSNLogin class.
        /// </summary>
        public static PSNLogin Instance;
        /// <summary>
        /// Called when the object is created. If there is no instance of the PSNLogin class, sets this as the instance and makes it persistent. Otherwise, destroys this object.
        /// </summary>
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
#endregion
    }

#region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(PSNLogin))]
    [CanEditMultipleObjects]

    public class CustomPSNLoginInspector : Editor
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
            PSNLogin PSNLogin = (PSNLogin)target;
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