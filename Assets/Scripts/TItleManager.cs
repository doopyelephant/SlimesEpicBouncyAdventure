using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TItleManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalLevelManager.coins = 0;
        GlobalLevelManager.chests = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
