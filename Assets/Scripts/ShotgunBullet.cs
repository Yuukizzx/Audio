using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    public float speed = 8f;           // �ӵ��ٶ�
    public float lifetime = 2f;        // �ӵ��������ڣ������ʱ��û��ײ�����٣�

    private Vector2 direction;         // �ӵ�����
    private float currentLifetime = 0f; // ��ǰ��������

    // ��ʼ���ӵ��ķ�����ٶ�
    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    void Update()
    {
        // �ƶ��ӵ�
        transform.Translate(direction * speed * Time.deltaTime);

        // ������������
        currentLifetime += Time.deltaTime;

        // �����ʱ�������ӵ�
        if (currentLifetime > lifetime)
        {
            Destroy(gameObject);
        }
    }

    // �����ӵ���ײ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ����������˻��ϰ��ִ����ش���
        if (collision.collider.CompareTag("Enemy"))
        {
            // ������Դ���������˻�������
            // Example: collision.collider.GetComponent<Enemy>().TakeDamage(damage);

            Destroy(gameObject); // �ӵ�����
        }
        
    }
}
