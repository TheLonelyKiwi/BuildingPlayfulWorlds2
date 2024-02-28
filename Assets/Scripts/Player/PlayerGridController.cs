using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGridController : MonoBehaviour
{
    //references
    private PlayerInput playerInput;
    private CharacterController characterController;
    [SerializeField] private Transform movePoint; 

    private float gravity = -9.81f;
    private float groundedGravity = -0.05f;
    [SerializeField] private float moveSpeed = 5f;
    
    //internal variables
    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private bool isMovementPresed;
    private float RotationFactor = 20f; 

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        if (Mathf.Abs(currentMovementInput.x) == 1f)
        {
            currentMovement.x = currentMovementInput.x;
        }

        if (Mathf.Abs(currentMovementInput.y) == 1f)
        {
            currentMovement.z = currentMovementInput.y;
        }
        
        isMovementPresed = currentMovementInput.x != 0 || currentMovementInput.y != 0; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
