using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    private float moveX;

    public bool isTouchingGround;

    [SerializeField] float jumpForce;

    public int coinsCollected = 0;

    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sprite;
    bool isMovingRight;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        //animator.SetBool("name", value)
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isJumping", !isTouchingGround);
        //animator.SetBool("isRunning", MathF.Abs(rb.linearVelocityX) > 0.01f);
        animator.SetBool("isRunning", !Mathf.Approximately(rb.linearVelocityX, 0f));
        if (isMovingRight && moveX < 0)
        {
            isMovingRight = false;
            sprite.flipX = true;
        }
        else if (!isMovingRight && moveX > 0) 
        {
            isMovingRight = true;
            sprite.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = moveX * moveSpeed;
    }

    void OnMove(InputValue value)
    {
        moveX = value.Get<Vector2>().x;
    }

    void OnJump() 
    {
        if (isTouchingGround) 
        {
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground")) 
        {
            isTouchingGround = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isTouchingGround = false;
        }
    }
}
