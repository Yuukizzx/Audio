using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float attackRange = 1.5f;  // 攻击距离
    public float attackWidth = 1.0f;  // 攻击宽度
    public int attackDamage = 20;     // 伤害
    public Transform attackPoint;     // 攻击判定点
    public LayerMask playerLayer;     // 只检测玩家

    private bool isAttacking = false;
    private float attackCooldown = 1.5f;
    private float lastAttackTime;

    void Update()
    {
        if (isAttacking) return;  // 正在攻击时不移动

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();  // 追击玩家
        }
        else if (Time.time - lastAttackTime >= attackCooldown)
        {
            StartAttack();
        }

        FlipTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        Vector2 moveDirection = (player.position - transform.position).normalized;
        rb.velocity = moveDirection * moveSpeed;
        animator.SetBool("isMoving", true);
    }

    void StartAttack()
    {
        isAttacking = true;
        lastAttackTime = Time.time;
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Attack");
    }

    // 这个方法在攻击动画的关键帧上触发
    public void PerformAttack()
    {
        Collider2D hit = Physics2D.OverlapBox(attackPoint.position, new Vector2(attackRange, attackWidth), 0, playerLayer);

        if (hit != null)
        {
            HealthSystem playerHealth = hit.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }

    // 在攻击动画的"结束帧"添加动画事件，调用此函数
    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isMoving", true);  // 继续移动
    }

    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackPoint.position, new Vector2(attackRange, attackWidth));
        }
    }
}
