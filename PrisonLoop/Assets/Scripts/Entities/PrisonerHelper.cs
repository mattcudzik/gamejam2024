using System;
using Entities;
using UnityEngine;
using UnityEngine.Serialization;

public class PrisonerHelper : MonoBehaviour,IInteractable
{
    public Action<HelpOffer> OnHelpBought;
    public HelpOffer helpOffer;
    private SpriteRenderer spriteRenderer;
    private PrisonerStateManager prisonerStateManager;
    public HelpUI helpUI;
    
    private bool helped =false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        helpUI.SetUp(helpOffer);
    }


    void Start()
    {
        helpUI.ShowUI();
    }
    public void Interact()
    {
        if (helped)
        {
            spriteRenderer.color = Color.red;
            Debug.Log("Bought out");
            return;
        }
        spriteRenderer.color = Color.green;
        //Try to buy item with every item form Eq 
        PlayerEq playerEq = GameManager.Instance.PlayerEq;
        Debug.Log(playerEq.ContainsItem(helpOffer.desiredItem.Type));

        if (playerEq.ContainsItem(helpOffer.desiredItem.Type))
        {
            if (playerEq.removeItem(helpOffer.desiredItem))
            {
                //prisonerStateManager
                helped = true;
                helpUI.HideUI();
                OnHelpBought?.Invoke(helpOffer);
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
