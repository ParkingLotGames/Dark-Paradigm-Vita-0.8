using UnityEngine;
using DP.DevTools;
using UnityEngine.UI;
using System.Collections;
using static Sony.NP.User;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif


namespace DP
{
    /// <summary>
    /// Downloads and sets the PSN profile picture of the logged in user.
    /// </summary>
    public class DownloadPSNPfp : MonoBehaviour
    {
        public static DownloadPSNPfp Instance;
        Image PSNPfp;
        UserProfile profile;

        /// <summary>
        /// Downloads and sets the image sprite of the user's profile picture.
        /// </summary>
        /// <param name="url">The url of the profile picture.</param>
        IEnumerator DownloadAndSetImage(string url)
        {
            using (WWW image = new WWW(url))
            {
                yield return image;
                //if (image.bytesDownloaded.Equals(0))
                //{
                //    log.text += ("Generic Message : " + image.error);
                //    log.text += ("Error Message : " + image.text);
                //}
                //else
                //{
                    Texture2D imageTex = image.texture;
                    Sprite imageSprite = Sprite.Create(imageTex, new Rect(Vector2.zero, new Vector2(imageTex.width, imageTex.height)), Vector2.zero);
                    PSNPfp.sprite = imageSprite;
                //}
            }
        }

        /// <summary>
        /// Sets up the PSN profile picture and begins the download after 1.5 seconds.
        /// </summary>
        public void Start()
        {
#if UNITY_PSP2 && !UNITY_EDITOR
        profile = Sony.NP.User.GetCachedUserProfile();
#endif
            PSNPfp = GetComponent<Image>();
            Invoke("StartDownload", 1.5f);
        }

        /// <summary>
        /// Begins downloading the profile picture.
        /// </summary>
        void StartDownload()
        {
            StartCoroutine(DownloadAndSetImage(profile.avatarURL));
        }
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(DownloadPSNPfp))]
	[CanEditMultipleObjects]

    public class CustomDownloadPSNPfpInspector : Editor
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
        DownloadPSNPfp DownloadPSNPfp = (DownloadPSNPfp)target;
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