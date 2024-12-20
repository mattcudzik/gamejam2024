using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

class GameManager: MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public int[] TunelHp;
    public bool IsSceneWorkDone{ get; set; } = false;
    public PlayerEq PlayerEq{get; set;}
    public LevelManager LevelManagerInstance{get; set;}
    public int CurrentDay { get; set; } = 1;
    public int MaxDay { get; set; } = 10;
    [SerializeField] private GameObject UI;
    public Timer Timer;
    [SerializeField] public TextMeshProUGUI Days;

    public bool NextDay()
    {
        CurrentDay++;
        if (CurrentDay > MaxDay)
        {
            return false;
        }
        return true;
    }

    public void GetCaught(string text)
    {
        Timer.StartDelay(text);
    }

    void Awake()
    {
        TunelHp=new int[3];
        TunelHp[0] = 1000;
        TunelHp[1] = 1000;
        TunelHp[2] = 1000;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
        LevelManagerInstance = FindFirstObjectByType<LevelManager>();
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
        //Timer = GetComponentInChildren<Timer>();
    }
    private void OnSceneUnLoaded(Scene scene)
    {
       
    }

    public void UIActive(bool state)
    {
        UI.SetActive(state);
    }

    private void WorkComplete()
    {
        IsSceneWorkDone = true;
        Days.gameObject.SetActive(true);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string name = SceneManager.GetActiveScene().name;
        if ( name== "Meal" || name=="Work" || name=="Laundry")
        {
            IsSceneWorkDone = false;
            Days.gameObject.SetActive(false);
        }
        else
        {
            IsSceneWorkDone = true;
            Days.gameObject.SetActive(true);
        }
        LevelManagerInstance = FindFirstObjectByType<LevelManager>();
        

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



