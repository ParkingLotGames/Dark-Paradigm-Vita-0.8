using UnityEngine;
using DP.DevTools;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using DP.ResourceManagement;
#endif
// TODO: Remove Singleton pattern

namespace DP
{
    public class LoadScreen : MonoBehaviour
    {
        public static LoadScreen Instance;
        #region Variables
        [SerializeField] Sprite[] loadscreens;
        [SerializeField]public Image loadProgressSlider, loadScreenSprite;
        [SerializeField]public GameObject loadProggressContainer;
        #region Private Variables
        int loadscreensLength;
        float loadProgress;
        bool isLoading;
        AsyncOperation loadOperation;
        #endregion

        #endregion

        #region Methods

        void Awake()
        {
            loadscreensLength = loadscreens.Length;
            Singleton();
        }

        void Singleton()
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

        public void LoadSceneByIndex(int index)
        {
            if (!isLoading)
            {
                loadScreenSprite.sprite = loadscreens[Random.Range(0, loadscreensLength)];
                StartCoroutine(LoadWithIndex(index));
            }
        }
        public void LoadSceneByName(string name)
        {
            if (!isLoading)
            {
                loadScreenSprite.sprite = loadscreens[Random.Range(0, loadscreensLength)];
                StartCoroutine(LoadWithName(name));
            }
        }
        IEnumerator LoadWithIndex(int index)
        {
            yield return null;
            loadOperation = SceneManager.LoadSceneAsync(index);
            while (!loadOperation.isDone)
            {
                isLoading = true;
                loadScreenSprite.gameObject.SetActive(true);
                loadProggressContainer.SetActive(true);
                loadProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
                loadProgressSlider.fillAmount = loadProgress;
                yield return null;
            }
        }
        IEnumerator LoadWithName(string name)
        {

            yield return null;
            loadOperation = SceneManager.LoadSceneAsync(name);
            while (!loadOperation.isDone)
            {
                isLoading = true;
                loadProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
                loadProgressSlider.fillAmount = loadProgress;
                yield return null;
            }
        }
        #endregion
    }

    #region CustomInspector
#if UNITY_EDITOR
    [CustomEditor(typeof(LoadScreen))]
    [CanEditMultipleObjects]

    public class CustomLoadScreenInspector : Editor
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
            LoadScreen LoadScreen = (LoadScreen)target;
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