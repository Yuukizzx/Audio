using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    public GameObject rocketPrefab;  // �����Ԥ�Ƽ�
    public GameObject explosionEffectPrefab;  // ��ըЧ��Ԥ�Ƽ�
    public float explosionRadius = 5f;  // ��ը�뾶
    public int explosionDamage = 50;  // ��ը�˺�

    protected override void Fire(Vector2 target)
    {
        GameObject rocket = Instantiate(rocketPrefab, firePoint.position, Quaternion.identity);
        rocket.GetComponent<Rocket>().Initialize(target, 15f, explosionRadius, explosionDamage, explosionEffectPrefab);
    }
}
