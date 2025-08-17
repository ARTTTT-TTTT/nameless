using UnityEngine;
using Unity.Netcode;

namespace ART
{
    public class AICharacterInventoryManager : CharacterInventoryManager
    {
        private AICharacterManager aiCharacter;

        [Header("Loot Chance")]
        public int dropItemChance = 10;

        [SerializeField] private Item[] dropableItem;

        protected override void Awake()
        {
            base.Awake();

            aiCharacter = GetComponent<AICharacterManager>();
        }

        public void DropItem()
        {
            if (!aiCharacter.IsOwner)
            {
                return;
            }
            bool willDropItem = false;

            int itemChanceRoll = Random.Range(0, 100);

            if (itemChanceRoll <= dropItemChance)
            {
                willDropItem = true;
            }

            if (!willDropItem)
            {
                return;
            }

            Item generatedItem = dropableItem[Random.Range(0, dropableItem.Length)];

            if (generatedItem == null)
            {
                return;
            }
            // ตรวจสอบว่าเป็นเซิร์ฟเวอร์ก่อน Spawn
            if (!NetworkManager.Singleton.IsServer) return;

            // ตรวจสอบว่า Prefab ถูกต้อง
            if (WorldItemDatabase.Instance.pickUpItemPrefab == null)
            {
                Debug.LogError("pickUpItemPrefab is missing in WorldItemDatabase!");
                return;
            }

            // สร้างอินสแตนซ์ของ Item
            GameObject itemPickUpInteractableGameObject = Instantiate(WorldItemDatabase.Instance.pickUpItemPrefab);

            // ตรวจสอบว่า GameObject มี NetworkObject
            NetworkObject networkObject = itemPickUpInteractableGameObject.GetComponent<NetworkObject>();
            if (networkObject == null)
            {
                Debug.LogError("NetworkObject component is missing on pickUpItemPrefab!");
                return;
            }

            // Spawn บนเซิร์ฟเวอร์
            networkObject.Spawn();

            PickUpItemInteractable pickUpInteractable = itemPickUpInteractableGameObject.GetComponent<PickUpItemInteractable>();

            // กำหนดค่าไอเท็ม
            pickUpInteractable.itemID.Value = generatedItem.itemID;
            pickUpInteractable.networkPosition.Value = transform.position;
        }
    }
}