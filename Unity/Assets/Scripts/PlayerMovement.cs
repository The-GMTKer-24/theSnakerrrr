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
    float moveSpeed;
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

    public bool FacingRight { get; private set; }
    public bool Jumping { get; private set; }
    public bool WallJumping { get; private set; }
    public bool Sliding { get; private set; }

    public float LastOnGroundTime { get; private set; }
    public float LastOnWallTime { get; private set; }
    public float LastOnWallRightTime { get; private set; }
    public float LastOnWallLeftTime { get; private set; }
    private void Start()
    {
        SetGravity(movementData.GravityScale);
        
    }




    private void SetGravity(float gravity)
    {
        rb.gravityScale = gravity;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    private void Awake()
    {
        map = new InputMap(); 
        move = map.Player.Move;
    }

    private void OnEnable()
    {
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

}
