using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    BoxCollider2D collider;
    [SerializeField]
    MovementData movementData;
    [Header("Checks")] 
    [SerializeField] private Transform groundPoint;
    [SerializeField] private Vector2 groundPointSize = new Vector2(0.49f, 0.03f);
    [SerializeField] private Transform forwardWallPoint;
    [SerializeField] private Transform backWallPoint;
    [SerializeField] private Vector2 wallPointSize = new Vector2(0.5f, 1f);
    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    private InputAction move;
    private InputMap map;
    private InputAction jump;

    public bool FacingRight { get; private set; }
    public bool Jumping { get; private set; }
    public bool WallJumping { get; private set; }
    public bool Sliding { get; private set; }
    
    
    
    public float LastGroundTime { get; private set; }
    public float LastWallTime { get; private set; }
    public float LastWallRightTime { get; private set; }
    public float LastWallLeftTime { get; private set; }
    public float LastPressedJumpTime { get; private set; }

    private bool isJumpShort;
    private bool isFalling;
    private float wallJumpStartTime;
    private int lastWallJumpDirection;
    private Vector2 inputDirection;
    private float extGravity;

    public void SetGravityMult(float grav)
    {
        extGravity = grav;
    }
    private void Start()
    {
        extGravity = 1;
        SetGravity(movementData.GravityScale);
    }

    private void Update()
    {
        UpdateTimers();
        GetInput();
        CheckCollision();
        JumpChecks();
        SlideChecks();
        Gravity();
    }

    private void FixedUpdate()
    {
        if (WallJumping)
            Move(movementData.WallJumpMovementReduction);
        else
            Move(1);

        if (Sliding)
            Slide();
    }

    private void Gravity()
    {
        //if (Sliding)
        //{
        //    SetGravity(0f);
        //} 
        if (rb.velocity.y < 0 && inputDirection.y < 0)// higher grav if pressing down
        {
            SetGravity(movementData.GravityScale * movementData.FastFallGravityScale);
            
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -movementData.FastFallTerminalSpeed));
        }
        else if (isJumpShort)
        {
            SetGravity(movementData.GravityScale * movementData.ShortJumpGravityScale);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -movementData.TerminalSpeed));
        }
        else if (Jumping || WallJumping || isFalling && Mathf.Abs(rb.velocity.y) < movementData.JumpHangThreshold)
        {
            SetGravity(movementData.GravityScale * movementData.JumpHangGravityScale);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -movementData.TerminalSpeed));
        }
        else if (rb.velocity.y < 0)
        {
            SetGravity(movementData.GravityScale * movementData.FallingGravityScalar);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -movementData.TerminalSpeed));
        }
        else
        {
            SetGravity(movementData.GravityScale);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -movementData.TerminalSpeed));
        }
    }

    private void SlideChecks()
    {
        if (CanSlide() && ((LastWallLeftTime > 0 && inputDirection.x < 0) ||
                           (LastWallRightTime > 0 && inputDirection.x > 0)))
            Sliding = true;
        else
            Sliding = false;
    }

    private void JumpChecks()
    {
        if (Jumping && rb.velocity.y < 0)
        {
            Jumping = false;
            
            if(!WallJumping)
                isFalling = true;
        }

        if (WallJumping && Time.time - wallJumpStartTime > movementData.WallJumpMovementReductionTime)
        {
            WallJumping = false;
        }

        if (LastGroundTime > 0 && !Jumping && !WallJumping)
        {
            isJumpShort = false;
            if (!Jumping) 
                isFalling = false;
        }
        
        if (CanJump() && LastPressedJumpTime > 0)
        {
            Jumping = true;
            WallJumping = false;
            isJumpShort = false;
            isFalling = false;
            Jump();
        }
        else if (CanWallJump() && LastPressedJumpTime > 0)
        {
            WallJumping = true;
            Jumping = false;
            isJumpShort = false;
            isFalling = false;
            wallJumpStartTime = Time.time;
            lastWallJumpDirection = LastWallRightTime > 0 ? 1 : -1;
            WallJump(lastWallJumpDirection);
        }
    }

    private void GetInput()
    {
        inputDirection = move.ReadValue<Vector2>();
        if (inputDirection.x != 0)
        {
            CheckDirection(inputDirection.x);
        }
    }

    private void CheckCollision()
    {
        if (!Jumping)
        {
            // Ground
            if (Physics2D.OverlapBox(groundPoint.position, groundPointSize, 0, groundLayer))
            {
                LastGroundTime = movementData.CoyoteTime;
            }
            //Right 
            if (((Physics2D.OverlapBox(forwardWallPoint.position, wallPointSize, 0, groundLayer) && FacingRight)
                 || (Physics2D.OverlapBox(backWallPoint.position, wallPointSize, 0, groundLayer) && !FacingRight)) && !WallJumping)
                LastWallRightTime = movementData.CoyoteTime;

            // Left
            if (((Physics2D.OverlapBox(forwardWallPoint.position, wallPointSize, 0, groundLayer) && !FacingRight)
                 || (Physics2D.OverlapBox(backWallPoint.position, wallPointSize, 0, groundLayer) && FacingRight)) && !WallJumping)
                LastWallLeftTime = movementData.CoyoteTime;

            LastWallTime = Mathf.Max(LastWallLeftTime, LastWallRightTime);
        }
    }

    private void CheckDirection(float inputDirectionX)
    {
        if (inputDirectionX < 0 != FacingRight)
            Turn();
    }

    private bool CanJump()
    {
        return LastGroundTime > 0 && !Jumping;
    }

    private bool CanWallJump()
    {
        return LastPressedJumpTime > 0 && LastWallTime > 0 && LastGroundTime <= 0 && (!WallJumping ||
            (LastWallRightTime > 0 && lastWallJumpDirection == 1) ||
            (LastWallLeftTime > 0 && lastWallJumpDirection == -1));
    }

    private bool CanShortJump()
    {
        return Jumping && rb.velocity.y > 0;
    }

    private bool CanShortWallJump()
    {
        return WallJumping && rb.velocity.y > 0;
    }

    private bool CanSlide()
    {
        return LastWallTime > 0 && !Jumping && !WallJumping && LastGroundTime <= 0;
    }
    private void UpdateTimers()
    {
        LastGroundTime -= Time.deltaTime;
        LastWallTime -= Time.deltaTime;
        LastWallRightTime -= Time.deltaTime;
        LastWallLeftTime -= Time.deltaTime;
        LastPressedJumpTime -= Time.deltaTime;
        
    }


    private void SetGravity(float gravity)
    {
        rb.gravityScale = gravity * extGravity;
    }
    
    
    
    private void OnJumpUp(InputAction.CallbackContext obj)
    {
        if (CanShortJump() || CanShortWallJump())
        {
            isJumpShort = true;
        }
    }

    private void OnJumpDown(InputAction.CallbackContext obj)
    {
        LastPressedJumpTime = movementData.JumpInputBufferTime;
    }


    private void Move(float speedFactor)
    {
        float targetSpeed  = inputDirection.x * movementData.MaxHorizontalSpeed;
        
        targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, speedFactor);
        
        float accelRate = (LastGroundTime > 0)
            ? ((Mathf.Abs(targetSpeed) > 0.01f) ? movementData.MoveAccelerationAmount : movementData.MoveDecelerationAmount)
            : ((Mathf.Abs(targetSpeed) > 0.01f)
                ? movementData.MoveAccelerationAmount * movementData.AirAcceleration
                : movementData.MoveDecelerationAmount * movementData.AirDeceleration);
        if (Jumping || WallJumping || isFalling && Mathf.Abs(rb.velocity.y) < movementData.JumpHangThreshold)
        {
            accelRate *= movementData.JumpHangAccelerationScalar;
            targetSpeed *= movementData.JumpHangSpeedScalar;
        }
        // aproximately because resharper has the brains of a 2 year old with a quantum physics degreee
        if(Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastGroundTime < 0)
            accelRate = 0;
        float speedDelta = targetSpeed - rb.velocity.x;
        float movement = speedDelta * accelRate;
        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    private void Turn()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        FacingRight = !FacingRight;
    }

    private void Jump()
    {
        LastPressedJumpTime = 0;
        LastGroundTime = 0;
        rb.AddForce((movementData.JumpForce - (rb.velocity.y < 0 ? rb.velocity.y : 0)) * Vector2.up, ForceMode2D.Impulse);
    }
    private void WallJump(int dir)
    {
        LastPressedJumpTime = 0;
        LastGroundTime = 0;
        LastWallRightTime = 0;
        LastWallLeftTime = 0;

        Vector2 force = new Vector2(movementData.WallJumpForce.x, movementData.WallJumpForce.y);
        force.x *= dir; 
    
        if (!Mathf.Approximately(Mathf.Sign(rb.velocity.x), Mathf.Sign(force.x))) // ty resharper
            force.x -= rb.velocity.x;

        if (rb.velocity.y < 0)
            force.y -= rb.velocity.y;

        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void Slide()
    {
        float speedDelta = movementData.SlideSpeed - rb.velocity.y;
        float movement = speedDelta * movementData.SlideAcceleration;
        movement = Mathf.Clamp(movement, -Mathf.Abs(speedDelta)  * (1 / Time.fixedDeltaTime), Mathf.Abs(speedDelta) * (1 / Time.fixedDeltaTime));
        rb.AddForce(movement * Vector2.up);
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    private void Awake()
    {
        map = new InputMap(); 
        jump = map.Player.Jump;
        move = map.Player.Move;
    }

    private void OnEnable()
    {

        EnableInput();
    }

    private void OnDisable()
    {
        DisableInput();
    }

    public void EnableInput()
    {
        move.Enable();
        jump.Enable();
        jump.started += OnJumpDown;
        jump.canceled += OnJumpUp;
    }

    public void DisableInput()
    {
        move.Disable();
        jump.Disable();
    }

}
