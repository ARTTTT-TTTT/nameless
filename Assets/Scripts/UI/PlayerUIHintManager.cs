using System.Collections;
using UnityEngine;

namespace ART
{
    public class PlayerUIHintManager : MonoBehaviour
    {
        [Header("Hint")]
        [SerializeField] private GameObject hintController;

        [SerializeField] private GameObject hintMouseKeyboard;

        public void OpenHintManagerMenu()
        {
            PlayerUIManager.instance.hintWindowIsOpen = true;
            if (PlayerInputManager.instance.isUsingMouseKeyboard)
            {
                hintController.SetActive(false);
                hintMouseKeyboard.SetActive(true);
            }
            else
            {
                hintController.SetActive(true);
                hintMouseKeyboard.SetActive(false);
            }
        }

        //  THIS ISFINE BUT IF YOU R USING THE A BUTTON TO COSE MENUS YOU WILL JUMP AS YOU CLOSE THE MENU
        public void CloseHintManagerMenuAfterFixedFrame()
        {
            StartCoroutine(WaitThenCloseMenu());
        }

        private IEnumerator WaitThenCloseMenu()
        {
            yield return new WaitForFixedUpdate();
            PlayerUIManager.instance.hintWindowIsOpen = false;
            hintController.SetActive(false);
            hintMouseKeyboard.SetActive(false);
        }
    }
}