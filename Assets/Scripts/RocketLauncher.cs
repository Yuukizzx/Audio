using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    public GameObject rocketPrefab;  // »ð¼ýµ¯Ô¤ÖÆ¼þ
    public GameObject explosionEffectPrefab;  // ±¬Õ¨Ð§¹ûÔ¤ÖÆ¼þ
    public float explosionRadius = 5f;  // ±¬Õ¨°ë¾¶
    public int explosionDamage = 50;  // ±¬Õ¨ÉËº¦

    protected override void Fire(Vector2 target)
    {
        GameObject rocket = Instantiate(rocketPrefab, firePoint.position, Quaternion.identity);
        rocket.GetComponent<Rocket>().Initialize(target, 15f, explosionRadius, explosionDamage, explosionEffectPrefab);
    }
}
