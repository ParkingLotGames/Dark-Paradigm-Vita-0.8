using DP.ResourceManagement;
using UnityEngine;

namespace DP.DevTools
{
    /// <summary>
    /// A static class containing GUI styles used in custom editor scripts.
    /// </summary>
    public static class CustomEditorGUIStyles
    {
        /// <summary>
        /// The GUIStyle used for float fields in the custom editor.
        /// </summary>
        public static GUIStyle floatField = new GUIStyle();

        /// <summary>
        /// The GUIStyle used for int fields in the custom editor.
        /// </summary>
        public static GUIStyle intField = new GUIStyle();

        /// <summary>
        /// The GUIStyle used for string fields in the custom editor.
        /// </summary>
        public static GUIStyle stringField = new GUIStyle();

        /// <summary>
        /// The GUIStyle used for dropdown menus in the custom editor.
        /// </summary>
        public static GUIStyle dropDown = new GUIStyle();

        /// <summary>
        /// The GUIStyle used for labels in the custom editor.
        /// </summary>
        public static GUIStyle label = new GUIStyle();

        /// <summary>
        /// The GUIStyle used for mid-sized labels in the custom editor.
        /// </summary>
        public static GUIStyle midLabel = new GUIStyle();

        /// <summary>
        /// The GUIStyle used for large labels in the custom editor.
        /// </summary>
        public static GUIStyle largeLabel = new GUIStyle();

        /// <summary>
        /// The GUIStyle used for header labels in the custom editor.
        /// </summary>
        public static GUIStyle headerLabel = new GUIStyle();

        /// <summary>
        /// The GUIStyle used for the header section in the custom editor.
        /// </summary>
        public static GUIStyle header = new GUIStyle();

        /// <summary>
        /// The GUIStyle used for subheader sections in the custom editor.
        /// </summary>
        public static GUIStyle subHeader = new GUIStyle();

        /// <summary>
        /// The GUIStyle used for subsubheader sections in the custom editor.
        /// </summary>
        public static GUIStyle subSubHeader = new GUIStyle();

        /// <summary>
        /// The GUIStyle used for labels in the subheader section of the custom editor.
        /// </summary>
        public static GUIStyle subHeaderLabel = new GUIStyle();

        /// <summary>
        /// The GUIStyle used for buttons in the custom editor.
        /// </summary>
        public static GUIStyle button = new GUIStyle();

        /// <summary>
        /// The GUIStyle used for red buttons in the custom editor.
        /// </summary>
        public static GUIStyle redButton = new GUIStyle();

        /// <summary>
        /// Sets the GUI styles for the custom editor.
        /// </summary>
        public static void SetStyles()
        {
            Font font;
            //Get Font
            font = (Font)Resources.Load(ResourcesPathContainer.oswaldFontPath);
            //Set Header GUIStyle
            header.font = font;
            header.fontSize = 20;
            header.fontStyle = FontStyle.Bold;
            header.alignment = TextAnchor.MiddleCenter;
            header.normal.textColor = Color.white;
            //Set Header GUIStyle
            subHeader.font = font;
            subHeader.fontSize = 18;
            subHeader.fontStyle = FontStyle.Bold;
            subHeader.normal.textColor = Color.white;
            //Set Button GUIStyle
            button.font = font;
            button.fontSize = 13;
            button.fontStyle = FontStyle.Bold;
            button.alignment = TextAnchor.MiddleCenter;
            button.normal.textColor = Color.white;
            button.normal.background = (Texture2D)Resources.Load(ResourcesPathContainer.editorButtonBlack);
            button.normal.background = (Texture2D)Resources.Load(ResourcesPathContainer.editorButtonBlackClick);

            //Set Red Button GUIStyle
            redButton.font = font;
            redButton.fontSize = 13;
            redButton.fontStyle = FontStyle.Bold;
            redButton.alignment = TextAnchor.MiddleCenter;
            redButton.normal.textColor = Color.white;
            redButton.normal.background = (Texture2D)Resources.Load(ResourcesPathContainer.editorButtonRed);
            redButton.active.background = (Texture2D)Resources.Load(ResourcesPathContainer.editorButtonRedClick);

            //Set Subclass GUIStyle
            subSubHeader.font = font;
            subSubHeader.fontSize = 15;
            subSubHeader.normal.textColor = Color.white;

        }
    }
}