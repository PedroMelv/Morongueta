using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Basics")]
    private Weapon curWeapon;
    [SerializeField]private int getID;

    private bool canAttack;

    private bool combo = false;

    private bool attacking;

    
    private float fireRate;
    private float cooldown;
    private float perHitCooldown;
    private float minHitCooldown;

    private GameObject attackBox;

    private int comboHit;

    private Vector3 input;


    private PlayerModuleLink PML;

    void Start()
    {
        PML = PlayerModuleLink.i;
        curWeapon = WeaponDataBase.i.GetWeaponByID(getID);

        if (curWeapon is MeeleWeapon)
        {
            attackBox = Instantiate((curWeapon as MeeleWeapon).attackBoxes, transform);
            perHitCooldown = (curWeapon as MeeleWeapon).totalCooldown;
            minHitCooldown = perHitCooldown / 2f;
        }
    }

    void Update()
    {
        HandleWeapon();
    }

    #region Input Area

    public void HandleInput(Vector3 inputPos)
    {
        input = inputPos;

        Attack();
    }

    public Weapon CurrentWeapon()
    {
        return curWeapon;
    }

    #endregion

    public void Attack()
    {
        if (curWeapon is RangedWeapon)
        {
            if (canAttack)
                Shoot();
        }
        else
            Hit();
    }

    public void HandleWeapon()
    {
        if (curWeapon == null)
            return;

        if(curWeapon is MeeleWeapon)
        {
            if(comboHit >= (curWeapon as MeeleWeapon).maxComboHits)
            {
                combo = false;
            }

            if (!canAttack)
            {
                if (cooldown <= 0f)
                {
                    canAttack = true;
                    cooldown = (curWeapon as MeeleWeapon).totalCooldown;
                }
                else
                {
                    cooldown -= Time.deltaTime;
                }
            }
        }
        else
        {
            TargetLine();

            if (!canAttack)
            {
                if (fireRate <= 0f)
                {
                    canAttack = true;
                    fireRate = (curWeapon as RangedWeapon).fireRate;
                }
                else
                {
                    fireRate -= Time.deltaTime;
                }
            }
        }

    }

    #region RangedArea

    private void Shoot()
    {
        RangedWeapon RW = (curWeapon as RangedWeapon);
        //Debug.Log(RW.name);

        GameObject bullet = RW.bullet;
        Vector3 pos = (RW.shootPos + transform.forward) + transform.position;

        GameObject createdBullet = Instantiate(bullet, pos, Quaternion.identity);

        createdBullet.GetComponent<Bullet>().Create(RW.bulletSpeed, RW.bulletRotationSpeed, RW.damage, RW.bounceTimes, RW.bounceForce, RW.distanceToTarget, RW.destroyBulletTime, RW.amplitude, RW.frequency, RW.bulletType);

        createdBullet.transform.LookAt(new Vector3(input.x, pos.y, input.z));

        canAttack = false;
    }


    private void TargetLine()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = PML.pInput.GetMousePos(transform.position);

        PML.pEffects.DrawTargetLine(startPos, endPos);
    }

    #endregion


    #region MeeleArea
    public void Hit()
    {
        if (attacking)
            return;
        if(!canAttack)
        {
            if (combo)
            {
                comboHit++;
                StartCoroutine(EMeeleAttack());
            }
                
        }
        else
        {
            comboHit = 1;
            canAttack = false;
            combo = true;
            StartCoroutine(EMeeleAttack());
        }
        
    }

    public IEnumerator EMeeleAttack()
    {
        attacking = true;
        attackBox.GetComponent<Damager>().Reset(curWeapon.damage);
        attackBox.SetActive(true);
        yield return new WaitForSeconds(.05f);
        attackBox.SetActive(false);
        attacking = false;
    }
    #endregion
}
