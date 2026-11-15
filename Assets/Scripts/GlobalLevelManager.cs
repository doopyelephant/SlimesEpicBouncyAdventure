using System;
using UnityEngine;

namespace DefaultNamespace
{
    public static class GlobalLevelManager
    {
        private static LevelSwitcher switcher;
        private static bool loaded = false;
        public static int currentlevel = 0;
        public static Vector3 InitPlayer;
        public static Vector3 InitCamera;
        public static void LoadLevel(int level)
        {
            if (loaded)
            {
                currentlevel = level;
                switcher.LoadLevel(level);
            }
        }

        public static void Init(LevelSwitcher _switcher)
        {
            switcher = _switcher;
            loaded = true;
        }
    }
}