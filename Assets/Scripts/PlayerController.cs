using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 input;
    private CharacterController characterController;
    private Vector3 direction;

    [SerializeField] private float speed;

    [SerializeField] private Movement movement;

    [SerializeField] private float rotationSpeed = 500f;
    private Camera mainCamera;

        
    private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float velocity;

  
    [SerializeField] private float jumpPower;
    private int numberOfJumps = 0;
    [SerializeField] private int maxNumberOfJumps = 20;

    public int Health = 20;
   

    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        
    }

    public void Update()
    {
        
        ApplyRotation(); 
        ApplyGravity();
        ApplyMovement();

    }

    private void ApplyGravity()
    {

        if (IsGrounded() && velocity <0)
        {
            velocity = -1.0f;
        }
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }
           
       
        direction.y = velocity;
    }
    private void ApplyRotation()
    {
        if (input.sqrMagnitude == 0) return;

        direction = Quaternion.Euler(0.0f, mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(input.x, 0.0f, input.y);
        var targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    public void ApplyMovement()
    {
        var targetSpeed = movement.isSprinting ? movement.speed * movement.multiplier : movement.speed;
        movement.currentSpeed = Mathf.MoveTowards(movement.currentSpeed, targetSpeed, movement.acceleration * Time.deltaTime);

        characterController.Move(direction * movement.currentSpeed * Time.deltaTime);

    }
    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0.0f, input.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        
        if (!IsGrounded() && numberOfJumps >= maxNumberOfJumps) return;
        if (numberOfJumps == 0) StartCoroutine(WaitForLanding());

        numberOfJumps++;
        velocity = jumpPower;
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        movement.isSprinting = context.started || context.performed;
    }
    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil (()=> !IsGrounded());
        
        yield return new WaitUntil(IsGrounded);
   

        numberOfJumps = 0;
    }

    public void HealthUpdate(int hurt)
    {
        Health += hurt;
        
        Debug.Log(Health);

        if (Health <= 0.0f)
        {
            
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        Destroy(gameObject);
      
    }

    private bool IsGrounded() => characterController.isGrounded;
}
[Serializable]
public struct Movement
{
    [HideInInspector]public bool isSprinting;
    [HideInInspector] public float currentSpeed;
    public float speed;
    public float multiplier;
    public float acceleration;
}