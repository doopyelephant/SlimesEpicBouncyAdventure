using DefaultNamespace;
using UnityEngine;

public class collectOrb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            collectingOrbs(collision);
        }
    }
    private void collectingOrbs(Collider2D collision)
    {
        collision.gameObject.GetComponent<WallBounce>().dashAmount++;
        Destroy(gameObject, 0);
    }
}
