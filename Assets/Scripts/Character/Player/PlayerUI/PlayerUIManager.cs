using System.Collections;
using UnityEngine;
using Unity.Netcode;

namespace ART
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager instance;

        [Header("NETWORK JOIN")]
        [SerializeField] public bool startGameAsClient;

        [HideInInspector] public PlayerUIHudManager playerUIHudManager;
        [HideInInspector] public PlayerUIPopUpManager playerUIPopUpManager;
        [HideInInspector] public PlayerUICharacterMenuManager playerUICharacterMenuManager;
        [HideInInspector] public PlayerUIEquipmentManager playerUIEquipmentManager;
        [HideInInspector] public PlayerUISettingsManager playerUISettingsManager;
        [HideInInspector] public PlayerUIHintManager playerUIHintManager;

        [Header("UI FLAGS")]
        public bool hintWindowIsOpen = false;

        public bool menuWindowIsOpen = false;

        public bool menuPauseWindowIsOpen = false;
        public bool menuEquipmentIsOpen = false;
        public bool menuSettingsIsOpen = false;   //  INVENTORY SCREEN, EQUIPMENT MENU, BLACKSMITH MENU ETCpublic bool menuEquipmentIsOpen = false;
        public bool popUpWindowIsOpen = false;   //  ITEM PICKUP, DIALOG PUP UP ETC.

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            playerUIHudManager = GetComponentInChildren<PlayerUIHudManager>();
            playerUIPopUpManager = GetComponentInChildren<PlayerUIPopUpManager>();
            playerUICharacterMenuManager = GetComponentInChildren<PlayerUICharacterMenuManager>();
            playerUIEquipmentManager = GetComponentInChildren<PlayerUIEquipmentManager>();
            playerUISettingsManager = GetComponentInChildren<PlayerUISettingsManager>();
            playerUIHintManager = GetComponentInChildren<PlayerUIHintManager>();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (startGameAsClient)
            {
                startGameAsClient = false;
                StartCoroutine(RestartAsClient());
            }
        }

        private IEnumerator RestartAsClient()
        {
            NetworkManager.Singleton.Shutdown();

            yield return new WaitForSeconds(0.1f);

            Debug.Log("🔄 Restarting Client...");

            if (!NetworkManager.Singleton.StartClient())
            {
                Debug.LogError("❌ Client start failed.");
            }
        }

        public void CloseAllMenuWindows()
        {
            if (menuPauseWindowIsOpen)
            {
                playerUICharacterMenuManager.CloseCharacterMenuAfterFixedFrame();
            }
            if (menuEquipmentIsOpen)
            {
                playerUIEquipmentManager.CloseEquipmentManagerMenuAfterFixedFrame();
            }
            if (menuSettingsIsOpen)
            {
                playerUISettingsManager.CloseSettingsManagerMenuAfterFixedFrame();
            }
            if (hintWindowIsOpen)
            {
                playerUIHintManager.CloseHintManagerMenuAfterFixedFrame();
            }
        }
    }
}