using UnityEngine;

namespace ART
{
    public class AIKnightfallCharacterManager : AIBossCharacterManager
    {
        // HWY GIVE DURK HIS OWN CHARACTER MANAGER?
        // OUR CHARACTER MANAGERS ACT AS A HUB TO WHERE WE CAN REFERENCE ALL COMPONENTS OF A CHARACTER
        // A "PLAYER" MANAGER FOR EXAMPLE WILL HAVE ALL THE COMPONENTS OF A PLAYER CHARACTER
        // AN UNDEAD WILL HAVE ALL THE UNIQUE COMPONENTS OF AN UNDEAD
        // SINCE DURK HAS HIS OWN SFX (CLUB, STOMP) THAT ARE UNIQUE TO HIS CHARACTER ONLY, WE CREATED A DURK SFX MANAGER
        // AND TO REFERENCE THIS NEW SFX MANAGER, AND TO KEEP OUR DESIGN PATTERN THE SAME, WE NEED A DURK CHARACTER MANAGER TO REFERENCE IT FROM

        [HideInInspector] public AIKnightfallSoundFXManager knightfallSoundFXManager;
        [HideInInspector] public AIKnightfallCombatManager knightfallCombatManager;

        protected override void Awake()
        {
            base.Awake();

            knightfallSoundFXManager = GetComponent<AIKnightfallSoundFXManager>();
            knightfallCombatManager = GetComponent<AIKnightfallCombatManager>();
        }
    }
}