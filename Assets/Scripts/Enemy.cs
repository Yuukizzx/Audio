using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int maxHealth = 100;
    protected int currentHealth;

    protected Transform player;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    public GameObject[] pickupPrefabs; // ����������
    public float ammoDropChance = 0.3f; // ��ҩ�������
    public float powerupDropChance = 0.2f; // ǿ�������ܵ������

    private bool isDead = false;
    private WaveManager waveManager;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SetWaveManager(WaveManager manager)
    {
        waveManager = manager;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;

        StartCoroutine(FlashRed());

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Die();
            
        }
    }

    protected void Die()
    {
        if (waveManager != null)
        {
            waveManager.EnemyDied(gameObject);
        }
        SoundManager.Instance.PlaySFX(SoundManager.Instance.enemyDeathSound);
        animator.SetTrigger("Die");
        DropLoot();
        Destroy(gameObject, 1f); // ������������������
    }

    protected void FlipTowardsPlayer()
    {
        if (player.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }      
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red; // ���
        yield return new WaitForSeconds(0.1f); // �ȴ� 0.1 ��
        spriteRenderer.color = Color.white; // �ָ�ԭɫ
    }

    void DropLoot()
    {
        if (pickupPrefabs.Length == 0) return;

        float roll = Random.value; // ���� 0~1 �������

        if (roll < ammoDropChance) // 50% ���䵯ҩ
        {
            Instantiate(pickupPrefabs[0], transform.position, Quaternion.identity);
        }
        else if (roll < ammoDropChance + powerupDropChance) // 50% ����ǿ�����ߣ���ѡһ��
        {
            int randomIndex = Random.Range(1, pickupPrefabs.Length);
            Instantiate(pickupPrefabs[randomIndex], transform.position, Quaternion.identity);
        }
        else if (roll < 1)
        {
            return;
        }
    }

}
