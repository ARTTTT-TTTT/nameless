using UnityEngine;

namespace ART
{
    [CreateAssetMenu(menuName = "A.I/Actions/Attack")]
    public class AICharacterAttackAction : ScriptableObject
    {
        [Header("Attack")]
        [SerializeField] private string attackAnimation;

        [SerializeField] private bool isParryable = true;

        [Header("Combo Action")]
        public AICharacterAttackAction comboAction;  //  The combo action of this attack action

        [Header("Action Values")]
        [SerializeField] private AttackType attackType;

        public int attackWeight;

        //  ATTACK CAN BE REPEATED
        public float actionRecoveryTime = 1.5f;    // The time before the character can make another attack after performing this one

        public float minimumAttackAngle = -35;
        public float maximumAttackAngle = 35;
        public float minimumAttackDistance = 0;
        public float maximumAttackDistance = 10;

        public void AttemptToPerformAction(AICharacterManager aiCharacter)
        {
            //aiCharacter.characterAnimatorManager.PlayTargetAttackActionAnimation(attackType, attackAnimation, true);

            aiCharacter.characterAnimatorManager.PlayTargetActionAnimation(attackAnimation, true);
            aiCharacter.aiCharacterNetworkManager.isParryable.Value = true;
        }
    }
}