using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class SetStat : MonoBehaviour
{
    [Header("0-Coins, 1-Chests, 2-Runtime")]
    [SerializeField]

    private int Type;

    private TMP_Text text;

    public void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        switch (Type)
        {
            case 0:
                text.text = GlobalLevelManager.coins.ToString();
                break;
            case 1:
                text.text = GlobalLevelManager.chests.ToString();
                break;
            case 2:
                text.text = $"{Math.Floor(GlobalLevelManager.time / (60*60)).ToString()}:{Math.Floor((GlobalLevelManager.time % (60*60)) /(60)).ToString()}:{Math.Floor(GlobalLevelManager.time % (60)).ToString()}";
                break;
        }
    }
}
