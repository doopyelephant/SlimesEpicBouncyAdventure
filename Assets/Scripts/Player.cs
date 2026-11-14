using System;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float dashSpeed;

    public float maxspeedx;
    public float maxspeedy;

    public bool facingLeft;

    public float jump;

    public float isgrounddistance;
    public float secondssinceground;
    private float timeonground = 0f;
    private float sincejump = 0f;

    public float jumpcooldown;

    private int layermask;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layermask = ~(1 << LayerMask.NameToLayer("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Xvelocity", rb.linearVelocity.x);
        timeonground += Time.deltaTime;
        sincejump += Time.deltaTime;
        float hor =  Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        
        if (hor != 0)
        {
            rb.AddForce(new Vector2(hor, 0), ForceMode2D.Impulse);
        }
        if (hor > 0)
        {
            facingLeft = false;
        }
        else if (hor < 0)
        {
            facingLeft = true;
        }
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

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.down, isgrounddistance, layermask);
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
        rb.linearVelocity = new Vector2(Math.Clamp(rb.linearVelocityX, -maxspeedx, maxspeedx),Math.Clamp(rb.linearVelocityY, -maxspeedy, maxspeedy));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * isgrounddistance);
    }
}
