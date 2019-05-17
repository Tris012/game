using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 100f;
    public float jumpForce = 200f;
    
    private float xMove;
    private float jump;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    
    // Floor checking
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        xMove = Input.GetAxisRaw("Horizontal");
        jump = Input.GetAxisRaw("Jump");

        anim.SetFloat("Speed", Mathf.Abs(xMove));
        anim.SetFloat("yVel", rb.velocity.y);

        rb.velocity = new Vector2(xMove * speed * Time.deltaTime, rb.velocity.y);
 
        if (xMove > 0) {
            sr.flipX = false;
        } else if (xMove < 0) {
            sr.flipX = true;
        }
    }

    void Update() {
        if (isGrounded == true) {
            rb.velocity = new Vector2(rb.velocity.x, jump * jumpForce * Time.deltaTime);
            anim.SetBool("onGround", true);
        } else {
            anim.SetBool("onGround", false);
        }
    }
}


