using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletTypes
{
    COMMON,
    BOUNCE,
    TARGET,
    SIN,
    COS
}
public enum BulletSpecials
{
    COMMON,
    PIERCE,
    HEAL,
    SPLASH,
    IMORTAL
}
public enum ShootType
{
    COMMON,
    HIGH_SHOOT
}

public class Bullet : MonoBehaviour, IDamager
{
    [SerializeField]private LayerMask hitLayer, groundLayer;

    private BulletTypes    bulletType;
    private BulletSpecials bulletSpecial;
    private ShootType      shootType;

    #region TargetVariables
    private GameObject targetObj;
    private float distanceToTarget;
    private bool targeting;
    #endregion

    #region BounceVariables
    private int bounceTimes;
    private float bounceForce;
    private bool canBounce;
    #endregion

    #region Cos/Sin Variables
    private float amplitude = 10;
    private float frequency = 1;
    #endregion

    private float bulletSpeed, bulletRotationSpeed;
    public float damage { get; set; }

    private float startTime;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    #region Creation/Destruction Bullet
    public void Create(float destroyTime = 10f,BulletTypes bt = BulletTypes.COMMON, BulletSpecials bs = BulletSpecials.COMMON, ShootType st = ShootType.COMMON)
    {
        bulletType = bt;
        bulletSpecial = bs;
        shootType = st;

        startTime = Time.time;

        Invoke("DestroyBul", destroyTime);
    }

    public void Create(float bulletSpeed, float bulletRotationSpeed, float damage, int bounceTimes = 3, float bounceForce = 30f, float distanceToTarget = 100f, float destroyTime = 10f, float amplitude = 1f, float frequency = 1f, BulletTypes bt = BulletTypes.COMMON, BulletSpecials bs = BulletSpecials.COMMON, ShootType st = ShootType.COMMON)
    {
        this.bulletSpeed = bulletSpeed;
        this.distanceToTarget = distanceToTarget;
        this.bulletRotationSpeed = bulletRotationSpeed;
        this.damage = damage;

        this.bounceTimes = bounceTimes;
        this.bounceForce = bounceForce;

        this.amplitude = amplitude;
        this.frequency = frequency;

        Create(destroyTime,bt,bs,st);
    }
    

    public void DestroyBul()
    {
        Destroy(this.gameObject);
    }
    #endregion

    void Update()
    {
        ManageMovement();
        if(bulletType == BulletTypes.TARGET)    CheckTarget();
        CheckHit();
    }

    void ManageMovement()
    {
        //Debug.Log(Mathf.Sin(Time.time));
        switch (bulletType)
        {
            case BulletTypes.COMMON:
                rb.velocity = transform.forward * (bulletSpeed);
                break;
            case BulletTypes.TARGET:
                if(targetObj != null)
                {
                    targeting = (Vector3.Distance(targetObj.transform.position, transform.position) < distanceToTarget);
                }

                if (targeting)
                {
                    if (targetObj == null)
                        return;
                    Vector3 direction = (Vector3)targetObj.transform.position - rb.position;
                    direction.Normalize();

                    Vector3 rotateAmount = Vector3.Cross(direction, transform.forward);

                    rb.angularVelocity = -rotateAmount * bulletRotationSpeed;
                    rb.velocity = transform.forward * bulletSpeed;
                }
                else
                {
                    rb.velocity = transform.forward * bulletSpeed;
                }
                break;
            case BulletTypes.BOUNCE:
                rb.useGravity = true;
                rb.velocity = new Vector3(transform.forward.x * bulletSpeed, rb.velocity.y, transform.forward.z * bulletSpeed);
                if (rb.velocity.y <= 0f)
                    canBounce = true;
                if (IsGrounded())
                {
                    if(bounceTimes <= 0)
                    {
                        DestroyBul();
                    }

                    if (canBounce)
                    {
                        rb.velocity = Vector3.zero;
                        rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
                        bounceForce *= .90f;
                        bounceTimes--;
                        canBounce = false;
                    }
                }
                break;
            case BulletTypes.SIN:
                rb.velocity = transform.forward * (bulletSpeed);
                rb.velocity = new Vector3(rb.velocity.x, Mathf.Sin((startTime + Time.time) * frequency) * amplitude, rb.velocity.z) ;
                break;
            case BulletTypes.COS:
                rb.velocity = transform.forward * (bulletSpeed);
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z) + new Vector3(Mathf.Cos((startTime + Time.time) * frequency) * amplitude,0f,Mathf.Cos((startTime + Time.time) * frequency) * amplitude) ;
                break;
            default:
                break;
        }
    }

    private void CheckTarget()
    {
        if(targeting)
            return;

        Collider[] cols = Physics.OverlapSphere(transform.position, distanceToTarget, hitLayer);
        bool b = (cols.Length > 0);
        Debug.Log("Targeting " + b);
        if(b)
        {
            Debug.Log("Hit " + cols[0].gameObject.name);
            targetObj = cols[0].gameObject;
            //targeting = true;
        }
    }

    private void CheckHit()
    {
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1f, hitLayer);
        //Debug.DrawRay(transform.position, transform.forward, Color.red, 3f);
        if(hit.collider != null)
        {
            IDamagable d = hit.collider.gameObject.GetComponent<IDamagable>();
            if(d != null)
            {
                d.TakeDamage(damage, out bool killed);
                if(!killed)
                    DestroyBul();
            }
            
        }
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, .5f, groundLayer);
    }

}
