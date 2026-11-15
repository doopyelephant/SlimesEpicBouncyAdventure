using DefaultNamespace;
using UnityEngine;

public class collectChest : MonoBehaviour
{

    private bool collected = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            collectingChest(collision);
        }
    }

    private void collectingChest(Collider2D collision)
    {
        if (collected == false)
        {
            GlobalLevelManager.collectedchests++;
            GetComponent<Animator>().SetTrigger("OpenChest");
            Destroy(gameObject, 2);  
            collected = true; 
        }
    }
}
