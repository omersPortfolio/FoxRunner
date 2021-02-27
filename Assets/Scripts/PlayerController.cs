using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public float jumpForce;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    public float bounceForce;

    public Animator animator;
    public Rigidbody2D rb;
    bool isGrounded = true;
    bool canDoubleJump;

    public bool stopInput;

    public SpriteRenderer sr;

    public float knockBackLength, knockBackForce;
    private float knockBackCounter;

    public bool didJump;

    public bool isOnPlatform;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    { 

        if (!PauseMenu.instance.isPaused && !stopInput)
        {
            if (knockBackCounter <= 0)
            {
                rb.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rb.velocity.y);

                isGrounded = Physics2D.OverlapCircle(groundCheck.position, .2f, whatIsGround);

                if (isGrounded)
                    canDoubleJump = true;

                if (Input.GetButtonDown("Jump"))
                {
                    if (isGrounded)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                        AudioManager.instance.PlaySFX(10);
                        didJump = true;
                    }
                    else
                    {
                        if (canDoubleJump && didJump)
                        {
                            animator.SetTrigger("doubleJump");
                            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                            canDoubleJump = false;
                            AudioManager.instance.PlaySFX(12);
                            didJump = false;
                        }
                    }
                }

                if (rb.velocity.x < 0)
                {
                    sr.flipX = true;
                }
                else if (rb.velocity.x > 0)
                {
                    sr.flipX = false;
                }

            }
            else
            {
                knockBackCounter -= Time.deltaTime;

                if (!sr.flipX)
                {
                    rb.velocity = new Vector2(-knockBackForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(knockBackForce, rb.velocity.y);
                }
            }
        }
        
        animator.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("isGrounded", isGrounded);
    }

    public void KnockBack()
    {
        AudioManager.instance.PlaySFX(9);
        knockBackCounter = knockBackLength;
        rb.velocity = new Vector2(0f, knockBackForce);

        animator.SetTrigger("hurt");
    }

    public void Bounce()
    {
        
        animator.SetTrigger("doubleJump");
        rb.velocity = new Vector2(rb.velocity.x, bounceForce);
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }

        if (other.gameObject.tag == "Platform2")
        {
            transform.parent = other.transform;
            isOnPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }

        if (other.gameObject.tag == "Platform2")
        {
            transform.parent = null;
            isOnPlatform = false;
        }
    }
}
