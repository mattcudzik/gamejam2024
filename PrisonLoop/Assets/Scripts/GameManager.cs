using UnityEngine;
class GameManager: MonoBehaviour
{
    public static GameManager Instance;

    public PlayerEq PlayerEq;
    public Timer Timer  {get; set;}
        
    void Awake()
    {
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
}



