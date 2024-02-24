using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class MovementController : MonoBehaviour
{
    
    //references 
    private PlayerInput playerInput; 
    private CharacterController characterController;
    private Animator animator;
    
    //animation hashes
    private int isMovingHash = Animator.StringToHash("isMoving");
    
    //gravity
    private float gravity = -9.81f;
    private float groundedGravity = -0.05f;
    [SerializeField] private float gravityModifier = 1.0f;
    
    //internal move variables
    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private bool isMovementPressed;
    [SerializeField] private float moveSpeedMultiplier = 3.0f; 
    private float RotationFactor = 20f;
    
    //state machine 
    private enum State
    {
        Idle, 
        Moving, 
    }

    private State currentState; 
    
    
    //internal jump variables
    private bool isJumpPressed = false;
    private float initialJumpVelocity;
    [SerializeField] private float maxJumpHeight = 1.0f;
    [SerializeField] private float maxJumpTime = 0.5f;
    [SerializeField] private float fallMultiplier = 2.0f;
    private bool isJumping = false; 

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
    }
    private void OnJumpInput(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }
    private void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x * moveSpeedMultiplier;
        currentMovement.z = currentMovementInput.y * moveSpeedMultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void Update()
    {
        HandleRotation();
        handleAnimation();
        

    characterController.Move(currentMovement * Time.deltaTime);
        
        HandleGravity();
    }

    private void MovementUpdate()
    
    private void HandleGravity()
    {
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        
        if (characterController.isGrounded)
        {
            currentMovement.y = groundedGravity;
        }
        else if (isFalling)
        {
            currentMovement.y = CalculateYVelocity(currentMovement.y, fallMultiplier);
        }
        else
        {
            currentMovement.y = CalculateYVelocity(currentMovement.y, 1.0f);
        }
    }

    private float CalculateYVelocity(float currentVelocity, float modifier)
    {
        float newYVelocity = currentVelocity + (gravity * modifier * Time.deltaTime);
        return (currentVelocity + newYVelocity) * 0.5f;
    }
    
    private void HandleRotation()
    {
        Vector3 lookPosition; 
        lookPosition.x = currentMovement.x;
        lookPosition.y = 0;
        lookPosition.z = currentMovement.z;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookPosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationFactor * Time.deltaTime);
        }
    }
    
    private void handleAnimation()
    {
        bool isMoving = animator.GetBool(isMovingHash);

        if (isMovementPressed && !isMoving)
        {
            animator.SetBool(isMovingHash, true);
        } else if (!isMovementPressed && isMoving)
        {
            animator.SetBool(isMovingHash, false);
        }
    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
