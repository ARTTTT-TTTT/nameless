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
            // ��Ǩ�ͺ���������������͹ Spawn
            if (!NetworkManager.Singleton.IsServer) return;

            // ��Ǩ�ͺ��� Prefab �١��ͧ
            if (WorldItemDatabase.Instance.pickUpItemPrefab == null)
            {
                Debug.LogError("pickUpItemPrefab is missing in WorldItemDatabase!");
                return;
            }

            // ���ҧ�Թ�ᵹ��ͧ Item
            GameObject itemPickUpInteractableGameObject = Instantiate(WorldItemDatabase.Instance.pickUpItemPrefab);

            // ��Ǩ�ͺ��� GameObject �� NetworkObject
            NetworkObject networkObject = itemPickUpInteractableGameObject.GetComponent<NetworkObject>();
            if (networkObject == null)
            {
                Debug.LogError("NetworkObject component is missing on pickUpItemPrefab!");
                return;
            }

            // Spawn �����������
            networkObject.Spawn();

            PickUpItemInteractable pickUpInteractable = itemPickUpInteractableGameObject.GetComponent<PickUpItemInteractable>();

            // ��˹���������
            pickUpInteractable.itemID.Value = generatedItem.itemID;
            pickUpInteractable.networkPosition.Value = transform.position;
        }
    }
}