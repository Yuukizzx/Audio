using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f; // 子弹速度
    public int damage = 10; // 子弹伤害
    public float lifetime = 3f; // 子弹存活时间
    private Vector2 direction; // 移动方向


    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void Initialize(Vector2 targetPosition)
    {
        // 计算方向
        direction = (targetPosition - (Vector2)transform.position).normalized;

        // 计算角度并旋转子弹朝向目标
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 自动销毁子弹
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<HealthSystem>().TakeDamage(damage);
            Destroy(gameObject); // 子弹击中玩家后销毁
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            Destroy(gameObject); // 子弹击中墙壁后销毁
        }
    }
}
