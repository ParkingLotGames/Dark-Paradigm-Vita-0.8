using UnityEngine;
using UnityEngine.UI;
using DP.ResourceManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DP.Android
{
    /// <summary>
    /// A class used for scaling the screen on Android platforms.
    /// </summary>
    public class AndroidScreenScaler : MonoBehaviour
    {
        /// <summary>
        /// An instance of the AndroidScreenScaler class.
        /// </summary>
        public static AndroidScreenScaler Instance;

        /// <summary>
        /// A boolean indicating whether the current level is a game level.
        /// </summary>
        public bool gameLevel;

        /// <summary>
        /// An array of supported screen resolutions.
        /// </summary>
        public Resolution[] supportedResolutions;

        /// <summary>
        /// The number of supported screen resolutions.
        /// </summary>
        int resolutionsCount;

        /// <summary>
        /// The vertical layout group used for displaying screen resolutions.
        /// </summary>
        [SerializeField] VerticalLayoutGroup layoutGroup;

        /// <summary>
        /// The font used for displaying screen resolutions.
        /// </summary>
        Font font => Resources.Load<Font>(ResourcesPathContainer.oswaldFontPath);

        private void Awake()
        {
#if UNITY_ANDROID || UNITY_STANDALONE

            //Set Singleton
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
                Destroy(gameObject);

            //Get Supported Resolutions, set them in options
            supportedResolutions = Screen.resolutions;
            //Cache resolutions count, else this is calculated each execution
            resolutionsCount = supportedResolutions.Length;
            //print resolution count
            for (int i = 0; i < resolutionsCount; i++)
            {
                Text text = new GameObject().AddComponent<Text>();
                text.gameObject.SetActive(false);
                text.rectTransform.sizeDelta = new Vector2(200, 16);
                text.horizontalOverflow = HorizontalWrapMode.Overflow;
                text.verticalOverflow = VerticalWrapMode.Overflow;
                text.font = font;
                text.transform.parent = layoutGroup.transform;
                text.text = Screen.resolutions[i].ToString();
                text.gameObject.SetActive(true);
            }
#endif

#if UNITY_PS4
        Destroy(this);
#endif
        }

        private void OnLevelWasLoaded(int level)
        {
            if (level == 0)
                Screen.SetResolution(1280, 720, true);
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(AndroidScreenScaler))]
    public class CustomAndroidScreenScalerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            AndroidScreenScaler androidScreenScaler = (AndroidScreenScaler)target;
            //base.OnInspectorGUI();
        }
    }
#endif
}
