using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    public SpriteRenderer sr;
    public Color iteractColor;
    public ItemSO Item;
    public void Interact()
    {
        sr.color = iteractColor;
        //Mechanika doda do eq
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
        sr.sprite = Item.Sprite;
        sr.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
