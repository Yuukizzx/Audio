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

       
            

        // �û������Ŀ�귽��
        RotateTowardsDirection();
    }

    void Update()
    {
        // �ƶ������
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // ��ת�����
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
            Destroy(gameObject); // �ӵ�����ǽ�ں�����
            SoundManager.Instance.PlaySFX(SoundManager.Instance.RocketFx);
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

        // ��ⱬը��Χ�ڵ�������ײ��
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D enemy in enemiesInRange)
        {
            if (enemy.CompareTag("Enemy"))  // ȷ����ײ�����ǵ���
            {
                Enemy enemys = enemy.GetComponent<Enemy>();  // ͳһ��ȡ����
                if (enemys != null)
                {
                    enemys.TakeDamage(explosionDamage);
                }
            }
        }

        // ��ը������ٻ����
        Destroy(gameObject);

    }
}