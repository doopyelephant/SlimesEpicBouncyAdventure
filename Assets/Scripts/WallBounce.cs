using UnityEngine;

public class WallBounce : MonoBehaviour
{
    // set colliders of the camera boundaries
    public BoxCollider2D RightWallCollider;
    public BoxCollider2D LeftWallCollider;

    public Camera main;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        main = Camera.main;
        float offsetrightx = (RightWallCollider.transform.localScale.x * RightWallCollider.size.x) * 0.5f;
        RightWallCollider.transform.position = main.ScreenToWorldPoint(new Vector3(main.pixelWidth - 1, 0.5f * main.pixelHeight,0f));
        RightWallCollider.transform.Translate(offsetrightx, 0, 0);
        float offsetleftx = -(LeftWallCollider.transform.localScale.x * LeftWallCollider.size.x) * 0.5f;
        LeftWallCollider.transform.position = main.ScreenToWorldPoint(new Vector3(0, 0.5f * main.pixelHeight,0f));
        LeftWallCollider.transform.Translate(offsetleftx, 0, 0);
        RightWallCollider.transform.parent = main.transform;
        LeftWallCollider.transform.parent = main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
