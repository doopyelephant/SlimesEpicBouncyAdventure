using UnityEngine;

public class coinCode : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            collectCoin(collision);
        }
    }

    private void collectCoin(Collider2D collision)
    {
        collision.gameObject.GetComponent<Player>().coinAmount += 1;
        GetComponent<Animator>().SetTrigger("coinCollect");
        Destroy(gameObject, 1);
    }
}
