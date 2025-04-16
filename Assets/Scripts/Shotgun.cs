using UnityEngine;

public class Shotgun : Weapon
{
    public int pelletCount = 5;
    public float spreadAngle = 10f;

    protected override void Fire(Vector2 target)
    {
        for (int i = 0; i < pelletCount; i++)
        {
            float angle = Random.Range(-spreadAngle, spreadAngle);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector2 newDirection = rotation * (target - (Vector2)firePoint.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().Initialize(newDirection, 8f);
        }
    }
}
