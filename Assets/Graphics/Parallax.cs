using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float startPos;

    [SerializeField] private float parallaxEffect1;
    [SerializeField] private float parallaxEffect2;
    [SerializeField] private float parallaxEffect3;

    [SerializeField] private GameObject bg1;
    [SerializeField] private GameObject bg2;
    [SerializeField] private GameObject bg3;

    public GameObject bottom;
    public GameObject top;

    [SerializeField] private GameObject cam;    
    

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    { 
        bg1.transform.position = new Vector3(startPos + cam.transform.position.x * parallaxEffect1, transform.position.y);
        bg2.transform.position = new Vector3(startPos + cam.transform.position.x * parallaxEffect2, transform.position.y);
        bg3.transform.position = new Vector3(startPos + cam.transform.position.x * parallaxEffect3, transform.position.y);
        bottom.transform.position = new Vector3(startPos + cam.transform.position.x * parallaxEffect2, transform.position.y);
        top.transform.position = new Vector3(startPos + cam.transform.position.x * parallaxEffect3, transform.position.y);

        float temp1 = bg1.transform.position.x * (1 - parallaxEffect1);

        if (temp1 > startPos + length) startPos += length;
        else if (temp1 < startPos - length) startPos -= length;
    }
}
