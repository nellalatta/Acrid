using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player_Controller : MonoBehaviour
{ 
    private enum Directions { UP, DOWN, LEFT, RIGHT }

    [SerializeField] private PlayerStats stats; // TODO Ensure stats stay consistent upon game start (ex. in Player and PlayerController)

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D collider;

    private Vector2 moveDir = Vector2.zero;
    private Directions facingDirection = Directions.RIGHT;

    private readonly int animMoveRight = Animator.StringToHash("Anim_Player_Assault_Walk_Right");
    private readonly int animIdleRight = Animator.StringToHash("Anim_Player_Assault_Idle_Right");

    private Vector2 colliderOffsetRight;
    private Vector2 colliderOffsetLeft => new Vector2(-colliderOffsetRight.x, colliderOffsetRight.y);

    private void Awake()
    {
        colliderOffsetRight = collider.offset;
    }

    private void Update()
    {
        GatherInput();
        CalculateFacingDirection();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        MovementUpdate();
    }

    private void GatherInput()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
    }

    private void MovementUpdate()
    {
        if (stats != null)
        {
            rb.velocity = moveDir.normalized * stats.moveSpeed * Time.fixedDeltaTime;
        }
    }

    private void CalculateFacingDirection()
    {
        if (moveDir.x != 0)
        {
            facingDirection = moveDir.x > 0 ? Directions.RIGHT : Directions.LEFT;
        }
    }

    private void UpdateAnimation()
    {
        if (facingDirection == Directions.LEFT)
        {
            spriteRenderer.flipX = true;
            collider.offset = colliderOffsetLeft;
        }
        else if (facingDirection == Directions.RIGHT)
        {
            spriteRenderer.flipX = false;
            collider.offset = colliderOffsetRight;
        }

        if (moveDir.SqrMagnitude() > 0) // SqrMagnitude is better for performance than just magnitude; symbolizes ANY movement
        {
            animator.CrossFade(animMoveRight, 0); // Crossfade seems to allow for "interrupting" animations temporarily
        }
        else
        {
            animator.CrossFade(animIdleRight, 0); // 0 makes the transition instant
        }
    }
}