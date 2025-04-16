using UnityEngine;

public class TurretEnemy : Enemy
{
    public GameObject bulletPrefab;
    public int bulletCount = 8;
    public float fireRate = 2f;
    public float bulletSpeed = 5f;

    private float lastShotTime;

    protected override void Start()
    {
        base.Start();
        rb.bodyType = RigidbodyType2D.Static; // ÅÚÌ¨¹Ì¶¨Î»ÖÃ
    }

    void Update()
    {
        if (Time.time - lastShotTime >= fireRate)
        {
            lastShotTime = Time.time;
            ShootInCircle();
        }
    }

    void ShootInCircle()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.enemyFireSound3);
        float angleStep = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
        animator.SetTrigger("Shoot");
    }
}
