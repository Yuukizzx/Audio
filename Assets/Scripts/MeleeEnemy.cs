using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float attackRange = 1.5f;  // ��������
    public float attackWidth = 1.0f;  // �������
    public int attackDamage = 20;     // �˺�
    public Transform attackPoint;     // �����ж���
    public LayerMask playerLayer;     // ֻ������

    private bool isAttacking = false;
    private float attackCooldown = 1.5f;
    private float lastAttackTime;

    void Update()
    {
        if (isAttacking) return;  // ���ڹ���ʱ���ƶ�

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();  // ׷�����
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

    // ��������ڹ��������Ĺؼ�֡�ϴ���
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

    // �ڹ���������"����֡"��Ӷ����¼������ô˺���
    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isMoving", true);  // �����ƶ�
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
