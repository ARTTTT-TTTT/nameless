using UnityEngine;

namespace ART
{
    public class WeaponItem : EquipmentItem
    {
        [Header("Animations")]
        public AnimatorOverrideController weaponAnimator;

        [Header("Model Instantiation")]
        public WeaponModelType weaponModelType;

        [Header("Weapon Model")]
        public GameObject weaponModel;

        [Header("WeaponClass")]
        public WeaponClass weaponClass;

        [Header("Weapon Requirements")]
        public int strengthREQ = 0;

        public int dexREQ = 0;
        public int intREQ = 0;
        public int faithREQ = 0;

        [Header("Weapon Base Damage")]
        public int physicalDamage = 0;

        public int magicDamage = 0;
        public int fireDamage = 0;
        public int lightningDamage = 0;
        public int holyDamage = 0;

        //  WEAPON GUARD ABSORBTIONS (BLOCKING POWER)

        [Header("Weapon Poise")]
        public float poiseDamage = 10;

        //  OFFENSIVE POISE BONUS WHEN ATTACKING

        [Header("Attack Modifiers")]
        public float light_Attack_01_Modifier = 1.0f;

        public float light_Attack_02_Modifier = 1.2f;
        public float heavy_Attack_01_Modifier = 1.4f;
        public float heavy_Attack_02_Modifier = 1.6f;
        public float charge_Attack_01_Modifier = 2.0f;
        public float charge_Attack_02_Modifier = 2.2f;
        public float running_Attack_01_Modifier = 1.5f;
        public float rolling_Attack_01_Modifier = 1.2f;
        public float backstep_Attack_01_Modifier = 1.2f;

        //  CRITICAL DAMAGE MODIFIER ETC

        [Header("Stamina Costs Modifiers ")]
        public int baseStaminaCost = 20;

        public float lightAttackStaminaCostMultiplier = 0.9f;
        public float heavyAttackStaminaCostMultiplier = 1.3f;
        public float chargedAttackStaminaCostMultiplier = 1.5f;
        public float runningAttackStaminaCostMultiplier = 1.1f;
        public float rollingAttackStaminaCostMultiplier = 1.1f;
        public float backstepAttackStaminaCostMultiplier = 1.1f;

        //  RUNNNG ATTACK STAMINA COST MODIFIER
        //  LIGHT ATTACK STAMINA COST MOD.
        //  HEAVY ATT STAMINA COST MOD. ETC ...

        [Header("Weapon Blocking Absorption")]
        public float physicalBasedDamageAbsorption = 50;

        public float magicBasedDamageAbsorption = 50;
        public float fireBasedDamageAbsorption = 50;
        public float lightningBasedDamageAbsorption = 50;
        public float holyBasedDamageAbsorption = 50;
        public float stability = 50;  //  REDUCES STAMINA LOST FROM BLOCK

        [Header("Actions")]
        public WeaponItemAction oh_RB_Action;  //  ONE HAND RIGHT BUMPER ACT�ON

        public WeaponItemAction oh_RT_Action;  //  ONE HAND RIGHT TRIGGER ACT�ON
        public WeaponItemAction oh_LB_Action;  //  ONE HAND LEFT BUMPER ACT�ON
        public AshOfWar ashOfWarAction;

        [Header("SFX")]
        public AudioClip[] whooshes;

        public AudioClip[] blocking;

        //  ASH OF WAR

        //  BLOCKING SOUNDS
    }
}