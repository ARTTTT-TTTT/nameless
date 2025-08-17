using UnityEngine;

namespace ART
{
    public class AIKnightfallCombatManager : AICharacterCombatManager
    {
        private AIKnightfallCharacterManager knightfallManager;

        [Header("Damage Colliders")]
        [SerializeField] private BossWeaponDamageCollider clubDamageCollider;

        [SerializeField] private BossStompCollider stompCollider;
        public float stompAttackAOERadius = 1.5f;

        [Header("Damage")]
        [SerializeField] private int baseDamage = 25;

        [SerializeField] private int basePoiseDamage = 25;
        [SerializeField] private float attack01DamageModifier = 1.0f;
        [SerializeField] private float attack02DamageModifier = 1.4f;
        [SerializeField] private float attack03DamageModifier = 1.6f;
        public float stompDamage = 25;

        [Header("VFX")]
        public GameObject durkImpactVFX;

        protected override void Awake()
        {
            base.Awake();

            knightfallManager = GetComponent<AIKnightfallCharacterManager>();
        }

        public void SetAttack01Damage()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGruntSoundFX();
            clubDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
            clubDamageCollider.poiseDamage = basePoiseDamage * attack01DamageModifier;
        }

        public void SetAttack02Damage()
        {
            clubDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
            clubDamageCollider.poiseDamage = basePoiseDamage * attack02DamageModifier;
        }

        public void SetAttack03Damage()
        {
            clubDamageCollider.physicalDamage = baseDamage * attack03DamageModifier;
            clubDamageCollider.poiseDamage = basePoiseDamage * attack03DamageModifier;
        }

        public void OpenKnightfallDamageCollider()
        {
            clubDamageCollider.EnableDamageCollider();
            knightfallManager.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(knightfallManager.knightfallSoundFXManager.clubWooshes));
        }

        public void CloseKnightfallDamageCollider()
        {
            clubDamageCollider.DisableDamageCollider();
        }

        public void ActivateDurkStomp()
        {
            stompCollider.StompAttack();
        }

        /*        public override void PivotTowardsTarget(AICharacterManager aiCharacter)
        {
            //  PLAY A PIVOT ANIMATION  DEPENDING ON THE VIEWABLE ANGLE OF THE TARGET
            if (aiCharacter.isPerformingAction)
            {
                return;
            }

            if (viewableAngle >= 61 && viewableAngle <= 110)
            {
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Right_90", true);
            }
            else if (viewableAngle <= -61 && viewableAngle >= -110)
            {
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Left_90", true);
            }
            else if (viewableAngle >= 146 && viewableAngle <= 180)
            {
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Right_180", true);
            }
            else if (viewableAngle <= -146 && viewableAngle >= -180)
            {
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Left_180", true);
            }
        }*/
    }
}