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

    public GameObject[] pickupPrefabs; // 掉落物数组
    public float ammoDropChance = 0.3f; // 弹药掉落概率
    public float powerupDropChance = 0.2f; // 强化道具总掉落概率

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
        Destroy(gameObject, 1f); // 播放死亡动画后销毁
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
        spriteRenderer.color = Color.red; // 变红
        yield return new WaitForSeconds(0.1f); // 等待 0.1 秒
        spriteRenderer.color = Color.white; // 恢复原色
    }

    void DropLoot()
    {
        if (pickupPrefabs.Length == 0) return;

        float roll = Random.value; // 生成 0~1 的随机数

        if (roll < ammoDropChance) // 50% 掉落弹药
        {
            Instantiate(pickupPrefabs[0], transform.position, Quaternion.identity);
        }
        else if (roll < ammoDropChance + powerupDropChance) // 50% 掉落强化道具（四选一）
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
