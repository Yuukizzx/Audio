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
        // ���㷽��
        direction = (target - (Vector2)transform.position).normalized;
        speed = bulletSpeed;

        // ����ǶȲ���ת�ӵ�
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 3��������ӵ�
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
            Enemy enemy = collision.GetComponent<Enemy>();  // ͳһ��ȡ����
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                SoundManager.Instance.PlaySFX(SoundManager.Instance.bulletHitSound);
                Destroy(gameObject); // �ӵ����к�����
            }
            Explode();

        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            Destroy(gameObject); // �ӵ�����ǽ�ں�����
            SoundManager.Instance.PlaySFX(SoundManager.Instance.bulletHitSound);
            Explode();
        }
    }

    void Explode()
    {
        // ���ɱ�ը����
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        // �����ӵ�
        Destroy(gameObject);
    }
}
