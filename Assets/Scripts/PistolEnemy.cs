using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolEnemy : Enemy
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootingCooldown = 1.5f; // �����ȴ
    public float safeDistance = 4f;       // ����ϣ������ұ��ֵ���С����
    public float attackRange = 7f;        // �������������Χ

    private float lastShotTime;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // ���˻᳢�Ա��� safeDistance ���ϵľ���
        if (distanceToPlayer > safeDistance)
        {
            Vector2 moveDirection = (player.position - transform.position).normalized;
            rb.velocity = moveDirection * moveSpeed;
            animator.SetBool("isMoving", true);
        }
        else if (distanceToPlayer < safeDistance - 1f) // ������һ�㻺�壬���ⶶ��
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

        // ֻ������̷�Χ�ڲſ���
        if (distanceToPlayer <= attackRange)
        {
            Shoot();
        }
    }

        void AimAtPlayer()
    {
        Vector2 direction = (player.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ��ǹ����ת��׼���
        firePoint.rotation = Quaternion.Euler(0, 0, angle);

        // ����������ߣ�firePoint �� `y` �ᷭת
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
            // �����ӵ�
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // ���ӵ���������ƶ�
            bullet.GetComponent<EnemyBullet>().Initialize(player.position);

           
        }
    }
}
