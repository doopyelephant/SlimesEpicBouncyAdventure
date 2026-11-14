using System.Collections.Generic;
using UnityEngine;

public class MapPiece : MonoBehaviour
{
    public int MapIndex = 0;

    public List<Sprite> sprites;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[MapIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
