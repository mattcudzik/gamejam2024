using UnityEngine;

public class Fps : MonoBehaviour
{
    public int target = 60;
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
    }
}
