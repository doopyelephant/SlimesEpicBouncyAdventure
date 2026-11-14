using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    public float maxspeedx;
    public float maxspeedy;

    public float jump;

    public float isgrounddistance;
    public float secondssinceground;
    private float timeonground = 0f;
    private float sincejump = 0f;

    public float jumpcooldown;

    private int layermask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layermask = ~(1 << LayerMask.NameToLayer("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        timeonground += Time.deltaTime;
        sincejump += Time.deltaTime;
        float hor =  Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        if (hor != 0)
        {
            rb.AddForce(new Vector2(hor, 0), ForceMode2D.Impulse);
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
