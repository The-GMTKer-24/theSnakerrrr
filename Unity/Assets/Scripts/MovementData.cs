using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MovementData", menuName = "MovementData", order = 1)]
public class MovementData : ScriptableObject
{
    [Header("Gravity")] 
    [SerializeField] private float fallingGravityScalar;
    
    [SerializeField] private float terminalSpeed;
    [SerializeField] private float fastFallGravityScale;
    [SerializeField] private float fastFallTerminalSpeed;
    [Space(10)]
    [Header("Movement")] [SerializeField] private float maxHorizontalSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float airAcceleration;
    [SerializeField] private float airDeceleration;
    [Header("Jump")] [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpDuration;
    [SerializeField] private float shortJumpGravityScale;
    [SerializeField] private float jumpHangGravityScale;
    [SerializeField] private float jumpHangThreshold;
    [SerializeField] private float jumpHangAccelerationScalar;
    [SerializeField] private float jumpHangSpeedScalar;
    
    [Header("Wall Jump")]
    [SerializeField] private Vector2 wallJumpForce; 
    [Space(5)]
    [SerializeField] [Range(0f,1f)] private float wallJumpMovementReduction;
    [SerializeField] private float wallJumpMovementReductionTime;
    [SerializeField] bool doTurnOnWallJump;
    
    [Header("Sliding")]
    [SerializeField] float slideSpeed;
    [SerializeField] float slideAcceleration;
    
    [Header("Jayden we can have coyote time are you happy?????????????????")]
	[SerializeField] float coyoteTime; 
	[SerializeField] float jumpInputBufferTime; 
    public float GravityStrength => -(2* jumpHeight) / (jumpDuration * jumpDuration);
    public float GravityScale => GravityStrength / Physics2D.gravity.y;
    public float MoveAccelerationAmount => (50 * acceleration) / maxHorizontalSpeed;
    
    public float MoveDecelerationAmount => (50 * deceleration) / maxHorizontalSpeed;
    public float JumpForce => Mathf.Abs(GravityStrength) * jumpDuration;

    public Vector2 WallJumpForce => wallJumpForce;

    public float WallJumpMovementReduction => wallJumpMovementReduction;

    public float WallJumpMovementReductionTime => wallJumpMovementReductionTime;

    public bool DoTurnOnWallJump => doTurnOnWallJump;

    public float SlideSpeed => slideSpeed;

    public float SlideAcceleration => slideAcceleration;

    public float CoyoteTime => coyoteTime;

    public float JumpInputBufferTime => jumpInputBufferTime;
    
    public float TerminalSpeed => terminalSpeed;

    public float FastFallGravityScale => fastFallGravityScale;

    public float FastFallTerminalSpeed => fastFallTerminalSpeed;

    public float MaxHorizontalSpeed => maxHorizontalSpeed;

    public float Acceleration => acceleration;

    public float Deceleration => deceleration;

    public float AirAcceleration => airAcceleration;

    public float AirDeceleration => airDeceleration;

    public float JumpHeight => jumpHeight;

    public float JumpDuration => jumpDuration;

    public float ShortJumpGravityScale => shortJumpGravityScale;

    public float JumpHangGravityScale => jumpHangGravityScale;

    public float JumpHangThreshold => jumpHangThreshold;

    public float JumpHangAccelerationScalar => jumpHangAccelerationScalar;

    public float JumpHangSpeedScalar => jumpHangSpeedScalar;

    public float FallingGravityScalar => fallingGravityScalar;
}
