using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    public float speed = 8f;           // 子弹速度
    public float lifetime = 2f;        // 子弹生命周期（如果超时还没碰撞则销毁）

    private Vector2 direction;         // 子弹方向
    private float currentLifetime = 0f; // 当前生命周期

    // 初始化子弹的方向和速度
    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    void Update()
    {
        // 移动子弹
        transform.Translate(direction * speed * Time.deltaTime);

        // 增加生命周期
        currentLifetime += Time.deltaTime;

        // 如果超时，销毁子弹
        if (currentLifetime > lifetime)
        {
            Destroy(gameObject);
        }
    }

    // 处理子弹碰撞
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果碰到敌人或障碍物，执行相关处理
        if (collision.collider.CompareTag("Enemy"))
        {
            // 这里可以处理敌人受伤或者死亡
            // Example: collision.collider.GetComponent<Enemy>().TakeDamage(damage);

            Destroy(gameObject); // 子弹销毁
        }
        
    }
}
