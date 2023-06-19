using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    private bool AnimationLocked { get; set; }

    // Velocity/Movement Variables
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

    // Misc Variables
    [SerializeField] private LayerMask collidableGround;
    private Camera playerCamera;
    private Rigidbody2D playerRB;
    private BoxCollider2D playerCollider;
    
    // Animator variables
    private Animator playerAnimator;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    private static readonly int Anim_Idle = Animator.StringToHash("PlayerIdle");
    private static readonly int Anim_Walk = Animator.StringToHash("PlayerWalk");
    private static readonly int Anim_Jump = Animator.StringToHash("PlayerJump");
    private static readonly int Anim_Fall = Animator.StringToHash("PlayerFall");
    private static readonly int Anim_Attack = Animator.StringToHash("PlayerAttack");

    // Todo: Add a <Weapon> List, after creating the Weapon class (maybe scriptable object?)
    //private int currentWeapon; // Note: Could potentially hash these instead, check first watch later video
    //private int livesLeft;
    //private int movementSkillLevel = 1;
    //private int attackSkillLevel = 1;


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
        playerCollider = GetComponent<BoxCollider2D>();
        playerAnimator = GetComponentInParent<Animator>();
        

        inputManager.MoveEvent += HandleMovement;
        inputManager.JumpEvent += HandleJump;
        inputManager.JumpCancelledEvent += HandleJumpCancel;
        inputManager.AttackEvent += HandleAttack;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRB.velocity.y > 0.2 && IsJumping == true)
        {
            playerAnimator.CrossFade(Anim_Jump, 0f, 0);
        }
        else if (playerRB.velocity.y < 0.2 && IsGrounded == false)
        {
            playerAnimator.CrossFade(Anim_Fall, 0f, 0);
        }
        else if (playerRB.velocity.x > 0.2 || playerRB.velocity.x < -0.2)
        {
            playerAnimator.CrossFade(Anim_Walk, 0, 0);
        }
        else
        {
            playerAnimator.CrossFade(Anim_Idle, 0f, 0);
        }

        if (direction.x < 0)
        {
            playerSpriteRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            playerSpriteRenderer.flipX = false;
        }

        //Reset to Idle animation just in case these checks fail
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        Debug.Log($"IsGrounded: {IsGrounded}");
        desiredMoveVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed, 0f);

        // IS GROUNDED CHECK?
        moveVelocity = playerRB.velocity;

        acceleration = IsGrounded ? maxAccel : maxAirAccel;
        maxSpeedChange = acceleration * Time.fixedDeltaTime;
        moveVelocity.x = Mathf.MoveTowards(moveVelocity.x, desiredMoveVelocity.x, maxSpeedChange);

        if (playerRB.velocity.y > 0 || IsJumping == true)
        {
            playerRB.gravityScale = upMovementMulti;
        }
        else if (playerRB.velocity.y < 0 || IsJumping == true)
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
        if(IsJumping == false && IsGrounded == true)
        {
            IsJumping = true;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);

            if(moveVelocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - moveVelocity.y, 0f);
            }

            moveVelocity.y += jumpSpeed;
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

    private void HandleAttack()
    {
        //playerAnimator.CrossFade(Anim_Attack, 0, 0);
    }

    private void Interact()
    {

    }

    private void SwitchWeapon()
    {

    }
    #endregion

    #region Misc Functions

    private void CheckGrounded()
    {
        IsGrounded = Physics2D.OverlapBox(new Vector2(playerCollider.bounds.center.x, playerCollider.bounds.min.y + -0.3f), new Vector2(0.75f, 0.5f), 0f, collidableGround);
        // TODO: Fix this later, with either an overlap circle or Boxcast || https://www.youtube.com/watch?v=c3iEl5AwUF8d
    }

    // THIS FUNCTION IS ONLY HERE FOR DEBUG TESTING OF THE GROUND CHECK, TO REMOVE LATER
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube(new Vector2(playerCollider.bounds.center.x, playerCollider.bounds.min.y + -0.3f), new Vector3(0.75f, 0.5f, 0.1f));
    //}

    #endregion
}