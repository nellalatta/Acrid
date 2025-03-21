using UnityEngine;

public class EnemySpider : Enemy
{
    [SerializeField] private float shootingRange = 5f;
    [SerializeField] private float shootCooldown = 2f;
    [SerializeField] private Weapon weapon;

    private Transform player;
    private float shootTimer;
    private Vector2 moveDir = Vector2.zero;

    private enum Directions { LEFT, RIGHT }
    private Directions facingDirection = Directions.RIGHT;

    private readonly int animMove = Animator.StringToHash("Enemy_Spider_Walk_Right");
    private readonly int animIdle = Animator.StringToHash("Enemy_Spider_Idle_Right");

    private Vector2 colliderOffsetRight;
    private Vector2 colliderOffsetLeft => new Vector2(-colliderOffsetRight.x, colliderOffsetRight.y);

    protected override void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        weapon = GetComponent<Weapon>();
        colliderOffsetRight = enemyCollider.offset;
    }

    protected override void Update()
    {
        base.Update();
        shootTimer += Time.deltaTime;

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            moveDir = (player.position - transform.position).normalized;
            CalculateFacingDirection();

            if (distanceToPlayer <= shootingRange && shootTimer >= shootCooldown)
            {
                weapon.MainShoot(player.position);
                shootTimer = 0f;
            }
        }
    }

    private void CalculateFacingDirection()
    {
        if (moveDir.x != 0)
        {
            facingDirection = moveDir.x > 0 ? Directions.RIGHT : Directions.LEFT;
        }
    }

    protected override void UpdateAnimation()
    {
        Vector2 velocity = aiPath.velocity;

        if (velocity.x != 0)
        {
            facingDirection = velocity.x > 0 ? Directions.RIGHT : Directions.LEFT;
        }

        spriteRenderer.flipX = facingDirection == Directions.LEFT;
        enemyCollider.offset = facingDirection == Directions.LEFT ? colliderOffsetLeft : colliderOffsetRight;

        animator.CrossFade(velocity.sqrMagnitude > 0.1f ? animMove : animIdle, 0);
    }
}
