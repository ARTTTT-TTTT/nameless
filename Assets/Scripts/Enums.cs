using UnityEngine;

namespace ART
{
    public class Enums : MonoBehaviour
    {
    }

    public enum CharacterSlot
    {
        CharacterSlot_01,
        CharacterSlot_02,
        CharacterSlot_03,
        CharacterSlot_04,
        CharacterSlot_05,
        CharacterSlot_06,
        CharacterSlot_07,
        CharacterSlot_08,
        CharacterSlot_09,
        CharacterSlot_10,
        NO_SLOT
    }

    public enum CharacterGroup
    {
        Team01,
        Team02,
        Team03
    }

    public enum WeaponModelSlot
    {
        RightHand,
        LeftHandWeaponSlot,
        LeftHandShieldSlot,
        BackSlot
        //Right Hips,
        //Left Hips,
    }

    public enum WeaponModelType
    {
        Weapon,
        Shield
    }

    public enum EquipmentModelType
    {
        FullHelmet,     // WOULD ALWAYS HIDE FACE HAIR ETC
        Hat,     // WOULD ALWAYS HAIR
        Hood,            // WOULD ALWAYS HAIR
        HelmetAcessorie,
        FaceCover,
        Torso,
        Back,
        RightShoulder,
        RightUpperArm,
        RightElbow,
        RightLowerArm,
        RightHand,
        LeftShoulder,
        LeftUpperArm,
        LeftElbow,
        LeftLowerArm,
        LeftHand,
        Hips,
        HipsAttachment,
        RightLeg,
        RightKnee,
        LeftLeg,
        LeftKnee
    }

    public enum EquipmentType
    {
        RightWeapon01, // 0
        RightWeapon02, // 1
        RightWeapon03, // 2
        LeftWeapon01,  // 3
        LeftWeapon02,  // 4
        LeftWeapon03,   // 5
        Head,           // 6
        Body,           // 7
        Legs,           // 8
        Hands           // 9
    }

    public enum HeadEquipmentType
    {
        FullHelmet,   // HIDE ENTIRE HEAD + FEATURES
        Hat,          // DOESNT HIDE ANYTHING
        Hood,         // HIDES HAIR
        FaceCover     // HIDES BEARD
    }

    public enum WeaponClass
    {
        Sword,
        Greatsword,
        Spear,
        MediumShield,
        LightShield,
        Fist,
    }

    //  THIS IS USED TO CALCULATE DAMAGE BASED ON ATTACK TYPE
    public enum AttackType
    {
        LightAttack01,
        LightAttack02,
        HeavyAttack01,
        HeavyAttack02,
        ChargedAttack01,
        ChargedAttack02,
        RunningAttack01,
        RollingAttack01,
        BackstepAttack01
    }

    public enum DamageIntensity
    {
        Ping,
        Light,
        Medium,
        Heavy,
        Colossal
    }

    // USED TO DETERMINE ITEM PICUP TYPE
    public enum ItemPickupType
    {
        WorldSpawn,
        EnemyDrop,
        CharacterDrop
    }
}