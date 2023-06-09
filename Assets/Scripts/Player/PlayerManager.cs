using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerManager : MonoBehaviour
{
    // Class References
    public static PlayerManager playerManager;
    [SerializeField] private InputManager inputManager;
    
    // Player Properties
    private bool CanMove { get; set; }
    private bool IsSprinting { get; set; }
    private bool IsJumping { get; set; }
    private bool IsGrounded { get; set; }

    // Movement Variables
    //[SerializeField, Range(1f, 50f)] private float walkSpeed;
    //[SerializeField, Range(1f, 50f)] private float sprintSpeed;
    //[SerializeField, Range(1f, 50f)] private float jumpSpeed;

    // Velocity Variables
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAccel = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAccel = 20f;
    private Vector2 direction;
    private Vector2 moveVelocity;
    private Vector2 desiredMoveVelocity;
    private float maxSpeedChange;
    private float acceleration;

    // Jump Variables
    [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f;
    [SerializeField, Range(0f, 5f)] private float downMovementMulti = 3f;
    [SerializeField, Range(0f, 5f)] private float upMovementMulti = 1.7f;
    private float defaultGravityScale;

    [SerializeField] private LayerMask collidableGround;
    private Camera playerCamera;
    private Rigidbody2D playerRB;
    private BoxCollider2D boxCastCol;
    // private Vector2 moveDirection;
    // Todo: Add a <Weapon> List, after creating the Weapon class (maybe scriptable object?)
    private int currentWeapon; // Note: Could potentially hash these instead, check first watch later video
    private int livesLeft;
    private int movementSkillLevel = 1;
    private int attackSkillLevel = 1;


    // Start is called before the first frame update
    void Awake()
    {
        if(playerManager == null)
        {
            playerManager = this;
        } 
        else
        {
            Destroy(this.gameObject);
        }

        defaultGravityScale = 1f;
    }

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        boxCastCol = GetComponent<BoxCollider2D>();

        inputManager.MoveEvent += HandleMovement;
        inputManager.JumpEvent += HandleJump;
        inputManager.JumpCancelledEvent += HandleJumpCancel;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        desiredMoveVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed, 0f);

        // IS GROUNDED CHECK?
        moveVelocity = playerRB.velocity;

        acceleration = IsGrounded ? maxAccel : maxAirAccel;
        maxSpeedChange = acceleration * Time.fixedDeltaTime;
        moveVelocity.x = Mathf.MoveTowards(moveVelocity.x, desiredMoveVelocity.x, maxSpeedChange);

        if (playerRB.velocity.y > 0)
        {
            playerRB.gravityScale = upMovementMulti;
        }
        else if (playerRB.velocity.y < 0)
        {
            playerRB.gravityScale = downMovementMulti;
        }
        else if (playerRB.velocity.y == 0)
        {
            playerRB.gravityScale = defaultGravityScale;
        }

        playerRB.velocity = moveVelocity;
    }

    #region EventHandlers
    private void HandleMovement(Vector2 dir)
    {
        direction.x = dir.x;
    }

    private void HandleJump()
    {
        if(IsJumping == false)
        {
            IsJumping = true;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);

            if(moveVelocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - moveVelocity.y, 0f);
            }

            moveVelocity.y += jumpSpeed;
            IsJumping = false;
            playerRB.velocity = moveVelocity;

        } else if(IsGrounded == false || IsJumping == true)
        {
            // DO NOTHING
        }

        
    }

    private void HandleJumpCancel()
    {
        IsJumping = false;
    }

    private void Attack()
    {

    }

    private void Interact()
    {

    }

    private void SwitchWeapon()
    {

    }
    #endregion

    #region Misc Functions

    private bool CheckGrounded()
    {
        Physics2D.BoxCast(boxCastCol.bounds.center, boxCastCol.bounds.size, 0f, Vector2.down, .1f, collidableGround);
        // TODO: Fix this later, with either an overlap circle or Boxcast || https://www.youtube.com/watch?v=c3iEl5AwUF8
        return false;
    }

    #endregion
}