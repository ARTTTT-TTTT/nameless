using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ART
{
    public class PlayerUIPopUpManager : MonoBehaviour
    {
        [Header("Message Pop Up")]
        [SerializeField] private TextMeshProUGUI popUpMessageText;

        [SerializeField] private GameObject popUpMessageGameObject;

        [Header("Item Pop Up")]
        [SerializeField] private GameObject itemPopUpGameObject;

        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemAmount;

        [Header("YOU DIED Pop Up")]
        [SerializeField] private GameObject youDiedPopUpGameObject;

        [SerializeField] private TextMeshProUGUI youDiedPopUpBackgroundText;
        [SerializeField] private TextMeshProUGUI youDiedPopUpText;
        [SerializeField] private CanvasGroup youDiedPopUpCanvasGroup;   //  Allows us to set the alpha to fade over time

        [Header("BOSS DEFEATED Pop Up")]
        [SerializeField] private GameObject bossDefeatedPopUpGameObject;

        [SerializeField] private TextMeshProUGUI bossDefeatedPopUpBackgroundText;
        [SerializeField] private TextMeshProUGUI bossDefeatedPopUpText;
        [SerializeField] private CanvasGroup bossDefeatedPopUpCanvasGroup;   //  Allows us to set the alpha to fade over time

        [Header("GREACE RESTORED Pop Up")]
        [SerializeField] private GameObject graceRestoredPopUpGameObject;

        [SerializeField] private TextMeshProUGUI graceRestoredPopUpBackgroundText;
        [SerializeField] private TextMeshProUGUI graceRestoredPopUpText;
        [SerializeField] private CanvasGroup graceRestoredPopUpCanvasGroup;   //  Allows us to set the alpha to fade over time

        public void CloseAllPopUpWindows()
        {
            popUpMessageGameObject.SetActive(false);
            itemPopUpGameObject.SetActive(false);

            PlayerUIManager.instance.popUpWindowIsOpen = false;
        }

        public void SendPlayerMessagePopUp(string messageText)
        {
            popUpMessageGameObject.SetActive(true);
            PlayerUIManager.instance.popUpWindowIsOpen = true;
            popUpMessageText.text = messageText;
        }

        public void SendItemPopUp(Item item, int amount)
        {
            itemAmount.enabled = false;
            itemIcon.sprite = item.itemIcon;
            itemName.text = item.itemName;

            if (amount > 0)
            {
                itemAmount.enabled = true;
                itemAmount.text = "x" + amount.ToString();
            }

            itemPopUpGameObject.SetActive(true);
            PlayerUIManager.instance.popUpWindowIsOpen = true;
        }

        public void SendYouDiedPopUp()
        {
            // ACTIVATE POST PROCESSING EFFECTS

            youDiedPopUpGameObject.SetActive(true);
            youDiedPopUpBackgroundText.characterSpacing = 0;
            StartCoroutine(StretchPopUpTextOverTime(youDiedPopUpBackgroundText, 8, 19));
            StartCoroutine(FadeInPopUpOverTime(youDiedPopUpCanvasGroup, 5));
            StartCoroutine(WaitThenFadeOutPopUpOverTime(youDiedPopUpCanvasGroup, 2, 5));
            //  SCRECT OUT THE POP UP
            //  FADE IN THE POP UP
            // WA�T THEN FADE OUT THE POP UP
        }

        public void SendBossDefeatedPopUp(string bossDefeatedMessage)
        {
            bossDefeatedPopUpText.text = bossDefeatedMessage;
            bossDefeatedPopUpBackgroundText.text = bossDefeatedMessage;
            bossDefeatedPopUpGameObject.SetActive(true);
            bossDefeatedPopUpBackgroundText.characterSpacing = 0;
            StartCoroutine(StretchPopUpTextOverTime(bossDefeatedPopUpBackgroundText, 8, 19));
            StartCoroutine(FadeInPopUpOverTime(bossDefeatedPopUpCanvasGroup, 5));
            StartCoroutine(WaitThenFadeOutPopUpOverTime(bossDefeatedPopUpCanvasGroup, 2, 5));
        }

        public void SendGraceRestoredPopUp(string graceRestoredMessage)
        {
            graceRestoredPopUpText.text = graceRestoredMessage;
            graceRestoredPopUpBackgroundText.text = graceRestoredMessage;
            graceRestoredPopUpGameObject.SetActive(true);
            graceRestoredPopUpBackgroundText.characterSpacing = 0;
            StartCoroutine(StretchPopUpTextOverTime(graceRestoredPopUpBackgroundText, 8, 19));
            StartCoroutine(FadeInPopUpOverTime(graceRestoredPopUpCanvasGroup, 5));
            StartCoroutine(WaitThenFadeOutPopUpOverTime(graceRestoredPopUpCanvasGroup, 2, 5));
        }

        private IEnumerator StretchPopUpTextOverTime(TextMeshProUGUI text, float duration, float stretchAmount)
        {
            if (duration > 0)
            {
                text.characterSpacing = 0;  //  RESETS OUR CHARACTER SPACING
                float timer = 0;
                yield return null;

                while (timer < duration)
                {
                    timer = timer + Time.deltaTime;

                    text.characterSpacing = Mathf.Lerp(text.characterSpacing, stretchAmount, duration * (Time.deltaTime / 20));
                    yield return null;
                }
            }
        }

        private IEnumerator FadeInPopUpOverTime(CanvasGroup canvas, float duration)
        {
            if (duration > 0)
            {
                canvas.alpha = 0;
                float timer = 0;
                yield return null;

                while (timer < duration)
                {
                    timer += Time.deltaTime;

                    canvas.alpha = Mathf.Lerp(canvas.alpha, 1, duration * Time.deltaTime);
                    yield return null;
                }
            }
            canvas.alpha = 1;

            yield return null;
        }

        private IEnumerator WaitThenFadeOutPopUpOverTime(CanvasGroup canvas, float duration, float delay)
        {
            if (duration > 0)
            {
                while (delay > 0)
                {
                    delay -= Time.deltaTime;
                    yield return null;
                }

                canvas.alpha = 1;
                float timer = 0;
                yield return null;

                while (timer < duration)
                {
                    timer += Time.deltaTime;

                    canvas.alpha = Mathf.Lerp(canvas.alpha, 0, duration * Time.deltaTime);
                    yield return null;
                }
            }
            canvas.alpha = 0;

            yield return null;
        }
    }
}