using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    protected override void Fire(Vector2 target)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Initialize(target, 20f);
        bullet.GetComponent<Bullet>().SetPenetrating(true); // ´©Í¸
    }
}
