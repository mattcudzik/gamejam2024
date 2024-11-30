using System;
using UnityEngine;
using UnityEngine.SceneManagement;

class GameManager: MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public int TunelHp = 1000;
    private bool IsSceneWorkDone{ get; set; } = false;
    public PlayerEq PlayerEq{get; set;}
    public LevelManager LevelManagerInstance{get; set;}
    [SerializeField] private GameObject UI;
    public Timer Timer  {get; set;}
        
    void Awake()
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        PlayerEq = GetComponentInChildren<PlayerEq>();
        Timer = GetComponentInChildren<Timer>();
    }
    private void OnSceneUnLoaded(Scene scene)
    {
        Debug.Log($"Kończona scena: {scene.name}");
    }
    private void WorkComplete()
    {
        IsSceneWorkDone = true;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name != "StartScene")
        {
            IsSceneWorkDone = false;
            LevelManagerInstance = UnityEngine.Object.FindAnyObjectByType<LevelManager>();
            LevelManager.MiniGameCompleted += WorkComplete;
        }
    }

    public void StartGame()
    {
        UI.gameObject.SetActive(true);
        SceneManager.LoadScene("RollCall");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        LevelManager.MiniGameCompleted -= WorkComplete;
    }
}



