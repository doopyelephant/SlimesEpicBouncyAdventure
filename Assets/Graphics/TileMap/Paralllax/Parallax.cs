using Microsoft.Extensions.Logging.Abstractions;
using UnityEngine;

public class parallax : MonoBehaviour
{
    private float length;

    private float startPos;
    private float startPos2;

    [SerializeField] private float parallaxEffect1;
    [SerializeField] private float parallaxEffect2;
    [SerializeField] private float parallaxEffect3;

    [SerializeField] private GameObject bg1;
    [SerializeField] private GameObject bg2;
    [SerializeField] private GameObject bg3;

    [SerializeField] private GameObject bottom;
    [SerializeField] private GameObject top;

    [SerializeField] private GameObject cam;


    private void Start()
    {
        startPos = transform.position.x;
        startPos2 = transform.position.x;
        length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        bg1.transform.position = new Vector3(startPos + cam.transform.position.x * parallaxEffect1, transform.position.y);
        bg2.transform.position = new Vector3(startPos + cam.transform.position.x * parallaxEffect2, transform.position.y);
        bg3.transform.position = new Vector3(startPos2 + cam.transform.position.x * parallaxEffect3, transform.position.y);

        top.transform.position = new Vector3(startPos + cam.transform.position.x * parallaxEffect2, top.transform.position.y);
        bottom.transform.position = new Vector3(startPos + cam.transform.position.x * parallaxEffect2, bottom.transform.position.y);

        float temp1 = bg1.transform.position.x * (1 - parallaxEffect1);

        if (temp1+7 > startPos + length) startPos -= length;
        else if (temp1-7 < startPos - length) startPos += length;

        float temp2 = bg1.transform.position.x * (1 - parallaxEffect1);

        if (temp2 + 5 > startPos2 + length) startPos2 -= length;
        else if (temp2 - 5 < startPos2 - length) startPos2 += length;
    }
}
