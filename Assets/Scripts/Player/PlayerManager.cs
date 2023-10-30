/*
 * Filename: PlayerManager.cs
 * Author: Jamie Adaway
 * Last Updated: 18/10/23 12:26
 * Desc: Single file Script for handling all player features: Input; Animation; Data etc.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerManager : MonoBehaviour
{
    // Class References
    public static PlayerManager playerManager;
    [SerializeField] private InputManager inputManager;

    // Player Properties
    public bool CanMove { get; set; }
    public bool CanAttack { get; set; }
    private bool IsJumping { get; set; }
    private bool IsAttacking { get; set; }
    private bool IsGrounded { get; set; }
    private bool EnemyInRange { get; set; }

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
    [SerializeField] private LayerMask Damageable;
    private Camera playerCamera;
    private Rigidbody2D playerRB;
    private BoxCollider2D playerCollider;
    public int PlayerLives { get; private set; } = 4;

    // Animator variables
    private Animator playerAnimator;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    private int _currentState;
    private float _LockedTimer;
    private static readonly int Anim_Idle = Animator.StringToHash("PlayerIdle");
    private static readonly int Anim_Walk = Animator.StringToHash("PlayerWalk");
    private static readonly int Anim_Jump = Animator.StringToHash("PlayerJump");
    private static readonly int Anim_Fall = Animator.StringToHash("PlayerFall");
    private static readonly int Anim_Attack = Animator.StringToHash("PlayerAttack");
    

    // Audio Variables
    private AudioSource playerAudioSource;
    [SerializeField] private AudioClip[] audioClips;



    // Todo: Add a <Weapon> List, after creating the Weapon class (maybe scriptable object?)
    //private int currentWeapon; // Note: Could potentially hash these instead, check first watch later video
    //private int movementSkillLevel = 1;
    //private int attackSkillLevel = 1;

    void Awake()
    {
        // Keep this class a single instance class (singleton)
        if(playerManager == null)
        {
            playerManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        defaultGravityScale = 1f;
        CanMove = true;

    }

  

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerAnimator = GetComponentInParent<Animator>();
        playerAudioSource = playerRB.GetComponent<AudioSource>();


        inputManager.MoveEvent += HandleMovement;
        inputManager.JumpEvent += HandleJump;
        inputManager.JumpCancelledEvent += HandleJumpCancel;
        inputManager.AttackEvent += HandleAttack;

       
    }

    // Update is called once per frame
    void Update()
    {
        // Set the players horizontal sprite direction
        if (direction.x != 0) playerSpriteRenderer.flipX = direction.x < 0;
        int state = GetAnimationState();
        if(IsGrounded) IsJumping = false;

        if (state == _currentState) return;
        playerAnimator.CrossFade(state, 0, 0);
        _currentState = state;

    }

    // Checks if the AnimationState is locked, otherwise use a Firewall pattern to determine current player state for Animation
    private int GetAnimationState()
    {
        if (Time.time < _LockedTimer) return _currentState;

        // Firewall pattern priorities :: This will go down the list from most important to least important
        if (IsAttacking)
        {
            IsAttacking = false;
            return LockAnimationState(Anim_Attack, 0.6f);
        }
        if (IsJumping) return Anim_Jump;
        if (IsGrounded) return playerRB.velocity.x == 0 ? LockAnimationState(Anim_Idle, 0.15f) : LockAnimationState(Anim_Walk, 0.15f);
        return playerRB.velocity.y > 0 ? Anim_Jump : LockAnimationState(Anim_Fall, 0.15f);

        // LockAnimationState() :: Takes an Animation hashed number and a time before allowing any animation change
        int LockAnimationState(int s, float t)
        {
            _LockedTimer = Time.time + t;
            return s;
        }
    }


    // Handle anything related to Physics or movement here
    private void FixedUpdate()
    {
        CheckGrounded();
        CheckEnemyInRange();
        desiredMoveVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed, 0f);

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
        if (CanMove == false) return;
        direction.x = dir.x;
    }

    private void HandleJump()
    {
        if (IsJumping == false && IsGrounded == true)
        {
            IsJumping = true;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);

            if (moveVelocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - moveVelocity.y, 0f);
            }

            moveVelocity.y += jumpSpeed;
            playerRB.velocity = moveVelocity;
            StartCoroutine(TempDisableJumpReset());
        }
    }

    private void HandleJumpCancel()
    {
        return;
    }

    private void HandleAttack()
    {
        if (CanAttack == false) return;
        IsAttacking = true;

        if (EnemyInRange == true)
        {
            Collider2D[] colliders;
            if (playerSpriteRenderer.flipX == false) colliders = Physics2D.OverlapBoxAll(new Vector2(playerCollider.bounds.max.x + 1f, playerCollider.bounds.center.y), new Vector2(2f, 2f), 0f, Damageable);
            else colliders = Physics2D.OverlapBoxAll(new Vector2(playerCollider.bounds.min.x - 1f, playerCollider.bounds.center.y), new Vector2(2f, 2f), 0f, Damageable);


            foreach (Collider2D col in colliders)
            {
                col.gameObject.SendMessage("TakeDamage", 1);
            }
        }

        EnemyInRange = false;
    }

    // Function stub for later development
    private void Interact()
    {
        return;
    }

    // Function stub for later development
    private void SwitchWeapon()
    {
        return;
    }
    #endregion

    #region Misc Functions

    // TakeDamage(): This is ran when an enemy or damaging tile is hit. Takes one from PlayerLives and plays the oneshot Damage animation
    public void TakeDamage()
    {
        playerAudioSource.PlayOneShot(audioClips[0]);
        PlayerLives -= 1;
        PlayerLives = Math.Clamp(PlayerLives, 0, 3);
    }

    // CheckGrounded(): This checks ground by using the OverlapBox method, pretty much spawning a box collider temporarily
    private void CheckGrounded()
    {
        IsGrounded = Physics2D.OverlapBox(new Vector2(playerCollider.bounds.center.x, playerCollider.bounds.min.y + -0.3f), new Vector2(0.75f, 0.5f), 0f, collidableGround);
        // TODO: Fix this later, with either an overlap circle or Boxcast || https://www.youtube.com/watch?v=c3iEl5AwUF8d
    }

    private void CheckEnemyInRange()
    {
        if (playerSpriteRenderer.flipX == false) EnemyInRange = Physics2D.OverlapBox(new Vector2(playerCollider.bounds.max.x + 1f, playerCollider.bounds.center.y), new Vector2(2f, 2f), 0f, Damageable);
        else EnemyInRange = Physics2D.OverlapBox(new Vector2(playerCollider.bounds.min.x - 1.5f, playerCollider.bounds.center.y), new Vector2(2f, 2f), 0f, Damageable);

    }

    // TempDisableJumpReset() : This temporarily disables the resetting of IsJumping due to issues with the animation states
    IEnumerator TempDisableJumpReset()
    {
        yield return new WaitForSeconds(0.5f);
        IsJumping = false;
    }

    #endregion

    // OnDrawGizmos - For visibility when testing attack hitbox generations
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube(new Vector3(playerCollider.bounds.max.x + 1f, playerCollider.bounds.center.y, 0f), new Vector3(2f, 2f, 0f));
    //    Gizmos.DrawCube(new Vector3(playerCollider.bounds.min.x - 1f, playerCollider.bounds.center.y, 0f), new Vector3(2f, 2f, 0f));

    //}

}