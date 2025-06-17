using UnityEngine;

namespace ART
{
    public class AIMobCombatManager : AICharacterCombatManager
    {
        [Header("Damage Colliders")]
        [SerializeField] private MobWeaponDamageCollider rightWeaponDamageCollider;

        // [SerializeField] private MobWeaponDamageCollider leftWeaponDamageCollider;

        [Header("Damage")]
        [SerializeField] private int baseDamage = 25;

        [SerializeField] private int basePoiseDamage = 25;
        [SerializeField] private float attack01DamageModifier = 1.0f;
        [SerializeField] private float attack02DamageModifier = 1.4f;

        public void SetAttack01Damage()
        {
            rightWeaponDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
            // leftWeaponDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;

            rightWeaponDamageCollider.poiseDamage = basePoiseDamage * attack01DamageModifier;
            // leftWeaponDamageCollider.poiseDamage = basePoiseDamage * attack01DamageModifier;
        }

        public void SetAttack02Damage()
        {
            rightWeaponDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
            // leftWeaponDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;

            rightWeaponDamageCollider.poiseDamage = basePoiseDamage * attack02DamageModifier;
            // leftWeaponDamageCollider.poiseDamage = basePoiseDamage * attack02DamageModifier;
        }

        public void OpenRightWeaponDamageCollider()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGruntSoundFX();
            rightWeaponDamageCollider.EnableDamageCollider();
        }

        public void CloseRightWeaponDamageCollider()
        {
            rightWeaponDamageCollider.DisableDamageCollider();
        }

        public void OpenLeftWeaponDamageCollider()
        {
            // aiCharacter.characterSoundFXManager.PlayAttackGrunt();
            // leftWeaponDamageCollider.EnableDamageCollider();
        }

        public void CloseLeftWeaponDamageCollider()
        {
            // leftWeaponDamageCollider.DisableDamageCollider();
        }

        public override void CloseAllDamageColliders()
        {
            base.CloseAllDamageColliders();

            rightWeaponDamageCollider.DisableDamageCollider();
            // leftWeaponDamageCollider.DisableDamageCollider();
        }
    }
}