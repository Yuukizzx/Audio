using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    public Slider healthBar;  // 血条
    

    public Slider[] ammoBars; // 三个武器的弹药条
    public Image[] weaponIcons; // 武器图标
    public GameObject[] weaponBorders; // 选中武器的边框

    public HealthSystem player;
    public WeaponManager weaponSystem;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            healthBar.value = player.currentHealth; // 更新血条
        }

        if (weaponSystem != null && weaponSystem.currentWeapon != null)
        {
            UpdateWeaponUI(weaponSystem.currentWeaponIndex); // 更新武器UI

            for (int i = 0; i < ammoBars.Length; i++)
            {
                
                if (i == weaponSystem.currentWeaponIndex)
                {
                    ammoBars[i].value = weaponSystem.currentWeapon.GetCurrentAmmo(); // 更新选中武器的弹药
                }
            }
        }
    }

    void UpdateWeaponUI(int selectedIndex)
    {
        for (int i = 0; i < weaponIcons.Length; i++)
        {
            weaponBorders[i].SetActive(i == selectedIndex); // 选中的武器显示边框
        }
    }
}
