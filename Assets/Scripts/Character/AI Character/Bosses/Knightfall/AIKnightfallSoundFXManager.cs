using UnityEngine;

namespace ART
{
    public class AIKnightfallSoundFXManager : CharacterSoundFXManager
    {
        [Header("Sword Wooshes")]
        public AudioClip[] clubWooshes;

        [Header("Sword Impacts")]
        public AudioClip[] clubImpacts;

        [Header("Stomp Impacts")]
        public AudioClip[] stompImpacts;

        public virtual void PlayClubImpactSoundFX()
        {
            if (clubImpacts.Length > 0)
            {
                PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(clubImpacts));
            }
        }

        public virtual void PlayStompImpactSoundFX()
        {
            if (stompImpacts.Length > 0)
            {
                PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(stompImpacts));
            }
        }
    }
}