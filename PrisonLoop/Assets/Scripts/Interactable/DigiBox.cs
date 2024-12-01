using System;
using UnityEngine;

public class DigiBox : MonoBehaviour, IInteractable
{
    public int digiId;
    public SpriteRenderer sr;
    public Color iteractColor;
    [SerializeField] public GameObject MinigamePrefab;
    public GameObject minigameInstance;
    public void Interact()
    {
        
        Vector3 position = Vector3.zero;
        minigameInstance = Instantiate(MinigamePrefab, gameObject.transform.position, Quaternion.identity);
        DigidigiHole d;
        if (minigameInstance.gameObject.TryGetComponent<DigidigiHole>(out d))
        {
            // Komponent DigidigiHole istnieje i jest przypisany do zmiennej 'd'
            d.holeId = digiId;
        }
        else
        {
            // Komponent DigidigiHole nie istnieje na tym obiekcie
            Debug.LogWarning("Komponent DigidigiHole nie zosta� znaleziony!");
        }
    }

    public void OnPlayerEnter()
    {
        sr.color = Color.gray;
    }

    public void OnPlayerExit()
    {
        sr.color = Color.white;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
