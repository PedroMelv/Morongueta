using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Basics")]
    private Weapon curWeapon;
    [SerializeField]private int getID;

    private bool canShoot;
    private float fireRate;

    private Vector3 input;


    private PlayerModuleLink PML;

    void Start()
    {
        PML = PlayerModuleLink.i;
        curWeapon = WeaponDataBase.i.GetWeaponByID(getID);
    }

    void Update()
    {
        HandleWeapon();
    }

    public void HandleInput(Vector3 inputPos)
    {
        input = inputPos;

        if(!canShoot)
            return;
        
        if(curWeapon is RangedWeapon)
            Shoot();
    }

    public void HandleWeapon()
    {
        if (curWeapon == null)
            return;
        if (!canShoot)
        {
            if (fireRate <= 0f)
            {
                canShoot = true;
                fireRate = (curWeapon as RangedWeapon).fireRate;
            }
            else
            {
                fireRate -= Time.deltaTime;
            }
        }

        if (curWeapon is RangedWeapon)
            TargetLine();
    }

    private void Shoot()
    {
        RangedWeapon RW = (curWeapon as RangedWeapon);
        //Debug.Log(RW.name);

        GameObject bullet = RW.bullet;
        Vector3 pos = (RW.shootPos + transform.forward) + transform.position;

        GameObject createdBullet = Instantiate(bullet, pos, Quaternion.identity);

        createdBullet.GetComponent<Bullet>().Create(RW.bulletSpeed, RW.bulletRotationSpeed, RW.damage, RW.bounceTimes, RW.bounceForce, RW.distanceToTarget, RW.destroyBulletTime, RW.amplitude, RW.frequency, RW.bulletType);

        createdBullet.transform.LookAt(new Vector3(input.x, pos.y, input.z));

        canShoot = false;
    }


    private void TargetLine()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = PML.pInput.GetMousePos(transform.position);

        PML.pEffects.DrawTargetLine(startPos, endPos);
    }
}
