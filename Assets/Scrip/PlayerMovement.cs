using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public Collider2D standingCollider, crouchingCollider;
    public Transform groundCheckCollider;
    public Transform overheadCheckCollider;
    public LayerMask groundLayer;

    const float groundCheckRadius = 0.2f;
    const float overheadCheckRadius = 0.2f;

    [Header("Move & Jump")]
    [SerializeField] float speed = 2f;
    [SerializeField] float jumpPower = 500f;
    int availableJumps;
    float horizontalValue;
    float runSpeedModifier = 2f;
    float crouchSpeedModifier = 0.5f;
    public int totalJumps;
    bool isGrounded = true;
    bool isRunning;
    bool facingRight = true;
    bool crouchPressed;
    bool multipleJump;
    bool coyoteJump;

    [Header("Bash")]
    [SerializeField] private float radius;
    [SerializeField] GameObject BashAbleObj;
    private bool NearToBashAbleObj;
    private bool IsChosingDir;
    private bool IsBashing;
    [SerializeField] private float BashPower;
    [SerializeField] private float BashTime;
    [SerializeField] private GameObject Arrow;
    Vector3 BashDir;
    private float BashTimeReset;
    private Vector3 originalScale;

    [Header ("Dash")]
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    private bool canDash = true;
    private bool isDashing;


    void Start()
    {

        availableJumps = totalJumps;

        BashTimeReset = BashTime;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontalValue = Input.GetAxisRaw("Horizontal");

        isRunning = Input.GetKeyDown(KeyCode.LeftShift);
        
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        
        crouchPressed = Input.GetButtonDown("Crouch");
        
        if (Input.GetButtonUp("Crouch"))
        {
            crouchPressed = false;
        }

        animator.SetFloat("yVelocity", rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.E) && canDash)
        {
            StartCoroutine(Dash());
        }

        Bash();
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (!IsBashing)
        {
            rb.velocity = new Vector2(horizontalValue * Time.deltaTime, rb.velocity.y);
            GroundCheck();
            Move(horizontalValue, crouchPressed);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckCollider.position, groundCheckRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(overheadCheckCollider.position, overheadCheckRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        
        if (colliders.Length > 0)
        {
            isGrounded = true;
            
            if (!wasGrounded)
            {
                availableJumps = totalJumps;
                multipleJump = false;
            }        
            
            foreach(var c in colliders)
            {
                if (c.tag == "MovingPlatform")
                {
                    transform.parent = c.transform;
                }
            }
        }    
        else
        {
            transform.parent = null;
            
            if (wasGrounded)
            {
                StartCoroutine(CoyoteJumpDelay());
            }
        }
        
        animator.SetBool("Jump", !isGrounded);
    }

    #region Jump
    IEnumerator CoyoteJumpDelay()
    {
        coyoteJump = true;
        yield return new WaitForSeconds(0.2f);
        coyoteJump = false;
    }

    void Jump()
    {
        if (isGrounded)
        {
            multipleJump = true;
            availableJumps--;

            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("Jump", true);
        }
        else
        {
            if(coyoteJump)
            {
                multipleJump = true;
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }

            if(multipleJump && availableJumps > 0)
            {
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
        }
    }
    #endregion

    void Move(float dir,bool crouchFlag)
    {
        #region Crouch

        if(!crouchFlag)
        {
            if(Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundLayer))
            {
                crouchFlag = true;
            }
        }

        animator.SetBool("Crouch", crouchFlag);
        standingCollider.enabled = !crouchFlag;
        crouchingCollider.enabled = crouchFlag;

        #endregion

        #region Move & Run

        float xVal = dir * speed * 100 * Time.fixedDeltaTime;
        
        if (isRunning)
        {
            xVal *= runSpeedModifier;
        }
        
        if (crouchFlag)
        {
            xVal *= crouchSpeedModifier;
        }
        
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        
        rb.velocity = targetVelocity;
 
        if(facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            facingRight = false;
        }
        else if(!facingRight && dir > 0)
        {
            transform.localScale = originalScale;
            facingRight = true;
        }

        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));

        #endregion
    }

    void Bash()
    {
        RaycastHit2D[] Rays = Physics2D.CircleCastAll(transform.position, radius,Vector3.forward);
        foreach(RaycastHit2D ray in Rays)
        {

            NearToBashAbleObj = false;

            if(ray.collider.tag =="BashAble")
            {
                NearToBashAbleObj = true;
                BashAbleObj = ray.collider.transform.gameObject;
                break;
            }
        }
        if(NearToBashAbleObj)
        {
            BashAbleObj.GetComponent<SpriteRenderer>().color = Color.yellow;
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                Time.timeScale = 0;
                BashAbleObj.transform.localScale = new Vector2(1.4f, 1.4f);
                Arrow.SetActive(true);
                Arrow.transform.position = BashAbleObj.transform.transform.position;
                IsChosingDir = true;
            }
            else if(IsChosingDir && Input.GetKeyUp(KeyCode.Mouse1))
            {
                Time.timeScale = 1f;
                BashAbleObj.transform.localScale = new Vector2(1, 1);
                IsChosingDir = false;
                IsBashing = true;
                rb.velocity = Vector2.zero;
                transform.position = BashAbleObj.transform.position;
                BashDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                BashDir.z = 0;
                
                BashDir = BashDir.normalized;
                BashAbleObj.GetComponent<Rigidbody2D>().AddForce(-BashDir * 100, ForceMode2D.Impulse);
                Arrow.SetActive(false);

            }
        }
        else if (BashAbleObj != null)
        {
            BashAbleObj.GetComponent<SpriteRenderer>().color = Color.white;
        }

        ////// Preform the bash
        ///
        if(IsBashing)
        {
            if(BashTime > 0 )
            {
                BashTime -= Time.deltaTime;
                rb.velocity = BashDir * BashPower * 1000 * Time.deltaTime;
            }
            else
            {
                IsBashing = false;
                BashTime = BashTimeReset;
                rb.velocity = new Vector2(rb.velocity.x, 0);


            }
        }
    }

      void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        animator.SetTrigger ("Dash");

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        Vector2 dashVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        rb.velocity = dashVelocity;

        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
