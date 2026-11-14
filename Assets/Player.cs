using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hor =  Input.GetAxisRaw("Horizontal");
        if (hor != 0)
        {
            rb.linearVelocity = new Vector2(hor * speed, rb.linearVelocity.y);
        }

    }
}
