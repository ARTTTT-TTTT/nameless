using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Linq;
using System.Globalization;

namespace ART
{
    public class WorldAIManager : MonoBehaviour
    {
        public static WorldAIManager instance;

        [Header("DEBUG")]
        [SerializeField] private bool despawnCharacters = false;

        [Header("Characters")]
        [SerializeField] private List<AICharacterSpawner> aiCharacterSpawners;

        [SerializeField] private List<AICharacterManager> spawnedInCharacters;

        [Header("Bosses")]
        [SerializeField] private List<AIBossCharacterManager> spawnedInBosses;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            DebugMenu();
        }

        public void SpawnCharacter(AICharacterSpawner aiCharacterSpawner)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                aiCharacterSpawners.Add(aiCharacterSpawner);
                aiCharacterSpawner.AttemptToSpawnCharacter();
            }
        }

        public void AddCharacterToSpawnedCharactersList(AICharacterManager character)
        {
            if (spawnedInCharacters.Contains(character))
            {
                return;
            }

            spawnedInCharacters.Add(character);

            AIBossCharacterManager bossCharacter = character as AIBossCharacterManager;

            if (bossCharacter != null)
            {
               if (spawnedInBosses.Contains(bossCharacter))
               {
                   return;
               }

               spawnedInBosses.Add(bossCharacter);
            }
        }

        public AIBossCharacterManager GetBossCharacterByID(int ID)
        {
            return spawnedInBosses.FirstOrDefault(boss => boss.bossID == ID);
        }

        public void ResetAllCharacters()
        {
            DespawnAllCharacters();

            foreach (var spawner in aiCharacterSpawners)
            {
                spawner.AttemptToSpawnCharacter();
            }
        }

        private void DespawnAllCharacters()
        {
            foreach (var character in spawnedInCharacters)
            {
                character.GetComponent<NetworkObject>().Despawn();
            }
            spawnedInCharacters.Clear();
        }

        private void DebugMenu()
        {
            if (despawnCharacters)
            {
                despawnCharacters = false;
                ResetAllCharacters();
            }
        }

        private void DisableAllCharacters()
        {
            //  TO DO DISABLE CHARACTER GAMEOBJECTS, SYNC DISABLED STATUS ON NETWORK
            //  DISABLE GAMEOBJECTS FOR CLIENTS UPON CONNECTING, IF DISABLED STATUS IS TRUE
            //  CAN BE USED TO DISABLE CHARACTERS THAT ARE FAR FROM PLAYER TO SAVE MEMORY
            //  CHARACTERS CAN BE SPLIT INTO AREAS (AREA_00, AREA_01, AREA_02 ETC ...)
        }
    }
}