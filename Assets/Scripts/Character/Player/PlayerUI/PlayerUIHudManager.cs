using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

namespace ART
{
    public class PlayerUIHudManager : MonoBehaviour
    {
        public PlayerManager player;
        [SerializeField] private CanvasGroup[] canvasGroup;

        [Header("HINT CONTROLLER")]
        [SerializeField] private GameObject controller;

        [SerializeField] private GameObject reviveController;
        [SerializeField] private GameObject itemsController;
        [SerializeField] private GameObject messegeController;
        [SerializeField] private GameObject menuController;

        [Header("HINT MOUSEKEYBOARD")]
        [SerializeField] private GameObject mouseKeyboard;

        [SerializeField] private GameObject reviveMouseKeyboard;
        [SerializeField] private GameObject itemsMouseKeyboard;
        [SerializeField] private GameObject messegeMouseKeyboard;
        [SerializeField] private GameObject menuMouseKeyboard;

        [Header("STAT BARS")]
        [SerializeField] private UI_StatBar healthBar;

        [SerializeField] private UI_StatBar staminaBar;

        [Header("QUICK SLOTS")]
        [SerializeField] private Image rightWeaponQuickSlotIcon;

        [SerializeField] private Image leftWeaponQuickSlotIcon;

        [Header("BOSS HEALTH BAR")]
        public Transform bossHealthBarParent;

        public GameObject bossHealthBarObject;

        private void Start()
        {
            ToggleHUD(true);
        }

        public void ToggleHUD(bool status)
        {
            // TO DO FADE IN AND OUT OVER TIME
            if (PlayerInputManager.instance.isUsingMouseKeyboard)
            {
                controller.SetActive(false);
                reviveController.SetActive(false);
                itemsController.SetActive(false);
                messegeController.SetActive(false);

                mouseKeyboard.SetActive(true);
                reviveMouseKeyboard.SetActive(true);
                itemsMouseKeyboard.SetActive(true);
                messegeMouseKeyboard.SetActive(true);
            }
            else
            {
                mouseKeyboard.SetActive(false);
                reviveMouseKeyboard.SetActive(false);
                itemsMouseKeyboard.SetActive(false);
                messegeMouseKeyboard.SetActive(false);

                controller.SetActive(true);
                reviveController.SetActive(true);
                itemsController.SetActive(true);
                messegeController.SetActive(true);
            }
            if (status)
            {
                foreach (var canvas in canvasGroup)
                {
                    canvas.alpha = 1;
                }
                menuController.SetActive(false);
                menuMouseKeyboard.SetActive(false);
            }
            else
            {
                foreach (var canvas in canvasGroup)
                {
                    canvas.alpha = 0;
                }
                if (PlayerInputManager.instance.isUsingMouseKeyboard)
                {
                    menuController.SetActive(false);
                    menuMouseKeyboard.SetActive(true);
                }
                else
                {
                    menuController.SetActive(true);
                    menuMouseKeyboard.SetActive(false);
                }
            }
        }

        public void RefreshHUD()
        {
            healthBar.gameObject.SetActive(false);
            healthBar.gameObject.SetActive(true);

            staminaBar.gameObject.SetActive(false);
            staminaBar.gameObject.SetActive(true);
        }

        public void SetNewHealthValue(int oldValue, int newValue)
        {
            healthBar.SetStat(newValue);
        }

        public void SetMaxHealthValue(int maxHealth)
        {
            healthBar.SetMaxStat(maxHealth);
        }

        public void SetNewStaminaValue(float oldValue, float newValue)
        {
            staminaBar.SetStat(Mathf.RoundToInt(newValue));
        }

        public void SetMaxStaminaValue(int maxStamina)
        {
            staminaBar.SetMaxStat(maxStamina);
        }

        public void SetRightWeaponQuickSlotIcon(int weaponID)
        {
            //  IF THE DATABASE DOES NOT CONTAIN A WEAPON MATCHING THE GIVEN ID, RETURN

            WeaponItem weapon = WorldItemDatabase.Instance.GetWeaponByID(weaponID);
            if (weapon == null)
            {
                // Debug.Log("ITEM IS NULL");
                rightWeaponQuickSlotIcon.enabled = false;
                rightWeaponQuickSlotIcon.sprite = null;
                return;
            }

            if (weapon.itemIcon == null)
            {
                rightWeaponQuickSlotIcon.enabled = false;
                rightWeaponQuickSlotIcon.sprite = null;
                // Debug.Log("No item icon found returned");
                return;
            }

            //  THIS IS WHERE YOU WOULD CHECK TO SEE IF YOU MEET THE ITEM REQS IF YOU WANT TO SHOW THE WARNING FOR NOT BEING ABLE TO WIELD IT
            rightWeaponQuickSlotIcon.sprite = weapon.itemIcon;
            rightWeaponQuickSlotIcon.enabled = true;
        }

        public void SetLeftWeaponQuickSlotIcon(int weaponID)
        {
            //  IF THE DATABASE DOES NOT CONTAIN A WEAPON MATCHING THE GIVEN ID, RETURN

            WeaponItem weapon = WorldItemDatabase.Instance.GetWeaponByID(weaponID);
            if (weapon == null)
            {
                // Debug.Log("ITEM IS NULL");
                leftWeaponQuickSlotIcon.enabled = false;
                leftWeaponQuickSlotIcon.sprite = null;
                return;
            }

            if (weapon.itemIcon == null)
            {
                leftWeaponQuickSlotIcon.enabled = false;
                leftWeaponQuickSlotIcon.sprite = null;
                // Debug.Log("No item icon found returned");
                return;
            }

            //  THIS IS WHERE YOU WOULD CHECK TO SEE IF YOU MEET THE ITEM REQS IF YOU WANT TO SHOW THE WARNING FOR NOT BEING ABLE TO WIELD IT
            leftWeaponQuickSlotIcon.sprite = weapon.itemIcon;
            leftWeaponQuickSlotIcon.enabled = true;
        }
    }
}