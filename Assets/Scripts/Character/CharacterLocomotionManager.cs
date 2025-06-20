﻿using UnityEngine;

namespace ART

{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        private CharacterManager character;

        [Header("Ground Check & Jumping")]
        [SerializeField] protected float gravityForce = -5.55f;

        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckSphereRadius = 0.3f;
        [SerializeField] protected Vector3 yVelocity;   // THE FORCE AT WHICH OUR CHARACTER IS PULLED UP OR DOWN (Jumping or Falling)
        [SerializeField] protected float groundedYVelocity = -20;   // THE FORCE AT WHICH OUR CHARACTER IS STICKING TO GROUND WHILST THEY ARE GROUNDED
        [SerializeField] protected float fallStartYVelocity = -5;   // THE FORCE AT WHICH OUR CHARACTER BEGINS TO FALL WHEN THEY BECOME UNGROUNDED (RISES AS THEY FALL LONGER)
        protected bool fallingVelocityHasBeenSet = false;
        protected float inAirTimer = 0;

        [Header("Flags")]
        public bool isRolling = false;

        public bool canRotate = true;
        public bool canMove = true;
        public bool isGrounded = true;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public void Start()
        {
        }

        protected virtual void Update()
        {
            HandleGroundCheck();

            if (character.characterLocomotionManager.isGrounded)
            {
                // IF WE ARE NOT ATEMPTING TO JUMP OR MOVE UPWARD
                if (yVelocity.y < 0)
                {
                    inAirTimer = 0;
                    character.animator.SetFloat("inAirTimer", inAirTimer);
                    fallingVelocityHasBeenSet = false;
                    yVelocity.y = groundedYVelocity;
                }
            }
            else
            {
                // IF WE ARE NOT JUMPING AND OUR FALLING VELOCITY HAS NOT BEEN SET
                if (!character.characterNetworkManager.isJumping.Value && !fallingVelocityHasBeenSet)
                {
                    fallingVelocityHasBeenSet = true;
                    yVelocity.y = fallStartYVelocity;
                }

                inAirTimer = inAirTimer + Time.deltaTime;
                character.animator.SetFloat("inAirTimer", inAirTimer);
                yVelocity.y += gravityForce * Time.deltaTime;
            }

            // THERE SHOULD ALWAYS BE SOME FORCE APPLIED TO THE Y VELOCITY
            character.characterController.Move(yVelocity * Time.deltaTime);
        }

        protected void HandleGroundCheck()
        {
            character.characterLocomotionManager.isGrounded = Physics.CheckSphere(character.transform.position, groundCheckSphereRadius, groundLayer);
        }

        // DRAWS OUR GROUND CHECK SPHERE IN SCENE VIEW
        protected void OnDrawGizmosSelected()
        {
            // Gizmos.DrawSphere(character.transform.position, groundCheckSphereRadius);
        }

        public void EnableCanRotate()
        {
            canRotate = true;
        }

        public void DisableCanRotate()
        {
            canRotate = false;
        }
    }
}