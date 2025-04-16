using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolEnemy : Enemy
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootingCooldown = 1.5f; // 射击冷却
    public float safeDistance = 4f;       // 敌人希望与玩家保持的最小距离
    public float attackRange = 7f;        // 开火的最大射击范围

    private float lastShotTime;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 敌人会尝试保持 safeDistance 以上的距离
        if (distanceToPlayer > safeDistance)
        {
            Vector2 moveDirection = (player.position - transform.position).normalized;
            rb.velocity = moveDirection * moveSpeed;
            animator.SetBool("isMoving", true);
        }
        else if (distanceToPlayer < safeDistance - 1f) // 允许有一点缓冲，避免抖动
        {
            Vector2 moveDirection = (transform.position - player.position).normalized;
            rb.velocity = moveDirection * moveSpeed;
            animator.SetBool("isMoving", true);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isMoving", false);
        }

        FlipTowardsPlayer();

        // 只有在射程范围内才开火
        if (distanceToPlayer <= attackRange)
        {
            Shoot();
        }
    }

        void AimAtPlayer()
    {
        Vector2 direction = (player.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 让枪口旋转对准玩家
        firePoint.rotation = Quaternion.Euler(0, 0, angle);

        // 如果玩家在左边，firePoint 的 `y` 轴翻转
        if (player.position.x < transform.position.x)
            firePoint.localScale = new Vector3(1, -1, 1);
        else
            firePoint.localScale = new Vector3(1, 1, 1);
    }

    void Shoot()
    {
        if (Time.time - lastShotTime >= shootingCooldown)
        {
            lastShotTime = Time.time;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.enemyFireSound2);
            // 生成子弹
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // 让子弹朝向玩家移动
            bullet.GetComponent<EnemyBullet>().Initialize(player.position);

           
        }
    }
}
