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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

        rb.linearVelocity = new Vector2(Math.Clamp(rb.linearVelocityX, -maxspeedx, maxspeedx),Math.Clamp(rb.linearVelocityY, -maxspeedy, maxspeedy));
    }
}
