using System;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public float viewportmove;

    private Vector3 targetviewpos;

    public float viewspeed;
    public float randomstrength;
    private float camoffset;
    public float viewportspeedlag;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetviewpos = main.transform.position;
        camoffset = main.transform.position.y - transform.position.y;
        // set offset of right collider
        offsetrightx = RightWallCollider.transform.localScale.x * RightWallCollider.size.x * 0.5f;
        // set offset of left collider
        offsetleftx = -(LeftWallCollider.transform.localScale.x * LeftWallCollider.size.x) * 0.5f;
        
        
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
        targetviewpos = new  Vector3(targetviewpos.x, ((transform.position.y + camoffset) - main.transform.position.y) * viewportspeedlag * Time.deltaTime + main.transform.position.y, targetviewpos.z);
        main.transform.position = new Vector3(main.transform.position.x, ((transform.position.y + camoffset) - main.transform.position.y) * viewportspeedlag * Time.deltaTime + main.transform.position.y, main.transform.position.z);
        transform.position = new Vector3(Math.Clamp(transform.position.x,LeftWallCollider.transform.position.x - offsetleftx * 2 - 0.1f,RightWallCollider.transform.position.x - offsetrightx * 2 + 0.1f),transform.position.y,transform.position.z);
        if (Vector3.Distance(main.transform.position, targetviewpos) > 1f)
        {
            main.transform.position += Time.deltaTime * viewspeed * (targetviewpos - main.transform.position) +
                                       new Vector3(Random.Range(-randomstrength, randomstrength),
                                           Random.Range(-randomstrength, randomstrength), 0);
        }
        else if (Vector3.Distance(main.transform.position, targetviewpos) > 0.1f)
        {
            main.transform.position += Time.deltaTime * viewspeed * (targetviewpos - main.transform.position);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bounceable wall") && rb.linearVelocity.magnitude > dashthreshold)
        {
            Debug.Log("Collided with wall (Tag: Bounceable wall)");

            targetviewpos += new Vector3(transform.position.x - main.transform.position.x > 0 ? viewportmove : -viewportmove, 0, 0);
            rb.linearVelocityX = -rb.linearVelocityX * dashamount;
            rb.linearVelocityY += yadd;
            rb.linearVelocityY *= ymul;
        }
    }
}
