using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class NextLevel : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Player>() != null)
            {
                if (GlobalLevelManager.currentlevel >= GlobalLevelManager.switcher.Levels.Count - 1)
                {
                GlobalLevelManager.Complete();
                }
                else
                {
                    GlobalLevelManager.LoadLevel(GlobalLevelManager.currentlevel + 1);
                }
            }
        }
    }
}