using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    public float maxspeedx;
    public float maxspeedy;
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

        rb.linearVelocity = new Vector2(Math.Clamp(rb.linearVelocityX, -maxspeedx, maxspeedx),Math.Clamp(rb.linearVelocityY, -maxspeedy, maxspeedy));

    }
}
