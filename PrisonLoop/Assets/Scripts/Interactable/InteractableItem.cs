using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    public SpriteRenderer sr;
    public Color iteractColor;
    public ItemSO Item;
    bool interacting = false;

    public void Awake()
    {
        PlayerEq.OnItemDrop += UpdateColor;
    }

    public void OnDestroy()
    {
        PlayerEq.OnItemDrop -= UpdateColor;
    }

    public void Interact()
    {
        sr.color = iteractColor;
        var eq = GameManager.Instance.PlayerEq;
        if (eq.CanGetItem(Item))
        {
            if (eq.addItem(Item))
            {
                Destroy(gameObject);
            }
            sr.color = Color.clear;
        }

        //Mechanika doda do eq
    }

    public void OnPlayerEnter()
    {
        //TODO GameManadger ref
        interacting = true;
        UpdateColor();
    }

    private void UpdateColor()
    {
        if(interacting)
        {
            var eq = GameManager.Instance.PlayerEq;
            if (eq.CanGetItem(Item))
                sr.color = Color.green;
            else
                sr.color = Color.gray;
        }
        else
        {
            sr.color = Color.white;
        }
    }

    public void OnPlayerExit()
    {
        interacting = false;
        UpdateColor();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr.sprite = Item.Sprite;
        sr.color = Color.white;
    }
 
}
