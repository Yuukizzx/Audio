using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    public Slider healthBar;  // Ѫ��
    

    public Slider[] ammoBars; // ���������ĵ�ҩ��
    public Image[] weaponIcons; // ����ͼ��
    public GameObject[] weaponBorders; // ѡ�������ı߿�

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
            healthBar.value = player.currentHealth; // ����Ѫ��
        }

        if (weaponSystem != null && weaponSystem.currentWeapon != null)
        {
            UpdateWeaponUI(weaponSystem.currentWeaponIndex); // ��������UI

            for (int i = 0; i < ammoBars.Length; i++)
            {
                
                if (i == weaponSystem.currentWeaponIndex)
                {
                    ammoBars[i].value = weaponSystem.currentWeapon.GetCurrentAmmo(); // ����ѡ�������ĵ�ҩ
                }
            }
        }
    }

    void UpdateWeaponUI(int selectedIndex)
    {
        for (int i = 0; i < weaponIcons.Length; i++)
        {
            weaponBorders[i].SetActive(i == selectedIndex); // ѡ�е�������ʾ�߿�
        }
    }
}
