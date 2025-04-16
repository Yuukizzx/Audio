using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public int maxAmmo;
    public int currentAmmo;
    public float fireRate;
    public GameObject bulletPrefab;
    public Transform firePoint; // 枪口位置

    protected float lastFireTime;
    protected Animator animator;
    public GameObject IfiniteAmmoEffcet;

    public bool infiniteAmmo = false;

    public AudioClip fireSound;

    public virtual void Start()
    {
        currentAmmo = maxAmmo;
        animator = GetComponent<Animator>();
        infiniteAmmo = false;
    }

    public virtual void Shoot(Vector2 target)
    {

        if (currentAmmo <= 0)
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.noAmmoSound);
            return;
        }
        if (Time.time - lastFireTime < fireRate) return;

        lastFireTime = Time.time;

        if (fireSound != null)
        {
            SoundManager.Instance.PlaySFX(fireSound);
        }

        if (infiniteAmmo)
        {
            currentAmmo++;
            
        }
        
        currentAmmo--;
        animator.SetTrigger("Fire");
        Fire(target);
    }

    protected abstract void Fire(Vector2 target); // 让子类实现不同射击方式

    public void RefillAmmo()
    {
        currentAmmo = maxAmmo;
    }

    

    public IEnumerator InfiniteAmmo(float duration)
    {
        infiniteAmmo = true;
        IfiniteAmmoEffcet.SetActive(true);
        yield return new WaitForSeconds(duration);
        infiniteAmmo = false;
        IfiniteAmmoEffcet.SetActive(false);
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
}
