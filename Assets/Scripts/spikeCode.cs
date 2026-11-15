using UnityEngine;

public class spikeCode : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision!" + collision.gameObject);
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            collision.gameObject.GetComponent<Player>().rb.linearVelocity = new Vector2(0, 0);
            collision.gameObject.GetComponent<Player>().StartCoroutine("respawnPlayer");
        }
    }
}
