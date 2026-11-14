using UnityEngine;

public class WallBounce : MonoBehaviour
{
    // set colliders of the camera boundaries
    public BoxCollider2D RightWallCollider;
    public BoxCollider2D LeftWallCollider;

    public Camera main;

    bool wallBounceableCollided = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // set main camera
        main = Camera.main;
        // set offset of right collider
        float offsetrightx = RightWallCollider.transform.localScale.x * RightWallCollider.size.x * 0.5f;
        // move positon of collider to the right side of screem
        RightWallCollider.transform.position = main.ScreenToWorldPoint(new Vector3(main.pixelWidth - 1, 0.5f * main.pixelHeight,0f));
        RightWallCollider.transform.Translate(offsetrightx, 0, 0);

        // set offset of left collider
        float offsetleftx = -(LeftWallCollider.transform.localScale.x * LeftWallCollider.size.x) * 0.5f;
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
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bounceable wall"))
        {
            Debug.Log("Collided with wall (Tag: Bounceable wall)");
            wallBounceableCollided = true;
        }
    }
}
