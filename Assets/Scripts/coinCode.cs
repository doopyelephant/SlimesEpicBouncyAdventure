using DefaultNamespace;
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
       GlobalLevelManager.collectedcoins++;
        GetComponent<Animator>().SetTrigger("coinCollect");
        Destroy(gameObject, 1);
    }
}
