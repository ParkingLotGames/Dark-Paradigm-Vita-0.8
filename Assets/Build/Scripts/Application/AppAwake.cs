using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
//using DP.DevTools;
#if UNITY_EDITOR
using UnityEditor;
//using DP.ResourceManagement;
#endif


namespace DP
{
    public class AppAwake : MonoBehaviour
    {
        [Serializable]
        public class StoredUserName
        {
            public bool cached;
            public string username;
        }
        //public static Awake Instance;
        #region Variables

        Sony.NP.User.UserProfile user;
        private bool userNameCached;
        #region Private Variables

        #endregion

        #endregion

        #region Methods

        void Start()
        {
            ReadFromFile();
            if (userNameCached)
                SceneManager.LoadScene(2);
        }

        private void ReadFromFile()
        {
            if (File.Exists("ux0:/data/plsUser.pls"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open("ux0:/data/plsUser.pls", FileMode.Open);
                StoredUserName sun = (StoredUserName)bf.Deserialize(file);
                file.Close();
                PlayerIDContainer.Instance.ID = sun.username;
                userNameCached = sun.cached;
            }
            else
                Np();
        }

        private void WriteToFile()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            StoredUserName sun = new StoredUserName();
            FileStream fileStream = File.Create("ux0:/data/plsUser.pls");
            sun.username = user.onlineID;
            sun.cached = true;
            binaryFormatter.Serialize(fileStream, sun);
            fileStream.Close();
            userNameCached = true;
            SceneManager.LoadScene(2);
        }

        private void Np()
        {
            Sony.NP.Main.OnNPInitialized += OnNPInitialized;
            //Debug.Log("Initializing NP");
            Sony.NP.Main.enableInternalLogging = true;
            Sony.NP.Main.Initialize(Sony.NP.Main.kNpToolkitCreate_DoNotInitializeTrophies);
            string sessionImage = Application.streamingAssetsPath + "/PSP2SessionImage.jpg";
            Sony.NP.Main.SetSessionImage(sessionImage);
            Sony.NP.User.OnSignedIn += OnSignedIn;
            Sony.NP.User.OnSignedOut += OnSignedOut;
            Sony.NP.User.OnSignInError += OnSignInError;
        }


        void OnNPInitialized(Sony.NP.Messages.PluginMessage msg)
        {
            //Debug.Log("NP Initialized");
            Sony.NP.User.SignIn();
        }

        void OnSignedIn(Sony.NP.Messages.PluginMessage msg)
        {
            user = Sony.NP.User.GetCachedUserProfile();
            WriteToFile();

            //Debug.Log("Signed IN!");
        }

        void OnSignedOut(Sony.NP.Messages.PluginMessage msg)
        {
            //Debug.Log("SIGNED OUT!");
        }

        void OnSignInError(Sony.NP.Messages.PluginMessage msg)
        {
            //Debug.Log("Error " + msg.Text);
        }

        private void Update()
        {
            Sony.NP.Main.Update();
            if (Sony.NP.User.IsSignedInPSN)
            {

                PlayerIDContainer.Instance.ID = user.onlineID;
                //PlayerIDContainer.Instance.userPfp = user.profilePictureUrl;

                if (PlayerIDContainer.Instance.ID != null)
                    PlayerIDContainer.Instance.fetched = true;

                if (PlayerIDContainer.Instance.fetched)
                    SceneManager.LoadScene(1);
            }
        }

        #endregion
    }

    #region CustomInspector
#if UNITY_EDITOR
    //[CustomEditor(typeof(AppAwake))]
    [CanEditMultipleObjects]

    public class CustomAwakeInspector : Editor
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
            AppAwake Awake = (AppAwake)target;
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