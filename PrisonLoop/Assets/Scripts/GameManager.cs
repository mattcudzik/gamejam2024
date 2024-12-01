using System;
using UnityEngine;
using UnityEngine.SceneManagement;

class GameManager: MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public int[] TunelHp;
    public bool IsSceneWorkDone{ get; set; } = false;
    public PlayerEq PlayerEq{get; set;}
    public LevelManager LevelManagerInstance{get; set;}
    [SerializeField] private GameObject UI;
    public Timer Timer  {get; set;}
        
    void Awake()
    {
        TunelHp=new int[3];
        TunelHp[0] = 1000;
        TunelHp[1] = 1000;
        TunelHp[2] = 1000;
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
        LevelManager.MiniGameCompleted += WorkComplete;
        
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
        IsSceneWorkDone = false;
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



