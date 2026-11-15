using DefaultNamespace;
using UnityEngine;

public class coinCode : MonoBehaviour
{
    [SerializeField] private AudioClip sound;

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
        GetComponent<AudioSource>().PlayOneShot(sound);
        Destroy(gameObject, 0.5f);
    }
}
