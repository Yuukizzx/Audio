using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashDistance = 2f;
    public float dashSpeed = 20f;
    public float dashCooldown = 1f;

    public HealthSystem Health;
    public float effectDuration = 8f;


    private float defaultSpeed;
    public GameObject Speedfx;

    public WeaponManager weaponSystem;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isDashing = false;
    private float nextDashTime = 0f;
    private Vector2 moveInput;

    public bool isDead = false;



    void Start()
    {
        defaultSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (isDead) return;
        if (!isDashing) 
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();
        }

        
        animator.SetFloat("Speed", moveInput.magnitude);

        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = (mouseWorldPos - transform.position).normalized;

        
        spriteRenderer.flipX = directionToMouse.x < 0;

        
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextDashTime)
        {
            Dash();
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity = moveInput * moveSpeed;
        }
    }

    void Dash()
    {
        if (moveInput == Vector2.zero) return;
        if (isDead) return;
        SoundManager.Instance.PlaySFX(SoundManager.Instance.playerDashSound);
        nextDashTime = Time.time + dashCooldown;
        StartCoroutine(DashRoutine(moveInput));
    }

    public void TakeDamage(int damage)
    {
        
    }

    IEnumerator DashRoutine(Vector2 direction)
    {
        isDashing = true;
        animator.SetBool("IsDashing", true);

        // **�ڳ��ʱҲʹ����귽����з�ת**
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = (mouseWorldPos - transform.position).normalized;
        spriteRenderer.flipX = directionToMouse.x < 0;

        float dashDuration = dashDistance / dashSpeed;
        float elapsedTime = 0f;
        Vector2 startPos = rb.position;
        Vector2 targetPos = startPos + direction * dashDistance;

        while (elapsedTime < dashDuration)
        {
            rb.MovePosition(Vector2.Lerp(startPos, targetPos, elapsedTime / dashDuration));
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate(); // ʹ�� FixedUpdate �����������
        }

        isDashing = false;
        animator.SetBool("IsDashing", false);
    }

    public void RefillAmmo()
    {
        if (weaponSystem != null)
        {
            weaponSystem.RefillCurrentWeaponAmmo();
        }
    }

    // ========== �� �����ƶ��ٶ� ==========
    public IEnumerator SpeedBoost(float duration)
    {
        moveSpeed *= 1.5f; // ���� 50% ����
        Speedfx.SetActive(true);
        yield return new WaitForSeconds(duration);
        moveSpeed = defaultSpeed;
        Speedfx.SetActive(false);
    }

    // ========== �� ���޵�ҩ ==========
    public void InfiniteA()
    {
        
        weaponSystem.SetInfiniteAmmo();
        
    }
    public void Healing()
    {
        Health.Heal();
    }
    public void Invin()
    {
        Health.StartCoroutine(Health.Invincibility(effectDuration));
    }
    
}
