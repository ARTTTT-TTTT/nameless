using UnityEngine;

namespace ART
{
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Blocked Damage")]
    public class TakeBlockedDamageEffect : InstantCharacterEffect
    {
        [Header("Character Causing Damage")]
        public CharacterManager characterCausingDamage; // If the damage is caused by another character's attack it will be stored here

        [Header("Damage")]
        public float physicalDamage = 0;  // (In the future will be split into "Standart", "Strike", "Slahs", and "Pierce")

        public float magicDamage = 0;
        public float fireDamage = 0;
        public float lightningDamage = 0;
        public float holyDamage = 0;

        [Header("Final Damage")]
        private int finalDamageDealt = 0;                 //  The damage the character takes after ALL calculations have been made

        [Header("Poise")]
        public float poiseDamage = 0;

        public bool poiseIsBroken = false;                 //  If a character's Poise is Broken, They will be "Stunned" and play a damage animation

        [Header("Stamina")]
        public float staminaDamage = 0;

        public float finalStaminaDamage = 0;

        // TO DO > BUILD UPS
        // build up effect amounts

        [Header("Animation")]
        public bool playDamageAnimation = true;

        public bool manuallySelectDamageAnimation = false;
        public string damageAnimation;

        [Header("Sound FX")]
        public bool willPlayDamageSFX = true;

        public AudioClip elementalDamageSoundFX;          //  USED ON TOP OF REGULAR SFX IF THERE IS ELEMENTAL DAMAGE PRESENT (Magic/Fire/Lightning/Holy)

        [Header("Direction Damage Taken From")]
        public float angleHitFrom;                        //  USED TO DETERMINE WHAT DAMAGE ANIMATION TO PLAY (Move backwards, to the left, to the right etc) (i.e. Light_hit_taken_left_01.anim)

        public Vector3 contactPoint;                      //  USED TO DETERMINE WHERE THE BLOOD FX INSTANTIATE

        public override void ProcessEffect(CharacterManager character)
        {
            if (character.characterNetworkManager.isInvulnerable.Value)
            {
                return;
            }

            Debug.Log("HIT WAS BLOCKED!");

            //  IF THE CHARACTER IS DEAD, NO ADDITIONAL DAMAGE EFFECTS SHOULD BE PROCESSED
            if (character.isDead.Value)
                return;

            CalculateDamage(character);
            CalculateStaminaDamage(character);
            PlayDirectionalBasedBlockingAnimation(character);
            Debug.Log("TAKE DAMAGE CS >>>   PROCESSED DIRECTIONAL ANIMATION");
            //  CHECK FOR BUILD UPS (POISON, BLEED ETC...)
            PlayDamageSFX(character);
            PlayDamageVFX(character);

            //  IF CHARACTER IS A.I., CHECK FOR NEW TARGET IF CHARACTER CAUSING DAMAGE IS PRESENT
            CheckForGuardBreak(character);
        }

        private void CalculateDamage(CharacterManager character)
        {
            if (!character.IsOwner)
            {
                return;
            }

            if (characterCausingDamage != null)
            {
                // CHECK FOR DAMAGE MODIFIERS AND MODIFY BASE DAMAGE (Physical damage buff, elemental damage buff etc.)
            }

            //  CHECK CHARACTER FOR FLAT DEFENSES AND SUBSTRACT THEM FROM THE DAMAGE

            //  CHECK CHARACTER FOR ARMOR ABSORBTIONS, AND SUBSTRACT THE PERCENTAGE FROM THE DAMAGE

            //  AND ALL DAMAGE TYPES TOGETHER, AND APPLY FINAL DAMAGE

            Debug.Log("original physical damage: " + physicalDamage);

            physicalDamage -= (physicalDamage * (character.characterStatsManager.blockingPhysicalAbsorption / 100));
            magicDamage -= (magicDamage * (character.characterStatsManager.blockingMagicAbsorption / 100));
            fireDamage -= (fireDamage * (character.characterStatsManager.blockingFireAbsorption / 100));
            lightningDamage -= (lightningDamage * (character.characterStatsManager.blockingLightningAbsorption / 100));
            holyDamage -= (holyDamage * (character.characterStatsManager.blockingHolyAbsorption / 100));

            finalDamageDealt = Mathf.RoundToInt(physicalDamage + magicDamage + fireDamage + lightningDamage + holyDamage);

            if (finalDamageDealt <= 0)
            {
                finalDamageDealt = 1;
            }

            character.characterNetworkManager.currentHealth.Value -= finalDamageDealt;
            Debug.Log("final physical damage taken: " + finalDamageDealt);

            //  CALCULATE THE POISE DAMAGE TO DETERMINE IF THE CHARACTER WILL BE STUNNED
        }

        private void CalculateStaminaDamage(CharacterManager character)
        {
            if (!character.IsOwner)
            {
                return;
            }
            finalStaminaDamage = staminaDamage;

            float staminaDamageAbsorption = finalStaminaDamage * (character.characterStatsManager.blockingStability / 100);
            float staminaDamageAfterAbsorption = finalStaminaDamage - staminaDamageAbsorption;

            character.characterNetworkManager.currentStamina.Value -= staminaDamageAfterAbsorption;
        }

        private void CheckForGuardBreak(CharacterManager character)
        {
            // PLAY SFX

            if (!character.IsOwner)
            {
                return;
            }

            if (character.characterNetworkManager.currentStamina.Value <= 0)
            {
                character.characterAnimatorManager.PlayTargetActionAnimation("Guard_Break_01", true);
                character.characterNetworkManager.isBlocking.Value = false;
            }
        }

        private void PlayDamageVFX(CharacterManager character)
        {
            //  IF WE HAVE FIRE DAMAGE PLAY FIRE VFX, OR LIGHTNING OR ETC ...

            //character.characterEffectsManager.PlayBloodSplatterVFX(contactPoint);

            // IF VFX BASED ON BLOCKING WEAPON
        }

        private void PlayDamageSFX(CharacterManager character)
        {
            //AudioClip phsicalDamageSFX = WorldSoundFXManager.instance.ChooseRandomSFXFromArray(WorldSoundFXManager.instance.physicalDamageSFX);

            //character.characterSoundFXManager.PlaySoundFX(phsicalDamageSFX);
            //character.characterSoundFXManager.PlayDamageGruntSounFX();

            //  IF FIRE DAMAGE IS GREATER THAN 0, PLAY BURN SFX,
            //  "  LIGHTNING    "            "  , "     ZAP SFX

            //  GET SFX BASED ON BLOCKING WEAPON

            character.characterSoundFXManager.PlayBlockSoundFX();
        }

        private void PlayDirectionalBasedBlockingAnimation(CharacterManager character)
        {
            if (!character.IsOwner)
            {
                Debug.Log("TAKE DAMAGE CS >>>   NOT OWNER RETURNED");
                return;
            }

            if (character.isDead.Value)
            {
                return;
            }

            DamageIntensity damageIntensity = WorldUtilityManager.instance.GetDamageIntensityBasedOnPoiseDamage(poiseDamage);
            // PLAY A PROPER ANIMATION TO MATCH THE "INTENSITY" OF THE BLOW

            //  TO DO: CHECK FOR TWO HAND STATUS, IF TWO HANDING USE TWO HAND VERSIO OF BLOCK ANIMATION INSTEAD
            switch (damageIntensity)
            {
                case DamageIntensity.Ping:
                    damageAnimation = "Block_Ping_01";
                    break;

                case DamageIntensity.Light:
                    damageAnimation = "Block_Light_01";
                    break;

                case DamageIntensity.Medium:
                    damageAnimation = "Block_Medium_01";
                    break;

                case DamageIntensity.Heavy:
                    damageAnimation = "Block_Heavy_01";
                    break;

                case DamageIntensity.Colossal:
                    damageAnimation = "Block_Colossal_01";
                    break;
            }

            // Debug.Log("TAKE DAMAGE.cs:  >>>  sent play anim req");
            character.characterAnimatorManager.lastDamageAnimationPlayed = damageAnimation;
            character.characterAnimatorManager.PlayTargetActionAnimation(damageAnimation, true);
        }
    }
}