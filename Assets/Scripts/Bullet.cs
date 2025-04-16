using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    private Vector2 direction;
    private bool isPenetrating = false;
    public GameObject explosionPrefab;

    public void Initialize(Vector2 target, float bulletSpeed)
    {
        // 计算方向
        direction = (target - (Vector2)transform.position).normalized;
        speed = bulletSpeed;

        // 计算角度并旋转子弹
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 3秒后销毁子弹
        Destroy(gameObject, 3f);
    }

    public void SetPenetrating(bool penetrating)
    {
        isPenetrating = penetrating;
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPenetrating)
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();  // 统一获取基类
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                SoundManager.Instance.PlaySFX(SoundManager.Instance.bulletHitSound);
                Destroy(gameObject); // 子弹击中后销毁
            }
            Explode();

        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            Destroy(gameObject); // 子弹击中墙壁后销毁
            SoundManager.Instance.PlaySFX(SoundManager.Instance.bulletHitSound);
            Explode();
        }
    }

    void Explode()
    {
        // 生成爆炸动画
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        // 销毁子弹
        Destroy(gameObject);
    }
}
