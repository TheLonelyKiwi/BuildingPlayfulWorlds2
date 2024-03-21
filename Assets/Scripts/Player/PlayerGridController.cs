using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGridController : MonoBehaviour
{
    //references
    private PlayerInput playerInput;
    [SerializeField] private Transform movePoint; 

    private float gravity = -9.81f;
    private float groundedGravity = 0.05f;
    [SerializeField] private float tileSize = 1f;
    [SerializeField] private float moveSpeed = 5f;
    
    //internal variables
    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private float distanceToPoint;

    [SerializeField] private bool canMove; 
    private bool isMovementPressed;
    private float rotationFactor = 20f; 
    private void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
    }

    private void Start()
    {
        movePoint.parent = null;
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        //performs input if player completed jump to next square. 
        //and only registers movement if key or gamepad has been fully pressed. 

        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.z = currentMovementInput.x;
        currentMovement.x = currentMovementInput.y;

        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void Update()
    {
        HandleRotation();
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        
        canMove = CheckDistance(transform.position, movePoint.position, .05f);

        if (canMove && isMovementPressed)
        {
            UpdatePointer();
        }
    }

    private void UpdatePointer()
    {
        Vector3 newPos; 
        if (Mathf.Abs(currentMovement.x) == 1f)
        {
            newPos = movePoint.position + new Vector3(currentMovement.x * tileSize, 0f, 0f);
            if (CheckNewPointerPos(newPos))
            {
                movePoint.position = newPos; 
            }
        } else if (Mathf.Abs(currentMovement.z) == 1f)
        {
            newPos = movePoint.position + new Vector3(0f, 0f, currentMovement.z * -1 * tileSize); //flips A-D around due to camera angle
            if (CheckNewPointerPos(newPos))
            {
                movePoint.position = newPos;
            }
        }
    }

    private bool CheckNewPointerPos(Vector3 pos)
    {
        RaycastHit hit;
        if (Physics.Raycast(pos, -Vector3.up, out hit))
        {
            Debug.Log(hit.transform.name);
            switch (hit.transform.gameObject.layer) //have to find better solution.
            {
                //ground
                case 3:
                    return true; 
                default:
                    return false;
            }
        }

        return false; 
    }

    private bool CheckDistance(Vector3 p1, Vector3 p2, float closeDist)
    {
        Vector3 offset = p2 - p1;
        float dist = offset.sqrMagnitude;

        if (dist < closeDist * closeDist)
        {
            return true;
        }
        return false;
    }
    private void HandleRotation()
    {
        Vector3 lookPos;
        lookPos.x = currentMovement.x;
        lookPos.y = 0;
        lookPos.z = -1 * currentMovement.z;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationFactor * Time.deltaTime);
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
