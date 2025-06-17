using System.Collections.Generic;
using UnityEngine;

namespace ART
{
    public class BossStompCollider : DamageCollider
    {
        [SerializeField] private AIKnightfallCharacterManager knightCharacterManager;

        protected override void Awake()
        {
            base.Awake();

            knightCharacterManager = GetComponentInParent<AIKnightfallCharacterManager>();
        }

        public void StompAttack()
        {
            GameObject stompVFX = Instantiate(knightCharacterManager.knightfallCombatManager.durkImpactVFX, transform);
            Collider[] colliders = Physics.OverlapSphere(transform.position, knightCharacterManager.knightfallCombatManager.stompAttackAOERadius, WorldUtilityManager.instance.GetCharacterLayers());
            List<CharacterManager> charactersDamaged = new List<CharacterManager>();

            foreach (var collider in colliders)
            {
                CharacterManager character = collider.GetComponentInParent<CharacterManager>();

                if (character != null)
                {
                    if (charactersDamaged.Contains(character))
                    {
                        continue;
                    }

                    //  WE DON WANT THE DURK THE HURT HIMSELF WHEN HE STOMPS
                    if (character == knightCharacterManager)
                    {
                        continue;
                    }

                    charactersDamaged.Add(character);

                    //  WE ONLY PROCESS DAMAGEIF THE CHARACTER "ISOWNER" SO THAT THEY ONLY GET DAMAGED IF THE COLLIDER CONNECTS ON
                    //  THEIR CLIENT, MEANING IF YOU ARE HIT ON THE HOSTS SCREEN BUT NOT ON YOUR OWN, YOU WILL NOT BE HIT.
                    if (character.IsOwner)
                    {
                        //  CHECK FOR BLOCK
                        TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
                        damageEffect.physicalDamage = knightCharacterManager.knightfallCombatManager.stompDamage;
                        damageEffect.poiseDamage = knightCharacterManager.knightfallCombatManager.stompDamage;

                        character.characterEffectsManager.ProcessInstantEffect(damageEffect);
                    }
                }
            }
        }
    }
}