using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 15f;
    public float explosionRadius = 5f;
    public int explosionDamage = 50;
    public GameObject explosionEffectPrefab;
    public ParticleSystem smokeTrail;

    private Vector2 direction;

    public void Initialize(Vector2 target, float rocketSpeed, float radius, int damage, GameObject explosionEffect)
    {
        direction = (target - (Vector2)transform.position).normalized;
        speed = rocketSpeed;
        explosionRadius = radius;
        explosionDamage = damage;
        explosionEffectPrefab = explosionEffect;

       
            

        // 让火箭朝向目标方向
        RotateTowardsDirection();
    }

    void Update()
    {
        // 移动火箭弹
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // 旋转火箭弹
        RotateTowardsDirection();
    }

    void RotateTowardsDirection()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Explode();
            SoundManager.Instance.PlaySFX(SoundManager.Instance.RocketFx);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            Destroy(gameObject); // 子弹击中墙壁后销毁
            SoundManager.Instance.PlaySFX(SoundManager.Instance.RocketFx);
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

        // 检测爆炸范围内的所有碰撞体
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D enemy in enemiesInRange)
        {
            if (enemy.CompareTag("Enemy"))  // 确保碰撞到的是敌人
            {
                Enemy enemys = enemy.GetComponent<Enemy>();  // 统一获取基类
                if (enemys != null)
                {
                    enemys.TakeDamage(explosionDamage);
                }
            }
        }

        // 爆炸完后销毁火箭弹
        Destroy(gameObject);

    }
}