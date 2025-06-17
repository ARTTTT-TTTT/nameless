using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace ART
{
    public class PickUpItemInteractable : Interactable
    {
        public ItemPickupType pickupType;

        [Header("Item")]
        [SerializeField] private Item item;

        [Header("Create Loot Pick Up")]
        public NetworkVariable<int> itemID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        public NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("World Spawn Pick Up")]
        [SerializeField] private int worldSpawnInteractableID;

        [SerializeField] private bool hasBeenLooted = false;

        protected override void Start()
        {
            base.Start();

            if (pickupType == ItemPickupType.WorldSpawn)
            {
                CheckIfWorldItemWasAlreadyLooted();
            }
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            itemID.OnValueChanged += OnItemiDChange;
            networkPosition.OnValueChanged += OnNetworkPositionChanged;
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            itemID.OnValueChanged -= OnItemiDChange;
            networkPosition.OnValueChanged -= OnNetworkPositionChanged;
        }

        private void CheckIfWorldItemWasAlreadyLooted()
        {
            //  IF THE PLAYER ISN'T HOST HIDE THE ITEM
            if (!NetworkManager.Singleton.IsHost)
            {
                gameObject.SetActive(false);
                return;
            }

            // COMPARE THE DATA OF THE LOOTED ITEMS ID S WITH THIS ITEMS ID
            if (!WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.ContainsKey(worldSpawnInteractableID))
            {
                WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.Add(worldSpawnInteractableID, false);
            }

            hasBeenLooted = WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted[worldSpawnInteractableID];

            // IF IT HAS BEEN LOOTED HIDE THE GAMEOBJECT
            if (hasBeenLooted)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }

        public override void Interact(PlayerManager player)
        {
            base.Interact(player);

            // 1 play sfx
            player.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.pickUpItemSFX);
            // 2 ad item to inventory
            player.playerInventoryManager.AddItemToInventory(item);
            // 3 show ui popup show item name n pic
            PlayerUIManager.instance.playerUIPopUpManager.SendItemPopUp(item, 1);

            // 4 SAVE LOOT STATUS IF IT IS A WORLD SPAWN
            if (pickupType == ItemPickupType.WorldSpawn)
            {
                if (WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.ContainsKey((int)worldSpawnInteractableID))
                {
                    WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.Remove(worldSpawnInteractableID);
                }
                WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.Add(worldSpawnInteractableID, true);
            }
            // 5 HIDE OR DESTROY THE GAME OBJECT
            if (NetworkManager.Singleton.IsServer)
            {
                GetComponent<NetworkObject>().Despawn();
            }
        }

        protected void OnItemiDChange(int oldvalue, int newvalue)
        {
            if (pickupType != ItemPickupType.CharacterDrop)
                return;

            item = WorldItemDatabase.Instance.GetItemByID(itemID.Value);
        }

        protected void OnNetworkPositionChanged(Vector3 oldPosition, Vector3 newPosition)
        {
            if (pickupType != ItemPickupType.CharacterDrop)
                return;

            transform.position = networkPosition.Value;
        }
    }
}