using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 100f;
    public float jump_force = 200f;
    public int jump_times = 1;
    
    private float xMove;
    private bool jump;
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

        anim.SetFloat("Speed", Mathf.Abs(xMove));
        anim.SetFloat("yVel", rb.velocity.y);
        anim.SetBool("onGround", isGrounded);

        rb.velocity = new Vector2(xMove * speed * Time.deltaTime, rb.velocity.y);

        // Flip
        if (xMove > 0) {
            sr.flipX = false;
        } else if (xMove < 0) {
            sr.flipX = true;
        }
    }

    void Update() {
        jump = Input.GetButtonDown("Jump");
        if (isGrounded == true) {
            jump_times = 1;
        }
        
        if (jump == true && jump_times > 0) {
            rb.velocity = Vector2.up * jump_force * Time.deltaTime;
            jump_times--;
        }
        Debug.Log(jump_times);
        Debug.Log(jump);
    }
}


