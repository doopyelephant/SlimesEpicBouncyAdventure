using DefaultNamespace;
using UnityEngine;

public class collectChest : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            collectingChest(collision);
        }
    }

    private void collectingChest(Collision2D collision)
    {
        GlobalLevelManager.collectedchests++;
        GetComponent<Animator>().SetTrigger("OpenChest");
        Destroy(gameObject, 2);
    }
}
