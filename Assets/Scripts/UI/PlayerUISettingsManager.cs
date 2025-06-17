using UnityEngine;
using System.Collections;
using TMPro;

namespace ART
{
    public class PlayerUISettingsManager : MonoBehaviour
    {
        [Header("Menu")]
        [SerializeField] private GameObject menu;

        [SerializeField] private TextMeshProUGUI changeInputText;

        public void OpenSettingsManagerMenu()
        {
            changeInputText.text = (PlayerInputManager.instance.isUsingMouseKeyboard ? "Mouse Keyboard" : "Controller");

            PlayerUIManager.instance.playerUICharacterMenuManager.CloseCharacterMenu();
            PlayerUIManager.instance.menuSettingsIsOpen = true;

            menu.SetActive(true);
        }

        //  THIS ISFINE BUT IF YOU R USING THE A BUTTON TO COSE MENUS YOU WILL JUMP AS YOU CLOSE THE MENU
        public void CloseSettingsManagerMenuAfterFixedFrame()
        {
            StartCoroutine(WaitThenCloseMenu());
        }

        private IEnumerator WaitThenCloseMenu()
        {
            yield return new WaitForFixedUpdate();
            PlayerUIManager.instance.playerUICharacterMenuManager.OpenCharacterMenu();
            PlayerUIManager.instance.menuSettingsIsOpen = false;

            menu.SetActive(false);
        }

        public void AttemptToChangeInput()
        {
            PlayerInputManager.instance.isUsingMouseKeyboard = !PlayerInputManager.instance.isUsingMouseKeyboard;
            changeInputText.text = (PlayerInputManager.instance.isUsingMouseKeyboard ? "Mouse Keyboard" : "Controller");
            PlayerInputManager.instance.ApplyInputSettings();
        }
    }
}