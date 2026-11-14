using System;
using UnityEngine;

public class WallBounce : MonoBehaviour
{
    // set colliders of the camera boundaries
    public BoxCollider2D RightWallCollider;
    public BoxCollider2D LeftWallCollider;

    public Camera main;
    public Rigidbody2D rb;
    public float dashthreshold = 3f;
    public float dashamount = 2f;
    float offsetrightx; 
    float offsetleftx;
    public float ymul;
    public float yadd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // set offset of right collider
        offsetrightx = RightWallCollider.transform.localScale.x * RightWallCollider.size.x * 0.5f;
        // set offset of left collider
        offsetleftx = -(LeftWallCollider.transform.localScale.x * LeftWallCollider.size.x) * 0.5f;
        
        // set main camera
        main = Camera.main;
        
        
        // move positon of collider to the right side of screem
        RightWallCollider.transform.position = main.ScreenToWorldPoint(new Vector3(main.pixelWidth - 1, 0.5f * main.pixelHeight,0f));
        RightWallCollider.transform.Translate(offsetrightx, 0, 0);

        // move position of collider to the left side of screen
        LeftWallCollider.transform.position = main.ScreenToWorldPoint(new Vector3(0, 0.5f * main.pixelHeight,0f));
        LeftWallCollider.transform.Translate(offsetleftx, 0, 0);

        //set collider to have camera as parent
        RightWallCollider.transform.parent = main.transform;
        LeftWallCollider.transform.parent = main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Math.Clamp(transform.position.x,LeftWallCollider.transform.position.x - offsetleftx * 2 - 0.1f,RightWallCollider.transform.position.x - offsetrightx * 2 + 0.1f),transform.position.y,transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bounceable wall") && rb.linearVelocity.magnitude > dashthreshold)
        {
            Debug.Log("Collided with wall (Tag: Bounceable wall)");
            rb.linearVelocityX = -rb.linearVelocityX * dashamount;
            rb.linearVelocityY += yadd;
            rb.linearVelocityY = rb.linearVelocityY * ymul;
        }
    }
}
