using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]

public class Player_Controller : MonoBehaviour
{ 
    #region Enums
    private enum Directions { UP, DOWN, LEFT, RIGHT }
    #endregion

    #region EditorData //regions are unnecessary, just for organization and should discontinue in future
    [Header("Movement Attributes")]
    [SerializeField] float _moveSpeed = 50f; // f signifies value to be a float

    [Header("Dependencies")]
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] BoxCollider2D _collider;
    #endregion

    #region InternalData
    private Vector2 _moveDir = Vector2.zero;
    private Directions _facingDirection = Directions.RIGHT;

    private readonly int _animMoveRight = Animator.StringToHash("Anim_Player_Assault_Walk_Right"); // Why hash better?
    private readonly int _animIdleRight = Animator.StringToHash("Anim_Player_Assault_Idle_Right");

    private Vector2 _colliderOffsetRight;
    private Vector2 _colliderOffsetLeft => new Vector2(-_colliderOffsetRight.x, _colliderOffsetRight.y);
    #endregion

    private void Awake()
    {
        _colliderOffsetRight = _collider.offset;
    }

    #region Tick
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
    #endregion

    #region InputLogic
    private void GatherInput()
    {
        _moveDir.x = Input.GetAxisRaw("Horizontal"); // Using Unity's project settings name for the movements
        _moveDir.y = Input.GetAxisRaw("Vertical");
    }
    #endregion

    #region MovementLogic
    private void MovementUpdate()
    {
        _rb.velocity = _moveDir.normalized * _moveSpeed * Time.fixedDeltaTime;
    }
    #endregion

    #region AnimationLogic
    private void CalculateFacingDirection()
    {
        if (_moveDir.x != 0)
        {
            if (_moveDir.x > 0) // Moving right
            {
                _facingDirection = Directions.RIGHT;
            }
            else if (_moveDir.x < 0) // Moving left
            {
                _facingDirection = Directions.LEFT;
            }
        }
    }

    private void UpdateAnimation()
    {
        if (_facingDirection == Directions.LEFT)
        {
            _spriteRenderer.flipX = true;
            _collider.offset = _colliderOffsetLeft;
        }
        else if (_facingDirection == Directions.RIGHT)
        {
            _spriteRenderer.flipX = false;
            _collider.offset = _colliderOffsetRight;
        }

        if (_moveDir.SqrMagnitude() > 0) // SqrMagnitude is better for performance than just magnitude; symbolizes ANY movement
        {
            _animator.CrossFade(_animMoveRight, 0); // Crossfade seems to allow for "interrupting" animations temporarily
        }
        else
        {
            _animator.CrossFade(_animIdleRight, 0); // 0 makes the transition instant
        }
    }
    #endregion
}
