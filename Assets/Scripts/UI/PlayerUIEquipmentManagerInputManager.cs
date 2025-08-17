using UnityEngine;

namespace ART
{
    public class PlayerUIEquipmentManagerInputManager : MonoBehaviour
    {
        private PlayerControls playerControls;

        private PlayerUIEquipmentManager playerUIEquipmentManager;

        [Header("Inputs")]
        [SerializeField] private bool unequipItemInput;

        private void Awake()
        {
            playerUIEquipmentManager = GetComponentInParent<PlayerUIEquipmentManager>();
        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerActions.X.performed += i => unequipItemInput = true;
            }

            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        private void Update()
        {
            HandlePlayerUIEquipmentManagerInputs();
        }

        private void HandlePlayerUIEquipmentManagerInputs()
        {
            if (unequipItemInput)
            {
                unequipItemInput = false;
                playerUIEquipmentManager.UnequipSelectedItem();
            }
        }
    }
}