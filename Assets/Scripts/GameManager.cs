using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject options;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            options.SetActive(!options.activeSelf);
        }

    }

    public void ToggleOptions()
    {
        options.SetActive(!options.activeSelf);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
