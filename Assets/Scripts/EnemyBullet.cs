using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f; // �ӵ��ٶ�
    public int damage = 10; // �ӵ��˺�
    public float lifetime = 3f; // �ӵ����ʱ��
    private Vector2 direction; // �ƶ�����


    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void Initialize(Vector2 targetPosition)
    {
        // ���㷽��
        direction = (targetPosition - (Vector2)transform.position).normalized;

        // ����ǶȲ���ת�ӵ�����Ŀ��
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // �Զ������ӵ�
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
            Destroy(gameObject); // �ӵ�������Һ�����
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            Destroy(gameObject); // �ӵ�����ǽ�ں�����
        }
    }
}
