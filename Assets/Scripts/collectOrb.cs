using DefaultNamespace;
using UnityEngine;

public class collectOrb : MonoBehaviour
{
    public AudioClip clip;

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
        Destroy(GetComponent<CircleCollider2D>(), 0);
        GetComponent<AudioSource>().PlayOneShot(clip);
        GetComponent<Animator>().SetTrigger("OrbCollected");
        Destroy(gameObject, 0.55f);
    }
}
