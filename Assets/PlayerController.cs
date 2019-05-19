using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jump_force = 200f;
    public int jump_value = 2;
    public float max_speed = 10f;
    public float time_fromZeroToMax = 1.5f;
    public float time_fromMaxToZero = 0.5f;
    
    private float acce;
    private float decce;
    private float x_dir;
    private float x_vel;
    private bool jump;
    private int jump_times;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    
    // Floor checking variables
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        acce = max_speed / time_fromZeroToMax;
        decce = max_speed / time_fromMaxToZero;
    }


    void FixedUpdate() {
        // Floor checking
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // Horizontal movement
        

        // Flipping
        if (x_dir > 0) {
            sr.flipX = false;
        } else if (x_dir < 0) {
            sr.flipX = true;
        }

        // Jumping
        if (isGrounded == true) {
            jump_times = jump_value;
         }
        if (jump == true && jump_times > 0) {   
            rb.velocity = new Vector2(rb.velocity.x, jump_force * Time.deltaTime);
            jump_times--;
        }
    }

    void Update() {
        jump = Input.GetButtonDown("Jump");
        x_dir = Input.GetAxis("Horizontal");

        if (Mathf.Abs(x_dir) != 0) {
            x_vel += acce * Time.deltaTime * x_dir;
            rb.velocity = new Vector2(x_vel, rb.velocity.y);
        } else {
            x_vel = 0;
            rb.velocity = new Vector2(x_vel, rb.velocity.y);
        }
        anim.SetFloat("Speed", Mathf.Abs(x_dir));
        anim.SetFloat("yVel", rb.velocity.y);
        anim.SetBool("onGround", isGrounded);
    }
}


