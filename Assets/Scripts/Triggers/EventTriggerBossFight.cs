using UnityEngine;

namespace ART
{
    public class EventTriggerBossFight : MonoBehaviour
    {
        [SerializeField] private int bossID;

        private void OnTriggerEnter(Collider other)
        {
            AIBossCharacterManager boss = WorldAIManager.instance.GetBossCharacterByID(bossID);

            if (boss != null)
            {
                boss.WakeBoss();
            }
        }
    }
}