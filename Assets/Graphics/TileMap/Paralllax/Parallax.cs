using Microsoft.Extensions.Logging.Abstractions;
using UnityEngine;

public class parallax : MonoBehaviour
{
    [SerializeField] private float parallaxEffect1;
    [SerializeField] private float parallaxEffect2;
    [SerializeField] private float parallaxEffect3;

    [SerializeField] private Transform bg1;
    [SerializeField] private Transform bg2;
    [SerializeField] private Transform bg3;
    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;
    public Transform cam;

    [SerializeField] private float wrapMargin1 = 10f;
    [SerializeField] private float wrapMargin2 = 7f;

    private Transform selfTransform;
    private float length;
    private float startPos1;
    private float startPos2;

    private void Awake()
    {
        selfTransform = transform;
    }

    private void Start()
    {
        startPos1 = startPos2 = selfTransform.position.x;
        length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    // LateUpdate: runs after the camera has moved this frame -> no jitter
    private void LateUpdate()
    {
        if (cam != null)
        {
            float camX = cam.position.x;
            float y = selfTransform.position.y;

            SetX(bg1, startPos1 + camX * parallaxEffect1, y);
            SetX(bg2, startPos1 + camX * parallaxEffect2, y);
            SetX(bg3, startPos2 + camX * parallaxEffect3, y);

            float x2 = startPos1 + camX * parallaxEffect2;
            SetX(top, x2, top.position.y);
            SetX(bottom, x2, bottom.position.y);

            // Layer group 1 (bg1, bg2, top, bottom)
            float temp1 = camX * (1f - parallaxEffect1);
            if (temp1 + wrapMargin1 > startPos1 + length) startPos1 += length;
            else if (temp1 - wrapMargin1 < startPos1 - length) startPos1 -= length;

            // Layer 3 (bg3), uses its own effect value
            float temp2 = camX * (1f - parallaxEffect3);
            if (temp2 + wrapMargin2 > startPos2 + length) startPos2 += length;
            else if (temp2 - wrapMargin2 < startPos2 - length) startPos2 -= length;
        }
    }

    private static void SetX(Transform t, float x, float y)
    {
        Vector3 p = t.position;
        p.x = x;
        p.y = y;
        t.position = p; // keeps Z intact
    }
}
