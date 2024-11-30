using UnityEngine;
class GameManager: MonoBehaviour
{
    public static GameManager Instance;

    public PlayerEq PlayerEq;
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
    }
}



