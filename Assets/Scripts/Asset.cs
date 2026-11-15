using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void SetTargetFrameRate()
    {
        // Disable VSync so Application.targetFrameRate is not ignored
        QualitySettings.vSyncCount = 0;

        // Set your desired FPS cap (use -1 for unlimited)
        Application.targetFrameRate = 60;
    }
}
