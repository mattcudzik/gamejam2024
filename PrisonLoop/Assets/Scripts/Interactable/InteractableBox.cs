using UnityEngine;

public class InteractableBox : MonoBehaviour, IInteractable
{
    public SpriteRenderer sr;
    public Color iteractColor;

    public void Interact()
    {
        sr.color = iteractColor;
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
