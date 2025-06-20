using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ART
{
    public class AICharacterManager : CharacterManager
    {
        [Header("Character")]
        public string characterName = "";

        public GameObject characterObject;

        [HideInInspector] public AICharacterNetworkManager aiCharacterNetworkManager;
        [HideInInspector] public AICharacterCombatManager aiCharacterCombatManager;
        [HideInInspector] public AICharacterLocomotionManager aiCharacterLocomotionManager;
        [HideInInspector] public AICharacterInventoryManager aiCharacterInventoryManager;

        [Header("Navmesh Agent")]
        public NavMeshAgent navMeshAgent;

        [Header("Current State")]
        [SerializeField] protected AIState currentState;

        [Header("States")]
        public IdleState idle;

        public PursueTargetState pursueTarget;
        public CombatStanceState combatStance;
        public AttackState attack;

        protected override void Awake()
        {
            base.Awake();

            aiCharacterNetworkManager = GetComponent<AICharacterNetworkManager>();
            aiCharacterCombatManager = GetComponent<AICharacterCombatManager>();
            aiCharacterLocomotionManager = GetComponent<AICharacterLocomotionManager>();
            aiCharacterInventoryManager = GetComponent<AICharacterInventoryManager>();

            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsOwner)
            {
                idle = Instantiate(idle);
                pursueTarget = Instantiate(pursueTarget);
                combatStance = Instantiate(combatStance);
                attack = Instantiate(attack);
                currentState = idle;

                if (navMeshAgent != null)
                {
                    navMeshAgent.destination = transform.position;
                }
            }

            aiCharacterNetworkManager.currentHealth.OnValueChanged += aiCharacterNetworkManager.CheckHP;
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            aiCharacterNetworkManager.currentHealth.OnValueChanged -= aiCharacterNetworkManager.CheckHP;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (characterUIManager.hasFloatingHPBar)
            {
                characterNetworkManager.currentHealth.OnValueChanged += characterUIManager.OnHPChanged;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            characterNetworkManager.currentHealth.OnValueChanged -= characterUIManager.OnHPChanged;
        }

        protected override void Update()
        {
            base.Update();

            aiCharacterCombatManager.HandleActionRecovery(this);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (IsOwner)
            {
                ProcessStateMachine();
            }
        }

        private void ProcessStateMachine()
        {
            AIState nextState = currentState?.Tick(this);

            if (nextState != null)
            {
                currentState = nextState;
            }

            //  THE POSITION/ROTATION SHOULD BE RESET ONLY AFTER THE STATE MACHINE HAS PROCESSED IT'S TICK
            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localRotation = Quaternion.identity;

            if (aiCharacterCombatManager.currentTarget != null)
            {
                aiCharacterCombatManager.targetsDirection = aiCharacterCombatManager.currentTarget.transform.position - transform.position;
                aiCharacterCombatManager.viewableAngle = WorldUtilityManager.instance.GetAngleOfTarget(transform, aiCharacterCombatManager.targetsDirection);
                aiCharacterCombatManager.distanceFromTarget = Vector3.Distance(transform.position,
                    aiCharacterCombatManager.currentTarget.transform.position);
            }

            if (navMeshAgent.enabled)
            {
                Vector3 agentDestination = navMeshAgent.destination;
                float remainingDistance = Vector3.Distance(agentDestination, transform.position);

                if (remainingDistance > navMeshAgent.stoppingDistance)
                {
                    aiCharacterNetworkManager.isMoving.Value = true;
                }
                else
                {
                    aiCharacterNetworkManager.isMoving.Value = false;
                }
            }
            else
            {
                aiCharacterNetworkManager.isMoving.Value = false;
            }
        }

        public void DeleteCharacterAfterDeath()
        {
            if (aiCharacterNetworkManager.IsOwner)
            {
                characterObject.SetActive(false);
            }
        }
    }
}