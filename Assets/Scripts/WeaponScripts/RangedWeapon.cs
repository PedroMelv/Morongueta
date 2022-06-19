using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(fileName = "Ranged Weapon", menuName = "Weapons")]
[System.Serializable]
public class RangedWeapon : Weapon
{
    [Header("Bullet Things")]
    public float fireRate;
    public float bulletSpeed;
    public float destroyBulletTime;

    public BulletTypes bulletType;

    public GameObject bullet;
    public Vector3 shootPos;

    [Header("Target Type Things")]
    public float bulletRotationSpeed;
    public float distanceToTarget;


    [Header("Bounce Type Things")]
    public int bounceTimes;
    public float bounceForce;


    [Header("Cos/Sin Type Things")]
    public float amplitude = 1;
    public float frequency = 1;
}
