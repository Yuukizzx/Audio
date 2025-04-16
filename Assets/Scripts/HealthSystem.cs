using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.EventSystems;

public class HealthSystem : MonoBehaviour
{
    public CinemachineImpulseSource impulseSource;
    public int maxHealth = 100;   // 最大生命值
    public int currentHealth;    // 当前生命值
    private bool isInvincible = false;
    private bool Invincible = false; // 是否无敌（用于受伤时的短暂无敌）
    public float invincibleDuration = 0.5f;  // 无敌时间（防止被瞬间秒杀）

     // UI 血条（如果有）

    public int healingamount = 50;
    public GameObject InvisFx;
    public GameObject Hurt;

    private Animator animator;  // 角色动画
    private SpriteRenderer spriteRenderer; // 角色变色闪烁

    
    public GameObject gameOverUI;

    
    public WeaponManager weapon;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Hurt.SetActive(false);
        

        gameOverUI.SetActive(false);
        

    }
    private void Update()
    {
        hurt();
    }
    public void TakeDamage(int amount)
    {
        if (isInvincible) return;  // 如果无敌，不受伤害
        if (Invincible) return;
        SoundManager.Instance.PlaySFX(SoundManager.Instance.playerHurtSound);
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            if (impulseSource != null)
            {
                impulseSource.GenerateImpulse();
            }
            //animator.SetTrigger("Hurt");  // 播放受伤动画
            StartCoroutine(BecomeInvincible());  // 进入短暂无敌
        }

        
    }

    IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        for (int i = 0; i < 4; i++)  // 让角色闪烁 5 次
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(invincibleDuration / 10);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(invincibleDuration / 10);
        }
        isInvincible = false;
    }

    public void Heal()
    {
        currentHealth += healingamount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        
    }

   

    void Die()
    {
        if (weapon.isDead) return; // 避免多次触发
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX(SoundManager.Instance.lose);
        animator.SetTrigger("Die");
        // 禁止玩家控制
        GetComponent<PlayerMove>().enabled = false;
        // 让角色消失（或者触发重生逻辑）
        Destroy(gameObject, 1f);

        gameOverUI.SetActive(true);

        Time.timeScale = 0f;
        weapon.isDead = true;
    }

    public IEnumerator Invincibility(float duration)
    {
        Invincible = true;
        InvisFx.SetActive(true);
        Debug.Log("无敌方法执行了");
        yield return new WaitForSeconds(duration);
        Invincible = false;
        InvisFx.SetActive(false);
    }

    public void hurt()
    {
        if (currentHealth <= 20)
        {
            Hurt.SetActive(true);
        }
        else
        {
            Hurt.SetActive(false);
        }
    }
}
