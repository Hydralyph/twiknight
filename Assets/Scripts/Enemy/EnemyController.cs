using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum EnemyType
    {
        Idle,
        Waypoint
    }

    // Properties
    public bool CanMove { get; set; }
    public bool CanAttack { get; set; }
    private bool IsAttacking { get; set; }
    private bool PlayerInRange { get; set; }

    private Rigidbody2D enemyRB;
    private BoxCollider2D enemyCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private LayerMask Damageable;
    [SerializeField] private EnemyType enemyType;

    // Animator variables
    private Animator enemyAnimator;
    private int _currentState;
    private float _LockedTimer;
    private static readonly int Anim_Idle = Animator.StringToHash("SkeletonIdle");
    private static readonly int Anim_Walk = Animator.StringToHash("SkeletonWalk");
    private static readonly int Anim_Attack = Animator.StringToHash("SkeletonAttack");

    // Audio Variables
    private AudioSource enemyAudioSource;
    [SerializeField] private AudioClip hurtSFX;


    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<BoxCollider2D>();
        enemyAnimator = GetComponent<Animator>();
        enemyAudioSource = GetComponent<AudioSource>();

        CanAttack = true;
    }

    private void Update()
    {
        int state = GetAnimationState();

        if (state == _currentState) return;
        enemyAnimator.CrossFade(state, 0, 0);
        _currentState = state;
        IsAttacking = false;
    }

    private void FixedUpdate()
    {
        
        if(spriteRenderer.flipX == false) PlayerInRange = Physics2D.OverlapBox(new Vector2(enemyCollider.bounds.max.x + 1f, enemyCollider.bounds.center.y), new Vector2(2f, 2f), 0f, Damageable);
        else PlayerInRange = Physics2D.OverlapBox(new Vector2(enemyCollider.bounds.min.x - 1.5f, enemyCollider.bounds.center.y), new Vector2(2f, 2f), 0f, Damageable);


        // Gives a random chance for the enemy to attack when the player is in range
        if(PlayerInRange == true && CanAttack == true)
        {
            if (Random.Range(0f, 100f) > 80f)
            {
                CanAttack = false;
                IsAttacking = true;
                PlayerManager.playerManager.TakeDamage();
                StartCoroutine(AttackWaitTimer());
            }
        }

        // Gives a random chance for turning the enemy around if their type is Idle
        if(enemyType.Equals(EnemyType.Idle))
        {
            if (Random.Range(0f, 100f) > 99f && CanMove == true) spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    // AttackWaitTimer() : Gives a delay to the enemies attack ability, stopping instant full health loss upon attack
    IEnumerator AttackWaitTimer()
    {
        yield return new WaitForSeconds(2f);
        CanAttack = true;
        StopCoroutine(AttackWaitTimer());
    }

    private int GetAnimationState()
    {
        if (Time.time < _LockedTimer) return _currentState;
        CanMove = true;

        // Firewall pattern priorities :: This will go down the list from most important to least important
        if (IsAttacking) return LockAnimationState(Anim_Attack, 1f);
        return enemyRB.velocity.x == 0 ? Anim_Idle : Anim_Walk;

        int LockAnimationState(int s, float t)
        {
            CanMove = false;
            _LockedTimer = Time.time + t;
            return s;
        }
    }

    public void TakeDamage()
    {
        StartCoroutine(WaitForDeath());
    }

    IEnumerator WaitForDeath()
    {
        CanAttack = false;
        CanMove = false;
        yield return new WaitForSeconds(0.40f);
        enemyAudioSource.PlayOneShot(hurtSFX);
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
        StopCoroutine(WaitForDeath());
    }

    //private void OnDrawGizmos()
    //{
    //    if (spriteRenderer.flipX == false) Gizmos.DrawCube(new Vector3(enemyCollider.bounds.max.x + 1f, enemyCollider.bounds.center.y, 0f), new Vector3(2f, 2f, 0f));
    //    else Gizmos.DrawCube(new Vector3(enemyCollider.bounds.min.x - 1.5f, enemyCollider.bounds.center.y, 0f), new Vector3(2f, 2f, 0f));

    //}
}
