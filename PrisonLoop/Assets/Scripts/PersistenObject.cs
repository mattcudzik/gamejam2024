using UnityEngine;
class PersistentObject: MonoBehaviour
{
    private static PersistentObject instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}



