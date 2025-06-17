using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ART
{
    public class WorldItemDatabase : MonoBehaviour
    {
        public static WorldItemDatabase Instance;

        public WeaponItem unarmedWeapon;

        public GameObject pickUpItemPrefab;

        [Header("Weapons")]
        [SerializeField] private List<WeaponItem> weapons = new List<WeaponItem>();

        [Header("Head Equipment")]
        [SerializeField] private List<HeadEquipmentItem> headEquipment = new List<HeadEquipmentItem>();

        [Header("Body Equipment")]
        [SerializeField] private List<BodyEquipmentItem> bodyEquipment = new List<BodyEquipmentItem>();

        [Header("Hand Equipment")]
        [SerializeField] private List<HandEquipmentItem> handEquipment = new List<HandEquipmentItem>();

        [Header("Leg Equipment")]
        [SerializeField] private List<LegEquipmentItem> legEquipment = new List<LegEquipmentItem>();

        [Header("Ashes Of War")]
        [SerializeField] private List<AshOfWar> ashesOfWar = new List<AshOfWar>();

        //  A LIST OF EVERY ITEM WE HAVE IN THE GAME
        [Header("Items")]
        private List<Item> items = new List<Item>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            //  ADD ALL OF OUR WEAPONS TO THE LIST OF ITEMS
            foreach (var weapon in weapons)
            {
                items.Add(weapon);
            }

            foreach (var head in headEquipment)
            {
                items.Add(head);
            }
            foreach (var body in bodyEquipment)
            {
                items.Add(body);
            }
            foreach (var hand in handEquipment)
            {
                items.Add(hand);
            }
            foreach (var leg in legEquipment)
            {
                items.Add(leg);
            }
            foreach (var item in ashesOfWar)
            {
                items.Add(item);
            }

            //  ASSIGN ALL OF OUR ITEMS A UNIQUE ITEM ID
            for (int i = 0; i < items.Count; i++)
            {
                items[i].itemID = i;
            }
        }

        public Item GetItemByID(int ID)
        {
            return items.FirstOrDefault(item => item.itemID == ID);
        } 

        public WeaponItem GetWeaponByID(int ID)
        {
            return weapons.FirstOrDefault(weapon => weapon.itemID == ID);
        }

        public HeadEquipmentItem GetHeadEquipmentItemByID(int ID)
        {
            return headEquipment.FirstOrDefault(equipment => equipment.itemID == ID);
        }

        public BodyEquipmentItem GetBodyEquipmentItemByID(int ID)
        {
            return bodyEquipment.FirstOrDefault(equipment => equipment.itemID == ID);
        }

        public LegEquipmentItem GetLegEquipmentItemByID(int ID)
        {
            return legEquipment.FirstOrDefault(equipment => equipment.itemID == ID);
        }

        public HandEquipmentItem GetHandEquipmentItemByID(int ID)
        {
            return handEquipment.FirstOrDefault(equipment => equipment.itemID == ID);
        }

        public AshOfWar GetAshOfWarByID(int ID)
        {
            return ashesOfWar.FirstOrDefault(item => item.itemID == ID);
        }
    }
}