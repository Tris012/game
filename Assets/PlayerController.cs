using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jump_force = 200f;
    public int jump_value = 2;
    public float max_speed = 7f;
    public float time_fromZeroToMax = 1f;

    private float acce;
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

    // Melee variables
    public Transform meleePos;
    public float meleeRadius;
    public LayerMask whatIsEnemies;
    public float damage = 1;
    private float timeBtwnAttack;
    public float start_timeBtwnAttack = 0.3f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        acce = max_speed / time_fromZeroToMax;
        timeBtwnAttack = start_timeBtwnAttack;
    }


    void FixedUpdate() {
        // Floor checking
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // Horizontal accelerating movement
        if (Mathf.Abs(x_dir) != 0) {
            x_vel += acce * Time.deltaTime;
            x_vel = Mathf.Min (x_vel, max_speed);
            rb.velocity = new Vector2(x_vel * x_dir, rb.velocity.y);
        } 

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
            jump = false;   
            rb.velocity = new Vector2(rb.velocity.x, jump_force * Time.deltaTime);
            jump_times--;
        }
    }

    void Update() {
        x_dir = Input.GetAxis("Horizontal");

        // Check jump press
        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }
        if (Input.GetButtonUp("Jump")) {
            jump = false;
        }

        // Check if player just presses opposite button to set x_vel to 0
        if (Mathf.Abs(x_dir) == 0) {
            x_vel = 0;
            rb.velocity = new Vector2(x_vel, rb.velocity.y);
        }        

        anim.SetFloat("Speed", Mathf.Abs(x_dir));
        anim.SetFloat("yVel", rb.velocity.y);
        anim.SetBool("onGround", isGrounded);

        // Melee radius
        if (timeBtwnAttack < 0) {
            if (Input.GetButtonDown("Fire1")) {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(meleePos.position, meleeRadius, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                enemiesToDamage[i].GetComponent<Enemy>().takeDamage(damage);
                }
            timeBtwnAttack = start_timeBtwnAttack;
            }
        } else {
            timeBtwnAttack -= Time.deltaTime;
        }
    }

    // Draw melee radius on scene
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleePos.position, meleeRadius);
    }
}
