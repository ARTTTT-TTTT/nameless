using UnityEngine;

namespace ART
{
    public class ArmorItem : EquipmentItem
    {
        [Header("Equipment Absorption Bonus")]
        public float physicalDamageAbsorpion;

        public float magicDamageAbsorpion;
        public float fireDamageAbsorpion;
        public float lightningDamageAbsorpion;
        public float holyDamageAbsorpion;

        [Header("Equipment Resistance Bonus")]
        public float immunity;        // RESISTANCE TO ROT AND POISON

        public float robutsness;     // RESISTANCE TO BLEED AND FROST
        public float focus;         // RESISTANCE TO MADNESS AND SLEEP
        public float vitality;      // RESISTANCE TO DETH CURSE

        [Header("Poise")]
        public float poise;

        [Header("ARMOR MODELS")]
        public EquipmentModel[] equipmentModels;
    }
}