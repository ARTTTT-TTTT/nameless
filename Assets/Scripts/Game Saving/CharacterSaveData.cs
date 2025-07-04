using UnityEngine;

namespace ART
{
    [System.Serializable]
    public class CharacterSaveData
    {
        [Header("SCENE INDEX")]
        public int sceneIndex = 1;

        [Header("Character Name")]
        public string characterName = "Character";

        [Header("Body Type")]
        public bool isMale = true;

        [Header("Time Played")]
        public float secondsPlayed;

        // QUESTION: WHY NOT SAVE VECTOR3?
        // ANSWER: WE CAN ONLY SAVE DATA FROM "BASIC" VARIABLE TYPES (float, int, string, bool, ect)
        [Header("World Coordinates")]
        public float xPosition;

        public float yPosition;
        public float zPosition;

        [Header("Resources")]
        public int currentHealth;

        public float currentStamina;

        [Header("Stats")]
        public int vitality;

        public int endurance;

        [Header("Site Of Grace")]
        public SerializableDictionary<int, bool> sitesOfGrace;   // THE INT IS SITE OF GRACE ID, AND THE BOOL IS THE ACTIVATED STATUS

        [Header("Bosses")]
        public SerializableDictionary<int, bool> bossesAwakened;  //  The int is boss id, bool is the awakened status

        public SerializableDictionary<int, bool> bossesDefeated;  //  The int is boss id, bool is the defeated status

        [Header("World Items")]
        public SerializableDictionary<int, bool> worldItemsLooted;  //  The int is item id, bool is the looted status

        [Header("Equipment")]
        public int headEquipment;

        public int bodyEquipment;
        public int legEquipment;
        public int handEquipment;

        public int rightWeaponIndex;
        public int rightWeapon01;
        public int rightWeapon02;
        public int rightWeapon03;

        public int leftWeaponIndex;
        public int leftWeapon01;
        public int leftWeapon02;
        public int leftWeapon03;

        public CharacterSaveData()
        {
            sitesOfGrace = new SerializableDictionary<int, bool>();
            bossesAwakened = new SerializableDictionary<int, bool>();
            bossesDefeated = new SerializableDictionary<int, bool>();
            worldItemsLooted = new SerializableDictionary<int, bool>();
        }
    }
}