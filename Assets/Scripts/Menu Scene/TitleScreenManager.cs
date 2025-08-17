using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;
using System.Net;
using System.Collections;

namespace ART
{
    public class TitleScreenManager : MonoBehaviour
    {
        public static TitleScreenManager instance;
        public static PlayerUIManager playerUIManager;

        [Header("Hint")]
        private GameObject controller;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI hostIPText;

        [SerializeField] private TextMeshProUGUI changeInputText;

        [Header("Menus")]
        [SerializeField] private GameObject titleScreenMainMenu;

        [SerializeField] private GameObject titleScreenLoadMenu;
        [SerializeField] private GameObject titleScreenMultiplayerMenu;
        [SerializeField] private GameObject titleScreenSettingsMenu;

        [Header("Buttons")]
        [SerializeField] private Button loadMenuReturnButton;

        [SerializeField] private Button multiplayerMenuJoinGameButton;
        [SerializeField] private Button settingsChangeInputButton;
        [SerializeField] private Button mainMenuLoadGameButton;
        [SerializeField] private Button mainMenuNewGameButton;
        [SerializeField] private Button mainMenuMultiplayerButton;
        [SerializeField] private Button mainMenuSettingsButton;
        [SerializeField] private Button deleteCharacterPopUpNoButton;

        [Header("Pop Ups")]
        [SerializeField] private GameObject noCharacterSlotsPopUp;

        [SerializeField] private Button noCharacterSlotsOkayButton;
        [SerializeField] private GameObject deleteCharacterSlotPopUp;
        [SerializeField] private GameObject JoinGamePopUp;

        [Header("Character Slots")]
        public CharacterSlot currentSelectedSlot = CharacterSlot.NO_SLOT;

        [Header("Title Screen Inputs")]
        [SerializeField] private bool deleteCharacterSlot = false;

        [SerializeField] private TMP_InputField enterIPInput;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            playerUIManager = GetComponentInChildren<PlayerUIManager>();
        }

        public void StartNetworkAsHost()
        {
            var transport = NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>();

            transport.SetConnectionData("0.0.0.0", 7777);

            NetworkManager.Singleton.StartHost();
        }

        public void JoinGameAsClient()
        {
            string ipAddress = enterIPInput.text.Trim();

            if (string.IsNullOrEmpty(ipAddress))
            {
                Debug.LogError("❌ IP Address is empty!");
                return;
            }

            Debug.Log("🌐 Attempting to join game at IP: " + ipAddress);

            var transport = NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>();

            if (transport == null)
            {
                Debug.LogError("❌ UnityTransport component not found!");
                return;
            }

            transport.SetConnectionData(ipAddress, 7777);

            // เช็คก่อนว่า NetworkManager รันอยู่แล้วหรือไม่
            if (NetworkManager.Singleton.IsClient || NetworkManager.Singleton.IsServer)
            {
                Debug.LogWarning("⚠️ Client or Server is already running! Shutting down first...");
                NetworkManager.Singleton.Shutdown();
                StartCoroutine(WaitAndStartClient());
            }
            else
            {
                StartClientSafely();
            }
        }

        private IEnumerator WaitAndStartClient()
        {
            yield return new WaitForSeconds(0.5f); // รอให้ Shutdown เสร็จ
            StartClientSafely();
        }

        private void StartClientSafely()
        {
            if (!NetworkManager.Singleton.StartClient())
            {
                Debug.LogError("❌ Failed to start client.");
            }
            else
            {
                Debug.Log("✅ Client started successfully.");
            }
        }

        public void StartNewGame()
        {
            WorldSaveGameManager.instance.AttemptToCreateNewGame();
        }

        public void OpenLoadGameMenu()
        {
            // CLOSE THE MAIN MENU
            titleScreenMainMenu.SetActive(false);

            // OPEN THE LOAD MENU
            titleScreenLoadMenu.SetActive(true);

            // SELECT THE RETURN BUTTON FIRST
            loadMenuReturnButton.Select();
        }

        public void OpenMultiplayerMenu()
        {
            if (NetworkManager.Singleton.IsHost)
            {
                string ipAddress = GetLocalIPAddress();
                hostIPText.text = "Start HOST : " + ipAddress;
            }
            // CLOSE THE MAIN MENU
            titleScreenMainMenu.SetActive(false);

            // OPEN THE LOAD MENU
            titleScreenMultiplayerMenu.SetActive(true);

            // SELECT THE RETURN BUTTON FIRST
            multiplayerMenuJoinGameButton.Select();
        }

        public void OpenSettingsMenu()
        {
            changeInputText.text = "Input : " + (PlayerInputManager.instance.isUsingMouseKeyboard ? "Mouse Keyboard" : "Controller");
            titleScreenMainMenu.SetActive(false);

            titleScreenSettingsMenu.SetActive(true);

            settingsChangeInputButton.Select();
        }

        private string GetLocalIPAddress()
        {
            string localIP = "Not Found";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        public void CloseLoadGameMenu()
        {
            // CLOSE THE LOAD MENU
            titleScreenLoadMenu.SetActive(false);

            // OPEN THE MAIN MENU
            titleScreenMainMenu.SetActive(true);

            // SELECT THE LOAD BUTTON
            mainMenuLoadGameButton.Select();
        }

        public void CloseMultiplayerMenu()
        {
            // CLOSE THE LOAD MENU
            titleScreenMultiplayerMenu.SetActive(false);

            // OPEN THE MAIN MENU
            titleScreenMainMenu.SetActive(true);

            // SELECT THE LOAD BUTTON
            mainMenuNewGameButton.Select();
        }

        public void CloseSettingsMenu()
        {
            titleScreenSettingsMenu.SetActive(false);
            titleScreenMainMenu.SetActive(true);
            mainMenuSettingsButton.Select();
        }

        public void DisplayNoFreeCharacterSlotsPopUp()
        {
            noCharacterSlotsPopUp.SetActive(true);
            noCharacterSlotsOkayButton.Select();
        }

        public void CloseNoFreeCharacterSlotsPopUp()
        {
            noCharacterSlotsPopUp.SetActive(false);
            mainMenuNewGameButton.Select();
        }

        // CHARACTER SLOTS BELOW

        public void SelectCharacterSlot(CharacterSlot characterSlot)
        {
            currentSelectedSlot = characterSlot;
        }

        public void SelectNoSlot()
        {
            currentSelectedSlot = CharacterSlot.NO_SLOT;
        }

        public void AttemptToDeleteCharacterSlot()
        {
            if (currentSelectedSlot != CharacterSlot.NO_SLOT)
            {
                deleteCharacterSlotPopUp.SetActive(true);
                deleteCharacterPopUpNoButton.Select();
            }
        }

        public void AttemptToJoinGame()
        {
            JoinGamePopUp.SetActive(true);
            // enterIPInput.Select();
        }

        public void AttemptToChangeInput()
        {
            PlayerInputManager.instance.isUsingMouseKeyboard = !PlayerInputManager.instance.isUsingMouseKeyboard;
            changeInputText.text = "Input : " + (PlayerInputManager.instance.isUsingMouseKeyboard ? "Mouse Keyboard" : "Controller");
            PlayerInputManager.instance.ApplyInputSettings();
        }

        public void DeleteCharacterSlot()
        {
            deleteCharacterSlotPopUp.SetActive(false);
            WorldSaveGameManager.instance.DeleteGame(currentSelectedSlot);

            // WE DISABLE AND ENABLE THE LOAD MENU, TO REFRESH THE SLOTS (The deleted slots will now become inactive)
            titleScreenLoadMenu.SetActive(false);
            titleScreenLoadMenu.SetActive(true);

            loadMenuReturnButton.Select();
        }

        public void CloseDeleteCharacterPopUp()
        {
            deleteCharacterSlotPopUp.SetActive(false);
            loadMenuReturnButton.Select();
        }

        public void CloseJoinGamePopUp()
        {
            JoinGamePopUp.SetActive(false);
            multiplayerMenuJoinGameButton.Select();
        }
    }
}