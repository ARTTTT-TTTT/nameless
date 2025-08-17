using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // ใช้ระบบ Input ใหม่ของ Unity
using UnityEngine.SceneManagement;

namespace ART
{
    public class PlayerUICharacterMenuManager : MonoBehaviour
    {
        [Header("Menu")]
        [SerializeField] private GameObject menu;

        private void DisableMouseClicks()
        {
            Cursor.lockState = CursorLockMode.Locked; // ล็อกเมาส์
            Cursor.visible = false; // ซ่อนเมาส์

            if (Mouse.current != null)
            {
                InputSystem.DisableDevice(Mouse.current); // ปิดการใช้งานเมาส์ทั้งหมด
            }
        }

        public void EnableMouseClicks()
        {
            Cursor.lockState = CursorLockMode.None; // ปลดล็อกเมาส์
            Cursor.visible = true; // แสดงเมาส์

            if (Mouse.current != null)
            {
                InputSystem.EnableDevice(Mouse.current); // เปิดการใช้งานเมาส์กลับมา
            }
        }

        public void OpenCharacterMenu()
        {
            PlayerUIManager.instance.playerUIHudManager.ToggleHUD(false);
            PlayerUIManager.instance.menuWindowIsOpen = true;
            PlayerUIManager.instance.menuPauseWindowIsOpen = true;
            menu.SetActive(true);
            DisableMouseClicks();
        }

        public void CloseCharacterMenu()
        {
            PlayerUIManager.instance.menuPauseWindowIsOpen = false;
            menu.SetActive(false);
        }

        public void ExitToMainMenu()
        {
            WorldSaveGameManager.instance.SaveGame();
            StartCoroutine(WaitAndLoadMainMenu());
        }

        private IEnumerator WaitAndLoadMainMenu()
        {
            yield return new WaitForSeconds(5f); // Wait for 5 seconds
            SceneManager.LoadScene(0); // Load the main menu scene (assuming it's scene 0)
            PlayerUIManager.instance.playerUICharacterMenuManager.CloseCharacterMenuAfterFixedFrame();
        }

        public void CloseCharacterMenuAfterFixedFrame()
        {
            StartCoroutine(WaitThenCloseMenu());
        }

        private IEnumerator WaitThenCloseMenu()
        {
            yield return new WaitForFixedUpdate();
            PlayerUIManager.instance.menuWindowIsOpen = false;
            PlayerUIManager.instance.menuPauseWindowIsOpen = false;
            menu.SetActive(false);
            EnableMouseClicks(); // เปิดเมาส์กลับ
            PlayerUIManager.instance.playerUIHudManager.ToggleHUD(true);
        }
    }
}