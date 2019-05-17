using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 100f;
    public float jump_force = 200f;
    public int jump_value = 2;
    
    private float xMove;
    private bool jump;
    private int jump_times;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    
    // Floor checking vars
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
        // Floor checking
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);


        anim.SetFloat("Speed", Mathf.Abs(xMove));
        anim.SetFloat("yVel", rb.velocity.y);
        anim.SetBool("onGround", isGrounded);

        // Horizontal movement
        xMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xMove * speed * Time.deltaTime, rb.velocity.y);

        // Flipping
        if (xMove > 0) {
            sr.flipX = false;
        } else if (xMove < 0) {
            sr.flipX = true;
        }

        // Jumping
        if (isGrounded == true) {
            jump_times = jump_value;
        }
        if (jump == true && jump_times > 0) {   
            rb.velocity = Vector2.up * jump_force * Time.deltaTime;
            jump_times--;
        }
    }

    void Update() {
        jump = Input.GetButtonDown("Jump");
    }
}


