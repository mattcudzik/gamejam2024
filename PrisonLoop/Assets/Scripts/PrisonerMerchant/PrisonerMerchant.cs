using UnityEngine;

public class PrisonerMerchant : MonoBehaviour, IInteractable
{
    public TradeOffer tradeOffer;
    SpriteRenderer spriteRenderer;
    TradeUI tradeUI;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tradeUI = GetComponentInChildren<TradeUI>();
    }

    public void Interact()
    {
        //Try to buy item with every item form Eq 
        
        if(GameManager.Instance.PlayerEq.ContainsItem(tradeOffer.desiredItem));
        
        throw new System.NotImplementedException();
    }

    public void OnPlayerEnter()
    {
        //Show u interacted with him 
        spriteRenderer.color = Color.yellow;
        throw new System.NotImplementedException();
    }

    public void OnPlayerExit()
    {
        //Hide interacted with him
        spriteRenderer.color = Color.white;
        throw new System.NotImplementedException();
    }

    private bool TryToTrade(ItemEnum itemEnum)
    {
        if (itemEnum == tradeOffer.desiredItem)
        {
            return true;
        }
        return false;
    }
    
}
