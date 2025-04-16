using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.EventSystems;

public class HealthSystem : MonoBehaviour
{
    public CinemachineImpulseSource impulseSource;
    public int maxHealth = 100;   // �������ֵ
    public int currentHealth;    // ��ǰ����ֵ
    private bool isInvincible = false;
    private bool Invincible = false; // �Ƿ��޵У���������ʱ�Ķ����޵У�
    public float invincibleDuration = 0.5f;  // �޵�ʱ�䣨��ֹ��˲����ɱ��

     // UI Ѫ��������У�

    public int healingamount = 50;
    public GameObject InvisFx;
    public GameObject Hurt;

    private Animator animator;  // ��ɫ����
    private SpriteRenderer spriteRenderer; // ��ɫ��ɫ��˸

    
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
        if (isInvincible) return;  // ����޵У������˺�
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
            //animator.SetTrigger("Hurt");  // �������˶���
            StartCoroutine(BecomeInvincible());  // ��������޵�
        }

        
    }

    IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        for (int i = 0; i < 4; i++)  // �ý�ɫ��˸ 5 ��
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
        if (weapon.isDead) return; // �����δ���
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX(SoundManager.Instance.lose);
        animator.SetTrigger("Die");
        // ��ֹ��ҿ���
        GetComponent<PlayerMove>().enabled = false;
        // �ý�ɫ��ʧ�����ߴ��������߼���
        Destroy(gameObject, 1f);

        gameOverUI.SetActive(true);

        Time.timeScale = 0f;
        weapon.isDead = true;
    }

    public IEnumerator Invincibility(float duration)
    {
        Invincible = true;
        InvisFx.SetActive(true);
        Debug.Log("�޵з���ִ����");
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
