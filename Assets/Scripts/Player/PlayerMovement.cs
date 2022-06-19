using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move Variables")]
    [SerializeField] private float movementSpeed;

    private Vector3 input;
    private Vector3 moveVelocity;

    private bool canMove = true;
    
    [Space]
    
    [Header("Dash Variables")]
    [SerializeField] private int dashMaxCount;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;
    [SerializeField, Range(0.0f,1.0f)] private float minimumDashTime;

    private int curDash;

    [Header("Dash Recover")]
    [SerializeField ]private float timeToRecoverDash;

    private float timeRecoverDash;
    

    private bool isDashing, dashReleased;


    [Header("Components")]
    private PlayerModuleLink PML;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        PML = PlayerModuleLink.i;

        curDash = dashMaxCount;
        timeRecoverDash = timeToRecoverDash;
    }

    void Update()
    {
        HandleMovement();
        HandleDash();
    }

    void FixedUpdate()
    {
        if(canMove)
        {
            if(!isDashing)
                rb.velocity = moveVelocity;
        }
    }

    #region General System
    
    public void SetInput(Vector2 input)
    {
        this.input = Vector2.ClampMagnitude(input,1f);
        //Debug.Log(this.input);
    }

    public Vector3 GetInput(bool relativeToCamera = false)
    {
        Vector3 newInput = input;

        if(relativeToCamera)
        {
            Vector3 camF = Camera.main.transform.forward;
            Vector3 camR = Camera.main.transform.right;
            camF.y = 0f;
            camR.y = 0f;
            camF.Normalize();
            camR.Normalize();

            newInput = (camF * input.y + camR * input.x);
        } 

        return newInput;
    }

    #endregion

    #region Movement System

    public void HandleMovement()
    {
        if(!canMove)
            return;

        Vector3 moveDir = new Vector3(input.x,0f,input.y);
        Vector3 getInput = GetInput(true);
        
        moveVelocity =  getInput * movementSpeed;
    }

    #endregion


    #region Dash System

    private void HandleDash()
    {
        if(curDash < dashMaxCount)
        {
            if(timeRecoverDash <= 0f)
            {
                curDash++;
                timeRecoverDash = timeToRecoverDash;
            }else{
                timeRecoverDash -= Time.deltaTime;
            }
        }
    }

    public void ExecuteDash()
    {
        if(isDashing || curDash <= 0)
            return;
    
        StartCoroutine(EDash());
    }

    public void ReleaseDash()
    {
        if(!isDashing)
            return;

        dashReleased = true;
    }

    private void StopDash()
    {
        isDashing = false;
        rb.velocity = Vector3.zero;
    }

    private IEnumerator EDash()
    {
        float startTime = Time.time;
        Vector3 getInput = GetInput(true);
        
        isDashing = true;
        dashReleased = false;
        
        curDash--;

        rb.AddForce(getInput * dashForce, ForceMode.Impulse);

        while (Time.time < startTime + dashTime)
        {
            if(dashReleased && (Time.time > startTime + (dashTime * minimumDashTime)))
            {
                StopDash();
                yield break;
            } 

            yield return null;
        }

        isDashing = false;
    }

    #endregion
}
