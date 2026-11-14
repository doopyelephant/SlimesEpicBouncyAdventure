using System;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Components
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Movement Details")]
    public float speed;
    public float dashSpeed;

    public float maxspeedx;
    public float maxspeedy;

    public bool facingLeft;

    public float jump;

    [Header("Jump Details")]
    public float isgrounddistance;
    public float secondssinceground;
    private float timeonground = 0f;
    private float sincejump = 0f;

    private bool isGrounded = true;

    public float jumpcooldown;

    private int layermask;
    private int mapamount = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layermask = ~(1 << LayerMask.NameToLayer("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.flipX = facingLeft;

        HandleAnimation();
        HandleMovement();
        HandleDash();
        HandleJump();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent(typeof(MapPiece)) != null)
        {
            Destroy(other.gameObject);
            mapamount += 1;
        }
    }

    private void HandleMovement()
    {
        float hor = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;

        if (hor != 0) rb.AddForce(new Vector2(hor, 0), ForceMode2D.Impulse);
        if (hor > 0) facingLeft = false;
        else if (hor < 0) facingLeft = true;
    }

    private void HandleAnimation()
    {
        animator.SetFloat("Xvelocity", rb.linearVelocity.x);
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetBool("IsGrounded", isGrounded);
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (facingLeft)
            {
                Debug.Log("dash inputted");
                rb.AddForce(new Vector2(-dashSpeed, 0), ForceMode2D.Impulse);
            }
            else if (facingLeft == false)
            {
                Debug.Log("dash inputted");
                rb.AddForce(new Vector2(dashSpeed, 0), ForceMode2D.Impulse);
            }
        }
    }

    private void HandleJump()
    {
        timeonground += Time.deltaTime;
        sincejump += Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, isgrounddistance, layermask);
        isGrounded = hit;

        if (hit.collider != null)
        {
            timeonground = 0f;
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.Log(Vector3.Distance(transform.position, hit.point));
        }

        if (Input.GetButtonDown("Jump") && timeonground < secondssinceground && sincejump > jumpcooldown)
        {
            sincejump = 0f;
            rb.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
        }
        rb.linearVelocity = new Vector2(Math.Clamp(rb.linearVelocityX, -maxspeedx, maxspeedx), Math.Clamp(rb.linearVelocityY, -maxspeedy, maxspeedy));
    }


    // Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * isgrounddistance);
    }
}
