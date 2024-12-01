using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LevelManager:MonoBehaviour
{
    [SerializeField] InteractableBox MiniGameBox;
    public static Action MiniGameCompleted;
    private Boolean levelCompleted { get; set; } = false;

    InteractableBox GetMiniGameBox()
    {
        return MiniGameBox;
    }

    void start()
    {
        MiniGameBase.onMiniGameEnd += MiniGameEnd;
    }
    
    void MiniGameEnd()
    {
        MiniGameCompleted.Invoke();
        levelCompleted = true; 
        Destroy(MiniGameBox.minigameInstance);
    }

    void OnDestroy()
    {
        MiniGameBase.onMiniGameEnd -= MiniGameEnd;
    }
}