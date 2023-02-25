using UnityEngine;
using Sony.Vita.Dialog;

namespace DP.PSVita
{
    /// <summary>
    /// Opens a dialog for the user to enter text input.
    /// </summary>
    public class ShowTextInputDialog : MonoBehaviour
    {
        /// <summary>
        /// Text typed by the user in the input dialog.
        /// </summary>
        [SerializeField] string typed;

        /// <summary>
        /// Opens a dialog for the user to enter text input.
        /// </summary>
        public void ShowDialog()
        {
            //Makes an object to store all the information of the dialog.
            Ime.ImeDialogParams info = new Ime.ImeDialogParams();

            // Set supported languages, 'or' flags together or set to 0 to support all languages.
            info.supportedLanguages = 0;
            //Force them?
            info.languagesForced = true;
            //Which keyboard should pop up? The users default is selected here.
            info.type = Ime.EnumImeDialogType.TYPE_DEFAULT;
            //Input box display mode.
            info.option = Ime.FlagsTextBoxOption.OPTION_DEFAULT;
            //Can the user exit out?
            info.canCancel = true;
            //Text type. Password, normal, numbers, url etc 
            info.textBoxMode = Ime.FlagsTextBoxMode.TEXTBOX_MODE_WITH_CLEAR;
            //What should the enter button on the keyboard say? Leave it at default I say
            info.enterLabel = Ime.EnumImeDialogEnterLabel.ENTER_LABEL_GO;
            //max text length
            info.maxTextLength = 128;
            //Title of the popup
            info.title = "Type something";
            //Starting text (One thats in the dialog by default) like "Enter Name"
            info.initialText = "PLAYER";
            //Opens a dialog with the info object we made earlier
            Ime.Open(info);
            typed = info.ToString();
            Ime.GetResult();
        }
    }
}