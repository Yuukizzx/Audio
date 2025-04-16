using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>();
    public int currentWeaponIndex = 0;
    public Weapon currentWeapon;
    
    private SpriteRenderer weaponSprite;
    public float InfiniteDuration = 6f;

    public bool isDead = false;

    void Start()
    {
        if (weapons.Count > 0)
        {
            EquipWeapon(0);
        }
        isDead = false;
    }

    void Update()
    {
        if (isDead) return;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        HandleWeaponRotation(mousePosition);

        if (Input.GetMouseButtonDown(0) && currentWeapon != null)
        {
            if (isDead) return;
            currentWeapon.Shoot(mousePosition);
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipWeapon(2);
    }

    public void SetInfiniteAmmo()
    {
        currentWeapon.StartCoroutine(currentWeapon.InfiniteAmmo(InfiniteDuration));
    }

    public void RefillCurrentWeaponAmmo()
    {
        if (currentWeapon != null)
        {
            currentWeapon.RefillAmmo();
        }
    }

    void EquipWeapon(int index)
    {
        if (index >= weapons.Count) return;
        SoundManager.Instance.PlaySFX(SoundManager.Instance.switchWeaponSound);
        foreach (Weapon weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }

        currentWeaponIndex = index;
        currentWeapon = weapons[index];
        currentWeapon.gameObject.SetActive(true);
    }

    void HandleWeaponRotation(Vector2 mousePosition)
    {
        Vector2 direction = mousePosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // ·­×ªÇ¹¿Ú
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
