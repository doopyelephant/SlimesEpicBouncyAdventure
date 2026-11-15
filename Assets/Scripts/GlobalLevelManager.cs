using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public static class GlobalLevelManager
    {
        private static LevelSwitcher switcher;
        private static bool loaded = false;
        public static int currentlevel = 0;
        public static Vector3 InitPlayer;
        public static Vector3 InitCamera;
        private static int _chests = 0;
        private static int _coins = 0;
        private static int _collectedcoins = 0;
        private static int _collectedchests = 0;
        public static int collectedchests
        {
            get { return _collectedchests; }
            set
            {
               _collectedchests = value;
                switcher.chests.text = chests.ToString();
            }
        }
        public static int collectedcoins
        {
            get { return _collectedcoins; }
            set
            {
                _collectedcoins = value;
                switcher.coins.text = coins.ToString();
            }
        }
        public static int chests {
            get { return _chests + collectedchests; }
            set
            {
                _chests = value;
                switcher.chests.text = _chests.ToString();
            }
        }
        public static int coins {
            get { return _coins + collectedcoins; }
            set
            {
                    _coins = value;
                    switcher.coins.text = _coins.ToString();
            }
        }
        public static void LoadLevel(int level)
        {
            if (loaded)
            {
                currentlevel = level;
                switcher.LoadLevel(level);
                _chests += collectedchests;
                collectedchests = 0;
                _coins += collectedcoins;
                collectedcoins = 0;
            }
        }

        public static void Init(LevelSwitcher _switcher)
        {
            switcher = _switcher;
            loaded = true;
            currentlevel = switcher.Levels[0];
            switcher.chests.text = chests.ToString();
            switcher.coins.text = coins.ToString();
        }

        public static void ReloadLevel()
        {
            collectedchests = 0;
            collectedcoins = 0;
        switcher.ReloadLevel();
        }
    }
}