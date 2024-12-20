using System;
using UnityEngine;

public class InteractableBox : MonoBehaviour, IInteractable
{
    public SpriteRenderer sr;
    public Color iteractColor;
    [SerializeField] public GameObject MinigamePrefab;
    public GameObject minigameInstance;
    public Boolean interactable = true;
    public void Interact()
    {
        if (interactable)
        {
            Vector3 position = Vector3.zero;
            minigameInstance = Instantiate(MinigamePrefab, position, Quaternion.identity);
            interactable = false;
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
