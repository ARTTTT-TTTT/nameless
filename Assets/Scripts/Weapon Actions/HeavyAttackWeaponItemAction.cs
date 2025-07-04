using UnityEngine;

namespace ART
{
    [CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Heavy Attack Action")]
    public class HeavyAttackWeaponItemAction : WeaponItemAction
    {
        [SerializeField] private string heavy_Attack_01 = "Main_Heavy_Attack_01";  // Main = main hand
        [SerializeField] private string heavy_Attack_02 = "Main_Heavy_Attack_02";  // Main = main hand
        [SerializeField] private string th_heavy_Attack_01 = "TH_Heavy_Attack_01";  // TH = Two Hand
        [SerializeField] private string th_heavy_Attack_02 = "TH_Heavy_Attack_02";  // TH = Two Hand

        public override void AttempToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            base.AttempToPerformAction(playerPerformingAction, weaponPerformingAction);

            if (!playerPerformingAction.IsOwner)
            {
                return;
            }

            if (playerPerformingAction.playerNetworkManager.currentStamina.Value <= 0)
            {
                return;
            }

            if (!playerPerformingAction.characterLocomotionManager.isGrounded)
            {
                return;
            }

            if (playerPerformingAction.IsOwner)
            {
                playerPerformingAction.playerNetworkManager.isAttacking.Value = true;
            }

            PerformHeavyAttack(playerPerformingAction, weaponPerformingAction);
        }

        private void PerformHeavyAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            if (playerPerformingAction.playerNetworkManager.isTwoHandingWeapon.Value)
            {
                PerformTwoHandHeavyAttack(playerPerformingAction, weaponPerformingAction);
            }
            else
            {
                PerformMainHandHeavyAttack(playerPerformingAction, weaponPerformingAction);
            }
        }

        private void PerformMainHandHeavyAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            // IF WE ARE ATTACKING CURRENTLY, AND WE ARE ABLE TO PERFORM A COMBO, PERFORM THE COMBO ATTACK
            if (playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon && playerPerformingAction.isPerformingAction)
            {
                playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon = false;

                //  PERFORM A ATTACK BASED ON THE PREVIOUS ATTACK WE JUST PLAYED
                if (playerPerformingAction.characterCombatManager.lastAttackAnimationPerformed == heavy_Attack_01)
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack02, heavy_Attack_02, true);
                }
                else
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack01, heavy_Attack_01, true);
                }
            }
            //  OTHERWISE, IF WE ARE NOT ALREADY ATTACKING JUST PERFORM A REGULAR ATTACK
            else if (!playerPerformingAction.isPerformingAction)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack01, heavy_Attack_01, true);
            }
        }

        private void PerformTwoHandHeavyAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            // IF WE ARE ATTACKING CURRENTLY, AND WE ARE ABLE TO PERFORM A COMBO, PERFORM THE COMBO ATTACK
            if (playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon && playerPerformingAction.isPerformingAction)
            {
                playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon = false;

                //  PERFORM A ATTACK BASED ON THE PREVIOUS ATTACK WE JUST PLAYED
                if (playerPerformingAction.characterCombatManager.lastAttackAnimationPerformed == th_heavy_Attack_01)
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack02, th_heavy_Attack_02, true);
                }
                else
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack01, th_heavy_Attack_01, true);
                }
            }
            //  OTHERWISE, IF WE ARE NOT ALREADY ATTACKING JUST PERFORM A REGULAR ATTACK
            else if (!playerPerformingAction.isPerformingAction)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack01, th_heavy_Attack_01, true);
            }
        }
    }
}