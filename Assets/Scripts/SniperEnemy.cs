using UnityEngine;

public class SniperEnemy : Enemy
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform gun;
    public float shootingCooldown = 3f;
    public float aimingTime = 1.5f;
    public float safeDistance = 8f;
    public LineRenderer laserLine;

    private float lastShotTime;

    protected override void Start()
    {
        base.Start();
        laserLine.enabled = false;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 如果玩家太近，狙击手后退
        if (distanceToPlayer < safeDistance)
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
        TryShoot();
        //AimAtPlayer();
    }

    void AimAtPlayer()
    {
        Vector2 direction = (player.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (direction.x < 0)
        {
            gun.localScale = new Vector3(3, -3, 3);
        }
        else
        {
            gun.localScale = new Vector3(3, 3, 3);
        }

        // 让狙击枪朝向玩家
        gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void TryShoot()
    {
        if (Time.time - lastShotTime >= shootingCooldown)
        {
            lastShotTime = Time.time;
            StartCoroutine(AimAndShoot());
        }
    }

    System.Collections.IEnumerator AimAndShoot()
    {
        // 开始瞄准，显示红色激光
        laserLine.enabled = true;
        laserLine.SetPosition(0, firePoint.position);
        laserLine.SetPosition(1, player.position);

        yield return new WaitForSeconds(aimingTime);

        // 关闭激光并射击
        laserLine.enabled = false;
        SoundManager.Instance.PlaySFX(SoundManager.Instance.enemyFireSound);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<EnemyBullet>().Initialize(player.position);
        //animator.SetTrigger("Shoot");
    }
}
