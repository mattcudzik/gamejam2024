using UnityEngine;

public class PrisonerMerchant : MonoBehaviour, IInteractable
{
    public TradeOffer tradeOffer;
    public TradeUI tradeUI;
    SpriteRenderer spriteRenderer;
    private bool boughtOut;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tradeUI.SetUp(tradeOffer);
    }


    void Start()
    {
        tradeUI.ShowUI();
    }
    public void Interact()
    {
        if (boughtOut)
        {
            spriteRenderer.color = Color.red;
            Debug.Log("Bought out");
            return;
        }
        spriteRenderer.color = Color.green;
        //Try to buy item with every item form Eq 
        PlayerEq playerEq = GameManager.Instance.PlayerEq;
        Debug.Log(playerEq.ContainsItem(tradeOffer.desiredItem.Type));

        if (playerEq.ContainsItem(tradeOffer.desiredItem.Type))
        {
            if (playerEq.removeItem(tradeOffer.desiredItem) &&
                playerEq.addItem(tradeOffer.offeredItem))
            {
                boughtOut = true;
                tradeUI.HideUI();
            }
        }
            
    }

    public void OnPlayerEnter()
    {
        //Show u interacted with him 
        spriteRenderer.color = Color.yellow;
    }

    public void OnPlayerExit()
    {
        //Hide interacted with him
        spriteRenderer.color = Color.white;
    }

    
}
