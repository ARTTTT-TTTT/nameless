using UnityEngine;

namespace ART
{
    public class AIBossCharacterNetworkManager : AICharacterNetworkManager
    {
        private AIBossCharacterManager aiBossCharacter;
        private bool isPhaseShift = false;

        protected override void Awake()
        {
            base.Awake();

            aiBossCharacter = GetComponent<AIBossCharacterManager>();
        }

        public override void CheckHP(int oldValue, int newValue)
        {
            base.CheckHP(oldValue, newValue);

            if (aiBossCharacter.IsOwner)
            {
                if (isPhaseShift) { return; }
                if (currentHealth.Value <= 0)
                    return;
                float healthNeededForShift = maxHealth.Value * (aiBossCharacter.minimumHealthPercentageToShift / 100);
                if (currentHealth.Value <= healthNeededForShift)
                {
                    isPhaseShift = true;
                    aiBossCharacter.PhaseShift();
                }
            }
        }
    }
}